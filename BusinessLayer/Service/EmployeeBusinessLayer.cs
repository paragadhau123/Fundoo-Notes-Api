using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class EmployeeBusinessLayer : IBusinessLayer

    {
        private IRepositoryLayer repositoryLayer;

        public EmployeeBusinessLayer(IRepositoryLayer repositoryLayer)
        {
            this.repositoryLayer = repositoryLayer;
        }

        public List<Employee> GetEmployeeDetails()
        {
            return this.repositoryLayer.GetEmployeeDetails();
        }

        public bool AddEmployee(EmployeeDetails emp)
        {
            return this.repositoryLayer.AddEmployee(emp);
        }

        public bool DeleteEmployeeById(string id)
        {
            return this.repositoryLayer.DeleteEmployeeById(id);
        }

        public bool EditEmployeeDetails(string id, Employee employee)
        {
            return this.repositoryLayer.EditEmployeeDetails(id,employee);
        }
    }
}
