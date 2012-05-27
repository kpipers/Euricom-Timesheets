using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace Euricom.Timesheets.Models.Entities
{
    public abstract class Entity
    {
        public ObjectId Id { get; protected set; }
    }
}