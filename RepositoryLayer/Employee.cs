using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RepositoryLayer
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
       
        public string EmployeeFirstName { get; set; }

        public string EmployeeLastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

    }
}
