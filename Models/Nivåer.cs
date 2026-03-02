using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Models
{
    public class Nivåer
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("elevId")]
        public ObjectId ElevId { get; set; }


        [BsonElement("tala")]
        public int Tala { get; set; } = 0;

        [BsonElement("skriva")] 
        public int Skriva { get; set; } = 0;

        [BsonElement("läsa")]
        public int Läsa { get; set; } = 0;

        [BsonElement("höra")]
         public int Höra { get; set; } = 0;


        [BsonElement("uppdateringsDag")]
        public DateTime UppdateringsDag { get; set; } = DateTime.Now;

    }
}
