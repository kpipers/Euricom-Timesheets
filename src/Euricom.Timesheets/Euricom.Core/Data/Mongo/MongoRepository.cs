using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Euricom.Core.Data.Mongo
{
    public class MongoRepository : IRepository
    {
        private readonly string _connectionString;
        private readonly MongoDatabase _db;

        public MongoRepository(string connectionString)
        {
            _connectionString = connectionString;

            _db = MongoDatabase.Create(_connectionString);
        }

        public TEntity Single<TEntity>(object key) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var query = new QueryDocument("_id", BsonValue.Create(key));
            var entity = collection.FindOneAs<TEntity>(query);

            if (entity == null)
                throw new NullReferenceException("Document with key '" + key + "' not found.");

            return entity;
        }

        public IEnumerable<TEntity> All<TEntity>() where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var entity = collection.FindAllAs<TEntity>();
            return entity;
        }

        public bool Exists<TEntity>(object key) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var query = new QueryDocument("_id", BsonValue.Create(key));
            var entity = collection.FindOneAs<TEntity>(query);
            return (entity != null);
        }

        public void Save<TEntity>(TEntity item) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            collection.Save(item);
        }

        public void Delete<TEntity>(object key) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var query = new QueryDocument("_id", BsonValue.Create(key));
            collection.Remove(query);
        }

        private MongoCollection GetCollection<TEntity>()
        {
            return _db.GetCollection(typeof(TEntity).Name);
        }
    }
}
