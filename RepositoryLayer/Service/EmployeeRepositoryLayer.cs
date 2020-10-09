namespace RepositoryLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using CommonLayer.Model;
    using MongoDB.Driver;
    using RepositoryLayer.Interface;
    
    public class EmployeeRepositoryLayer : IRepositoryLayer
    {
        //private readonly EmployeeService employeeService;
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

        public bool AddEmployee(EmployeeDetails employee)
        {
           
                try
                {
                    Employee newEmployee = new Employee()
                    {
                        EmployeeFirstName = employee.EmployeeFirstName,
                        EmployeeLastName = employee.EmployeeLastName,
                        Email = employee.Email,
                        PhoneNumber = employee.PhoneNumber
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

