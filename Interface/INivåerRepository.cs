using MongoDB.Bson;
using SFI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Repositories
{
    internal interface INivåerRepository
    {
        Task<Nivåer> GetById(ObjectId id);
        Task<Nivåer> GetByElevId(ObjectId elevId);

        Task Add(Nivåer nivåer);
        Task Update(Nivåer nivåer);
        Task Delete(ObjectId id);
    }
}
