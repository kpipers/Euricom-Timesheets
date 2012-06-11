using System;
using Newtonsoft.Json;
using MongoDB.Bson;

namespace Euricom.Timesheets.Util
{
    public class ObjectIdConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(ObjectId));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
            {
                throw new Exception(
                    String.Format("Unexpected token parsing ObjectId. Expected String, got {0}.",
                    reader.TokenType));
            }

            return new ObjectId((string)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is ObjectId)
            {
                var id = ((ObjectId)value).ToString();
                writer.WriteValue(id);
            }
            else
            {
                throw new Exception("Expected ObjectId value.");
            }
        }
    }
}