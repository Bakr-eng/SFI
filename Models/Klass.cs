using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Models
{
    internal class Klass
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("namn")]
        public string Name { get; set; }

        [BsonElement("lärareId")]
        public ObjectId? LärareId { get; set; }
    }
}
