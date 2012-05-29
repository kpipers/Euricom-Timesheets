using MongoDB.Bson;

namespace Euricom.Timesheets.Models.Entities
{
    public abstract class Entity
    {
        public ObjectId Id { get; protected set; }
    }
}