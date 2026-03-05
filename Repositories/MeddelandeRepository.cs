using MongoDB.Bson;
using MongoDB.Driver;
using SFI.Data;
using SFI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Repositories
{
    internal class MeddelandeRepository : IMeddelandeRepository
    {
        private readonly IMongoCollection<Meddelande> _collection;

        public MeddelandeRepository()
        {
            var db = new MongoDb();
            _collection = db.Meddelanden;
        }

        public Task<Meddelande> GetById(ObjectId id) =>
            _collection.Find(m => m.Id == id).FirstOrDefaultAsync();

        public Task<List<Meddelande>> GetAllForUser(ObjectId userId) =>
            _collection.Find(m => m.MottagareId == userId || m.AvsändareId == userId)
            .SortByDescending(m => m.Tid)
            .ToListAsync();

        public Task<List<Meddelande>> GetAllForClass(ObjectId klassId) =>
        _collection.Find(m => m.MotagareTyp == "klass" && m.MottagareId == klassId)
        .SortByDescending(m => m.Tid)
        .ToListAsync();

        public Task<List<Meddelande>> GetConversation(ObjectId user1, ObjectId user2) =>
            _collection.Find(m =>
            (m.AvsändareId == user1 && m.MottagareId == user2) ||
            (m.AvsändareId == user2 && m.MottagareId == user1))
            .SortBy(m => m.Tid)
            .ToListAsync();

        public async Task Add(Meddelande meddelande)
        {
            await _collection.InsertOneAsync(meddelande);
        }
        public async Task Update(Meddelande meddelande)
        {
            await _collection.ReplaceOneAsync(m => m.Id == meddelande.Id, meddelande);
        }
        public async Task Delete(ObjectId id)
        {
            await _collection.DeleteOneAsync(m => m.Id == id);
        }
    }
    
}
