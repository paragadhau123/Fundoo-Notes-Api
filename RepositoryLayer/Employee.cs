//-----------------------------------------------------------------------
// <copyright file="Employee.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Employee Class
    /// </summary>
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "Please enter a valid first name ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "Please enter a valid  last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9]+([.][a-zA-Z0-9]+)?@[a-zA-Z0-9]+.[a-zA-Z]{2,4}([.][a-zA-Z]{2})?$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^([0-9]{2}[ ]+)?[0-9]{10}$", ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; }
    }
}
