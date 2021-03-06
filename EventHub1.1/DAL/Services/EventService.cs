﻿using EventHub1._1.DTO;
using EventHub1._1.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using UltiSports.Services;

namespace EventHub1._1.DAL.Services
{
    public class EventService : IEventService
    {
        private UnitOfWork uow = new UnitOfWork();

        public IEnumerable<Models.Event> GetAllActiveEvents()
        {
            var allActive = uow.EventRepository.Get(ev => ev.Active);
            var result = from ev in allActive
                         where ev.DateCreated.Date == DateTime.Today.Date
                         select ev;

            return result.AsEnumerable();
        }

        public Models.Event GetEventById(int id)
        {
            return uow.EventRepository.GetByID(id);
        }

        public void CreateEvent(Models.Event eventToAdd)
        {
            eventToAdd.Activity = uow.ActivityRepository.GetByID(eventToAdd.ActivityId);
            uow.EventRepository.Insert(eventToAdd);
            uow.Commit();
        }

        public void ToggleActiveById(int id)
        {
            var result = uow.EventRepository.GetByID(id);
            result.Active = !result.Active;
            uow.EventRepository.Update(result);

            uow.Commit();
        }

        public void DeleteEventById(int id)
        {
            uow.EventRepository.Delete(id);
        }

        public void UpdateEvent(Models.Event eventToUpdate)
        {
            uow.EventRepository.Update(eventToUpdate);
            uow.Commit();
        }


        public EventServiceResponseCodes JoinEvent(int eventId, string username)
        {
            EventServiceResponseCodes result;
            var eventToUpdate = uow.EventRepository.GetByID(eventId);
            var userToJoinEvent = uow.UserRepository.Get(user => user.Username == username) as List<User>;

            if (!eventToUpdate.Users.Contains(userToJoinEvent[0]))
            {
                var newUser = uow.UserRepository.GetByID(userToJoinEvent[0].UserId);
                eventToUpdate.Users.Add(newUser);
                uow.EventRepository.Update(eventToUpdate);

                uow.Commit();
                result = EventServiceResponseCodes.OperationSuccessfull;
            }
            else
            {
                result = EventServiceResponseCodes.CannotJoinTwice;
            }

            return result;
        }


        public EventServiceResponseCodes LeaveEvent(int eventId, string username)
        {
            EventServiceResponseCodes result;
            var eventToUpdate = uow.EventRepository.GetByID(eventId);
            var userToJoinEvent = uow.UserRepository.Get(user => user.Username == username) as List<User>;

            if (eventToUpdate.Users.Contains(userToJoinEvent[0]))
            {
                var newUser = uow.UserRepository.GetByID(userToJoinEvent[0].UserId);
                eventToUpdate.Users.Remove(newUser);
                uow.EventRepository.Update(eventToUpdate);

                uow.Commit();
                result = EventServiceResponseCodes.OperationSuccessfull;
            }
            else
            {
                result = EventServiceResponseCodes.CannotLeaveIfNotJoined;
            }

            return result;
        }


        public List<UserDTO> GetParticipantsByEventId(int eventId)
        {
            var eventInQuestion = uow.EventRepository.GetByID(eventId);
            var usersForEventInQuestion = new List<UserDTO>();

            foreach (var user in eventInQuestion.Users)
            {
                usersForEventInQuestion.Add(new UserDTO(user));
            }

            return usersForEventInQuestion;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return uow.EventRepository.Get();
        }

        public IEnumerable<Event> GetAllEventsForToday()
        {
            return uow.EventRepository.context.Events;
        }

        public EventServiceResponseCodes SendEmail()
        {
            var result = EventServiceResponseCodes.EmailHasBeenSent;

            var emailProvider = new EmailProvider();
            emailProvider.SendFirstEmail(DateTime.Today.DayOfWeek.ToString());

            return result;
        }
    }

    public interface IEventService
    {
        IEnumerable<Event> GetAllEventsForToday();

        IEnumerable<Models.Event> GetAllEvents();

        IEnumerable<Models.Event> GetAllActiveEvents();

        Models.Event GetEventById(int id);

        void CreateEvent(Models.Event eventToAdd);

        void ToggleActiveById(int id);

        void DeleteEventById(int id);

        void UpdateEvent(Models.Event eventToUpdate);

        EventServiceResponseCodes JoinEvent(int eventId, string username);

        EventServiceResponseCodes LeaveEvent(int eventId, string username);

        List<UserDTO> GetParticipantsByEventId(int eventId);

        EventServiceResponseCodes SendEmail();
    }

    public enum EventServiceResponseCodes
    {
        OperationSuccessfull,
        CannotJoinTwice,
        CannotLeaveIfNotJoined,
        UsersFound,
        EmailHasBeenSent
    }
}