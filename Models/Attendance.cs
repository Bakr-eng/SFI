using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Models
{
    public class Attendance
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("studentId")]
        public ObjectId StudentId { get; set; }


        [BsonElement("datum")]
        public DateTime Datum { get; set; }


        // 0 = Frånvaru (röd)
        // 1 = Sjuk (orange)
        // 2 = Närvarande (grön)
        [BsonElement("status")]
        public int Status { get; set; } 
    }
}
