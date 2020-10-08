using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer
{
   public  class EmployeeService
    {
        private readonly IMongoCollection<Employee> _Employee;
        public EmployeeService(EmployeeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _Employee = database.GetCollection<Employee>(settings.EmployeeCollectionName);
        }
        public List<Employee> Get() =>
            _Employee.Find(book => true).ToList();
    }
}
