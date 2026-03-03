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
    internal class PersonRepository : IPersonRepository
    {
        private readonly IMongoCollection<Person> _collection;

        public PersonRepository()
        {
            var db = new Data.MongoDb();
            _collection = db.Personer;
        }
        public async Task Add(Person person)
        {
            await _collection.InsertOneAsync(person);
        }
        public async Task Update(Person person)
        {
            await _collection.ReplaceOneAsync(p => p.Id == person.Id, person);
        }
        public async Task Delete(ObjectId id)
        {
            await _collection.DeleteOneAsync(p => p.Id == id);
        }

        public Task<List<Person>> GetAllStudents() => // Behöver inte skirva "return" eftersom det är en expression-bodied member
            _collection.Find(p => p.Roll == "Elev").ToListAsync();

        public Task<List<Person>> GetAllTeachers() =>
            _collection.Find(p => p.Roll == "Lärare").ToListAsync();

        public Task<Person> GetById(ObjectId id)=>
            _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public Task<Person> GetByName(string name) =>
            _collection.Find(p => p.Name == name).FirstOrDefaultAsync();
        public Task<List<Person>> GetStudentsByClass(ObjectId klassId)=>
            _collection.Find(p => p.KlassId == klassId && p.Roll == "Elev").ToListAsync();

       
    }
}
