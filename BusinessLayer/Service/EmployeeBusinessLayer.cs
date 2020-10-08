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

        public bool deleteEmployeeById(string id)
        {
            return this.repositoryLayer.deleteEmployeeById(id);
        }
    }
}
