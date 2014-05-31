using EventHub1._1.DTO;
using EventHub1._1.Models;
using System.Collections.Generic;

namespace EventHub1._1.DAL.Services
{
    public class PlusOneService : IPlusOneService
    {
        private UnitOfWork uow = new UnitOfWork();
        public IEnumerable<PlusOneDTO> GetAllPlusOnes()
        {
            // Get all the plus ones
            var allPlusOnes = uow.PlusOneRepository.Get();

            // List of all plus ones as DTOs
            var result = new List<PlusOneDTO>();

            // Convert each plus one to a DTO
            foreach (var plusOne in allPlusOnes)
            {
                result.Add(new PlusOneDTO(plusOne));
            }

            return result;
        }

        public Models.PlusOne GetPlusOneById(int id)
        {
            return uow.PlusOneRepository.GetByID(id);
        }

        public void CreateUser(Models.PlusOne plusOneToAdd)
        {
            uow.PlusOneRepository.Insert(plusOneToAdd);
            uow.Commit();
        }

        public void DeletePlusOneById(int id)
        {
            uow.PlusOneRepository.Delete(id);
            uow.Commit();
        }

        public void UpdatePlusOne(Models.PlusOne plusOneToUpdate)
        {
            uow.PlusOneRepository.Update(plusOneToUpdate);
            uow.Commit();
        }

        public PlusOneServiceResponseCodes AddPlusOne(int eventId, string username, string nameForPlusOne)
        {
            PlusOneServiceResponseCodes result;
            var eventToUpdate = uow.EventRepository.GetByID(eventId) as Event;
            var hostOfPlusOne = uow.UserRepository.Get(user => user.Username == username) as List<User>;

            // If the same name has already been used, return an error code.
            foreach (var plusOne in eventToUpdate.PlusOnes)
            {
                if (plusOne.Name == nameForPlusOne.ToLower())
                {
                    result = PlusOneServiceResponseCodes.CannotAddDuplicatePlusOnes;
                    return result;
                }
            }

            // If the name hasn't been used, create a new plus one and add it to both event and user
            // Construct the plus one with the correct relational information
            var newPlusOne = new PlusOne() { Name = nameForPlusOne, EventId = eventId, UserId = hostOfPlusOne[0].UserId };
            eventToUpdate.PlusOnes.Add(newPlusOne);
            hostOfPlusOne[0].PlusOnes.Add(newPlusOne);

            // Track the changes to be made to both repositories
            uow.EventRepository.Update(eventToUpdate);
            uow.UserRepository.Update(hostOfPlusOne[0]);

            // Committ the tracked changes for both repositories
            uow.Commit();
            result = PlusOneServiceResponseCodes.OperationSuccessfull;

            return result;
        }


        public PlusOneServiceResponseCodes DeletePlusOne(int eventId, string username, string nameForPlusOne)
        {
            PlusOneServiceResponseCodes result;
            //var eventToUpdate = uow.EventRepository.GetByID(eventId) as Event;
            //var hostOfPlusOne = uow.UserRepository.Get(user => user.Username == username) as List<User>;

            //// Retrieve the stored plus one by name (name is being kept as unique)
            

            //// If the name hasn't been used, create a new plus one and add it to both event and user
            //// Construct the plus one with the correct relational information
            //var newPlusOne = new PlusOne() { Name = nameForPlusOne, EventId = eventId, UserId = hostOfPlusOne[0].UserId };
            //eventToUpdate.PlusOnes.Add(newPlusOne);
            //hostOfPlusOne[0].PlusOnes.Add(newPlusOne);

            //// Track the changes to be made to both repositories
            //uow.EventRepository.Update(eventToUpdate);
            //uow.UserRepository.Update(hostOfPlusOne[0]);

            //// Committ the tracked changes for both repositories
            //uow.Commit();
            result = PlusOneServiceResponseCodes.OperationSuccessfull;

            return result;
        }


        public List<PlusOneDTO> GetAllPlusOnesForEventByEventId(int eventId)
        {
            var allPlusOnes = uow.PlusOneRepository.Get(plusone => plusone.EventId == eventId) as List<PlusOne>;
            var result = new List<PlusOneDTO>();

            foreach (var plusOne in allPlusOnes)
            {
                result.Add(new PlusOneDTO(plusOne));
            }

            return result;
        }
    }

    public interface IPlusOneService
    {

        IEnumerable<PlusOneDTO> GetAllPlusOnes();

        PlusOne GetPlusOneById(int id);

        List<PlusOneDTO> GetAllPlusOnesForEventByEventId(int eventId);

        void CreateUser(Models.PlusOne plusOneToAdd);

        void DeletePlusOneById(int id);

        void UpdatePlusOne(Models.PlusOne plusOneToUpdate);

        PlusOneServiceResponseCodes AddPlusOne(int eventId, string username, string nameForPlusOne);

        PlusOneServiceResponseCodes DeletePlusOne(int eventId, string username, string nameForPlusOne);
    }

    public enum PlusOneServiceResponseCodes
	{
        CannotAddDuplicatePlusOnes,
        CannotDeleteFictitiousPlusOne,
        OperationSuccessfull
	}
}