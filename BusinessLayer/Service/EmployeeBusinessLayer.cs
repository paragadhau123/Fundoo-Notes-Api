//-----------------------------------------------------------------------
// <copyright file="EmployeeBusinessLayer.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using RepositoryLayer;
    using RepositoryLayer.Interface;

    /// <summary>
    /// EmployeeBusinessLayer Class
    /// </summary>
    public class EmployeeBusinessLayer : IEmployeeBL
    {
        private IEmployeeRL repositoryLayer;

        public EmployeeBusinessLayer(IEmployeeRL repositoryLayer)
        {
            this.repositoryLayer = repositoryLayer;
        }

        public List<Employee> GetEmployeeDetails()
        {
            return this.repositoryLayer.GetEmployeeDetails();
        }

        public bool AddEmployee(EmployeeDetails employee)
        {
            return this.repositoryLayer.AddEmployee(employee);
        }

        public bool DeleteEmployeeById(string id)
        {
            return this.repositoryLayer.DeleteEmployeeById(id);
        }

        public bool EditEmployeeDetails(string id, Employee employee)
        {
            return this.repositoryLayer.EditEmployeeDetails(id, employee);
        }

        public Employee Login(LoginModel model)
        {
            try
            {
               return this.repositoryLayer.Login(model);
                              
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string ForgetPassword(ForgetPassword model)
        {
            string Token = repositoryLayer.ForgetPassword(model);
            return Token;
        }

        public bool ResetPassword( ResetPassword resetPassword, string employeeId)
        {
            bool pass = repositoryLayer.ResetPassword(resetPassword, employeeId);
            return pass;
        }
    }
}
