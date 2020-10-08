using CommonLayer.Model;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class EmployeeRepositoryLayer : IRepositoryLayer
    {
        //private readonly EmployeeService employeeService;
        private readonly IMongoCollection<Employee> _Employee;
        public EmployeeRepositoryLayer(IEmployeeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _Employee = database.GetCollection<Employee>(settings.EmployeeCollectionName);
        }
        public List<Employee> GetEmployeeDetails()
        {
           return  this._Employee.Find(book => true).ToList();
        }

        public bool AddEmployee(EmployeeDetails emp)
        {
           
                try
                {
                    Employee newEmployee = new Employee()
                    {
                        EmployeeFirstName = emp.EmployeeFirstName,
                        EmployeeLastName = emp.EmployeeLastName,
                        Email = emp.Email,
                        PhoneNumber = emp.PhoneNumber
                    };
                    _Employee.InsertOne(newEmployee);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }

