using EventHub1._1.DAL.Services;
using EventHub1._1.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Linq;

namespace UltiSports.Services
{
    public class EmailProvider : IEmailService
    {
        private EventService _eventService = new EventService();
        private UserService _userService = new UserService();
        private string _webAddress = "http://localhost:4634";

        private void SendEmail(IEnumerable<User> receipents, string subject, string body)
        {
            var mail = new MailMessage();

            foreach (var r in receipents)
            {
                if (r.EMail == "")
                    continue;
                mail.To.Add(r.EMail);
            }

            var from = "ultievents@gmail.com";
            mail.From = new MailAddress(from);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587; //587 is when program is running from Visual Studio, must be 25 at deployement.
            smtp.Credentials = new System.Net.NetworkCredential("ultievents@gmail.com", "usa-canu");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        public void SendFirstEmail(string dayOfWeek)
        {
            var allEvents = _eventService.GetAllEventsForToday();
            var allActivePlayers = _userService.GetAllActiveUsers() as List<User>;
            var allInactivePlayers = _userService.GetAllInactiveUsers() as List<User>;
            var allPlayers = allActivePlayers.Concat(allInactivePlayers);

            var subject = "Ultimate Software - You're invited!";

            string body = String.Format("Hello,\n\nThis is a message from Ultimate Software's event hub. You have been invited to an after hour event today," +
                          " go to http://josealw7x6530:8080/ to stay updated on the event's status and talk with the other guests!\n\nSee you on the field!\n\nToday's Events:\n\n");

            foreach (var ev in allEvents)
            {
                if (ev.DateCreated.Date == DateTime.Today.Date)
                {
                    //body += ev.Activity.Name + String.Format(" Join: {0}/Attendance/AddPlayer/{1}", _webAddress, ev.EventId);
                    body += "\n";
                }
            }

            SendEmail(allPlayers, subject, body);
        }

        public void SendFinalEmail(string dayOfWeek)
        {
            var allEvents = _eventService.GetAllActiveEvents();
            List<User> players;
            foreach (var ev in allEvents)
            {
                players = new List<User>();
                foreach (var participant in ev.Users)
                {
                    players.Add(participant);
                }

                var subject = String.Format("{0} Verdict: {1}", ev.Activity.Name, "It's on!");

                var body = "See you guys out there!";

                SendEmail(players, subject, body);
            }
        }

        public void SendCancellationEmail(string dayOfWeek)
        {
            var allEvents = _eventService.GetAllActiveEvents();
            List<User> players;
            foreach (var ev in allEvents)
            {
                players = new List<User>();
                foreach (var participants in ev.Users)
                {
                    players.Add(participants);
                }

                var subject = String.Format("{0} Verdict: {1}", ev.Activity.Name, "Today's event has been canceled!");

                var body = "Maybe next week!";

                SendEmail(players, subject, body);
            }
        }
    }

    public interface IEmailService
    {
        void SendFirstEmail(string dayOfWeek);
        void SendFinalEmail(string dayOfWeek);
        void SendCancellationEmail(string dayOfWeek);
    }
}