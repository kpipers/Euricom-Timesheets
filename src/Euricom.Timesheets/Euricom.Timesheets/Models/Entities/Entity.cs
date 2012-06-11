using MongoDB.Bson;
using Newtonsoft.Json;
using Euricom.Timesheets.Util;

namespace Euricom.Timesheets.Models.Entities
{
    public abstract class Entity
    {
        [JsonProperty(PropertyName = "id")]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; protected set; }
    }
}