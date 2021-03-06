﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MongoDB.Bson;

namespace Euricom.Timesheets.Util
{
    public class UnixDateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
            {
                throw new Exception(
                    String.Format("Unexpected token parsing date. Expected Float, got {0}.",
                    reader.TokenType));
            }

            var ticks = (long)reader.Value;

            return ticks.FromUnixTicks();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            double ticks;
            if (value is DateTime)
            {
                ticks = ((DateTime)value).ToUnixTicks();
            }
            else
            {
                throw new Exception("Expected date object value.");
            }

            writer.WriteValue(ticks);
        }
    }
}