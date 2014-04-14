using System.Collections.Generic;

namespace EventHub1._1.DAL.Services
{
    public class MessageService : IMessageService
    {
        private UnitOfWork uow = new UnitOfWork();
        public MessageService(UnitOfWork uow)
        {
            this.uow = uow;
        }
        public IEnumerable<Models.Message> GetAllMessages()
        {
            return uow.MessageRepository.Get();
        }

        public Models.Message GetMessageByAuthor(int authorId)
        {
            return uow.MessageRepository.GetByID(authorId);
        }

        public void CreateMessage(Models.Message messageToAdd)
        {
            uow.MessageRepository.Insert(messageToAdd);
            uow.Commit();
        }

        public void DeleteMessageById(int id)
        {
            uow.MessageRepository.Delete(id);
        }
    }

    public interface IMessageService
    {

        IEnumerable<Models.Message> GetAllMessages();

        Models.Message GetMessageByAuthor(int id);

        void CreateMessage(Models.Message activityToAdd);

        void DeleteMessageById(int id);
    }
}