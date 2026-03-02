using MongoDB.Driver;
using SFI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Data
{
    internal class MongoDb
    {
        // string m = "mongodb+srv://sfiuser:bakr123@cluster.my90zyb.mongodb.net/?appName=Cluster";
        private readonly IMongoDatabase _database;

        public MongoDb()
        {
            const string connectionString = "mongodb+srv://sfiuser:bakr123@cluster.my90zyb.mongodb.net/?appName=Cluster";
            var client = new MongoClient(connectionString);

            _database = client.GetDatabase("SFI");
        }

        public IMongoCollection<Person> Personer =>
            _database.GetCollection<Person>("Personer");

        public IMongoCollection<Klass> Klasser =>
            _database.GetCollection<Klass>("Klasser");

        public IMongoCollection<Meddelande> Meddelanden =>
            _database.GetCollection<Meddelande>("Meddelanden");

        public IMongoCollection<Nivåer> Nivåer =>
            _database.GetCollection<Nivåer>("Nivåer");
    }
}
