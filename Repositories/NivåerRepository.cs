using MongoDB.Bson;
using MongoDB.Driver;
using SFI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Repositories
{
    internal class NivåerRepository : INivåerRepository
    {
        private readonly IMongoCollection<Nivåer> _collection;

        public NivåerRepository()
        {
            var db = new Data.MongoDb();
            _collection = db.Nivåer;
        }
        public async Task Add(Nivåer nivåer)
        {
            await _collection.InsertOneAsync(nivåer);
        }
        public async Task Update(Nivåer nivåer)
        {
            await _collection.ReplaceOneAsync(n => n.Id == nivåer.Id, nivåer);
        }

        public async Task Delete(ObjectId id)
        {
            await _collection.DeleteOneAsync(n => n.Id == id);
        }

        public Task<Nivåer> GetByElevId(ObjectId elevId) =>
            _collection.Find(n => n.ElevId == elevId).FirstOrDefaultAsync();

        public Task<Nivåer> GetById(ObjectId id) =>
            _collection.Find(n => n.Id == id).FirstOrDefaultAsync();


    }
}
