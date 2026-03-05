using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Models
{
    internal class Meddelande
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("avsändareId")]
        public ObjectId AvsändareId { get; set; }

        [BsonElement("motagareTyp")]
        public string MotagareTyp { get; set; }

        [BsonElement("mottagareId")]
        public ObjectId? MottagareId { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("Tid")]
        public DateTime Tid { get; set; }

        [BsonElement("lästs")]
        public bool Lästs { get; set; }

    }
}
