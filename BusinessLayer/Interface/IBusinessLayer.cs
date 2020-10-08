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

       public bool SamplePost(EmployeeDetails emp);

    }
}
