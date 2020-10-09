using CommonLayer.Model;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBusinessLayer
    {
       
        List<Employee> GetEmployeeDetails();

       public bool AddEmployee(EmployeeDetails emp);

        public bool DeleteEmployeeById(string id);

        public bool EditEmployeeDetails(string id, Employee employee);
    }
}
