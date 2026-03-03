using MongoDB.Bson;
using SFI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Repositories
{
    internal interface IPersonRepository
    {
        Task<Person> GetById(ObjectId id );
        Task<Person> GetByName(string name);
        Task<List<Person>> GetAllStudents();
        Task<List<Person>> GetStudentsByClass(ObjectId klassId);
        Task<List<Person>> GetAllTeachers();
        Task Add(Person person);
        Task Update(Person person);
        Task Delete(ObjectId id);
    }
}
