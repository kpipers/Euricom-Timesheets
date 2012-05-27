using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace Euricom.Timesheets.Infrastructure
{
    public interface IMongoContext
    {
        MongoCollection<T> GetCollection<T>();
    }
}