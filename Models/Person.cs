using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SFI.Models
{
    public class Person
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("namn")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("lösenord")]
        public string Lösenord { get; set; }

        [BsonElement("roll")]
        public string Roll { get; set; }

        [BsonElement("klassId")]
        public ObjectId? KlassId { get; set; }

        [BsonElement("lärareId")]
        public ObjectId? LärareId { get; set; }
    }
}
