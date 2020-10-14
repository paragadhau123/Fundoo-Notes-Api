//-----------------------------------------------------------------------
// <copyright file="IRepositoryLayer.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Model;

    public interface IEmployeeRL
    {
        List<Employee> GetEmployeeDetails();

        bool AddEmployee(EmployeeDetails employee);

        public bool DeleteEmployeeById(string id);

        public bool EditEmployeeDetails(string id, Employee employee);

        Employee Login(LoginModel model);
    }
}
