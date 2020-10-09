//-----------------------------------------------------------------------
// <copyright file="IBusinessLayer.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Model;
    using RepositoryLayer;

    public interface IBusinessLayer
    {     
        List<Employee> GetEmployeeDetails();

        public bool AddEmployee(EmployeeDetails employee);

        public bool DeleteEmployeeById(string id);

        public bool EditEmployeeDetails(string id, Employee employee);
    }
}
