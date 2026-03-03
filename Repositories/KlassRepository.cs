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
    internal class KlassRepository : IKlassRepository
    {
        private readonly IMongoCollection<Klass> _collection;

        public KlassRepository()
        {
            var db = new Data.MongoDb();
            _collection = db.Klasser;
        }

        public async Task Add(Klass klass)
        {
            await _collection.InsertOneAsync(klass);
        }
        public async Task Update(Klass klass)
        {
            await _collection.ReplaceOneAsync(k => k.Id == klass.Id, klass);
        }
        public async Task Delete(ObjectId id)
        {
            await _collection.DeleteOneAsync(k => k.Id == id);
        }

        public Task<List<Klass>> GetAll() => // behöver inte skriva return eftersom det är en expression-bodied member
            _collection.Find(_ => true).ToListAsync();

        public Task<Klass> GetById(ObjectId id)=>
            _collection.Find(k => k.Id == id).FirstOrDefaultAsync();

       
    }
}
