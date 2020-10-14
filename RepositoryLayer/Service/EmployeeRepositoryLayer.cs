//----------------------- ------------------------------------------------
// <copyright file="EmployeeRepositoryLayer.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using CommonLayer.Model;
    using Microsoft.IdentityModel.Tokens;
    using MongoDB.Driver;
    using RepositoryLayer.Interface;

    /// <summary>
    /// EmployeeRepositoryLayer Class
    /// </summary>
    public class EmployeeRepositoryLayer : IEmployeeRL
    {
        private readonly IMongoCollection<Employee> _Employee;

        public EmployeeRepositoryLayer(IEmployeeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
           this._Employee = database.GetCollection<Employee>(settings.EmployeeCollectionName);
        }

        public List<Employee> GetEmployeeDetails()
        {
            return this._Employee.Find(employee => true).ToList();
        }

        public Employee Login(LoginModel model)
        {
            string pass = EncryptPassword(model.Password);
            List<Employee> validation = _Employee.Find(employee => employee.Email == model.Email && employee.Password == model.Password).ToList();

            Employee newEmployee = new Employee();

            newEmployee.Id = validation[0].Id;
            newEmployee.EmployeeFirstName = validation[0].EmployeeFirstName;
            newEmployee.EmployeeLastName = validation[0].EmployeeLastName;
            newEmployee.PhoneNumber = validation[0].PhoneNumber;
            newEmployee.Email = validation[0].Email;
            newEmployee.Password = pass;
            newEmployee.Token = GenrateJWTToken(model.Email, newEmployee.Id);

            return newEmployee;

        }

        private static string EncryptPassword(string Password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            byte[] encrypt = provider.ComputeHash(encoding.GetBytes(Password));
            String encrypted = Convert.ToBase64String(encrypt);
            return encrypted;
        }
        private string GenrateJWTToken(string email, string id)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey@345fghhhhhhhhhhhhhhhhhhhhhhhhhhhhhfggggggg"));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            string userId = Convert.ToString(id);
            var claims = new List<Claim>
                        {
                            new Claim("email", email),

                            new Claim("id",userId),

                        };
            var tokenOptionOne = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddMinutes(130),
                signingCredentials: signinCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptionOne);
            return token;
        }


        public bool AddEmployee(EmployeeDetails employee1)
        {           
                try
                {
                Employee newEmployee = new Employee()
                    {
                        EmployeeFirstName = employee1.EmployeeFirstName,
                        EmployeeLastName = employee1.EmployeeLastName,
                        Email = employee1.Email,
                        Password= employee1.Password,
                        PhoneNumber = employee1.PhoneNumber,
                        Token= employee1.Token
                    };
                    this._Employee.InsertOne(newEmployee);
                    return true;                 
                }
                catch
                {
                    return false;
                }
            }

        public bool DeleteEmployeeById(string id)
        {
            try
            {
                this._Employee.DeleteOne(employee => employee.Id == id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditEmployeeDetails(string id, Employee employee)
        {
            try
            {
                this._Employee.ReplaceOne(employee => employee.Id == id, employee);
                return true;
            }
            catch 
            {
                return false; 
            }
        }

       
    }
}