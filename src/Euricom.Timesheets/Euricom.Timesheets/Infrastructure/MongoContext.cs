using System;
using System.Collections.Generic;
using System.Configuration;
using MongoDB.Driver;
using Euricom.Timesheets.Models.Entities;

namespace Euricom.Timesheets.Infrastructure
{
    public class MongoContext : IMongoContext
    {
        private static readonly Dictionary<Type, string> _collectionMap = new Dictionary<Type, string>
        {
            { typeof(ApplicationName), "ApplicationName" }
        };

        private MongoDatabase _database;
        private string _connectionstring;

        public MongoContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public MongoContext()
            : this(ConfigurationManager.AppSettings["MONGOHQ_URI"])
        {
        }

        private MongoDatabase Database
        {
            get
            {
                if (_database == null)
                    _database = MongoDatabase.Create(_connectionstring);

                return _database;
            }
        }

        public MongoCollection<T> GetCollection<T>()
        {
            return Database.GetCollection<T>(_collectionMap[typeof(T)]);
        }
    }
}