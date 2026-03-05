using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Models
{
    public class RecipientModels 
    {
        // Det finns bara för UI, int för databasen
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Typ { get; set; }
    }
}
