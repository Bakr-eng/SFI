using MongoDB.Bson;
using SFI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Repositories
{
    internal interface IMeddelandeRepository
    {
        Task<Meddelande> GetById(ObjectId id);
        Task<List<Meddelande>> GetAllForUser(ObjectId userId);
        Task<List<Meddelande>> GetAllForClass(ObjectId klassId);
        Task<List<Meddelande>> GetConversation(ObjectId user1, ObjectId user2);
        Task<List<Meddelande>> GetMessagesForStudent(ObjectId elevId, ObjectId klassId);

        Task Add(Meddelande meddelande);
        Task Update(Meddelande meddelande);
        Task Delete(ObjectId id);
    }
}
