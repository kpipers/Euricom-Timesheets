using MongoDB.Driver;

namespace Euricom.Timesheets.Infrastructure
{
    public interface IMongoContext
    {
        MongoCollection<T> GetCollection<T>();
    }
}