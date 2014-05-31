using EventHub1._1.DTO;
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
        public IEnumerable<MessageDTO> GetAllMessages()
        {
            var allMessages = uow.MessageRepository.Get();
            var messagesToDTO = new List<MessageDTO>();

            foreach (var message in allMessages)
            {
                messagesToDTO.Add(new MessageDTO(message));
            }

            return messagesToDTO;
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
        IEnumerable<MessageDTO> GetAllMessages();

        Models.Message GetMessageByAuthor(int id);

        void CreateMessage(Models.Message activityToAdd);

        void DeleteMessageById(int id);
    }
}