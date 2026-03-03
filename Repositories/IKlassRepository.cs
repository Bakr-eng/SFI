using MongoDB.Bson;
using SFI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Repositories
{
    internal interface IKlassRepository
    {
        Task<Klass> GetById(ObjectId id);
        Task<List<Klass>> GetAll();

        Task Add(Klass klass);
        Task Update(Klass klass);
        Task Delete(ObjectId id);
    }
}
