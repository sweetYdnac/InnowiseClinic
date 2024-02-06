﻿using MongoDB.Bson;
using System.Net;

namespace Logs.Data.DTOs
{
    public class CreateLogDTO
    {
        public ObjectId Id { get; set; }
        public DateTime DateTime { get; set; }
        public string ApiName { get; set; }
        public string Route { get; set; }
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
