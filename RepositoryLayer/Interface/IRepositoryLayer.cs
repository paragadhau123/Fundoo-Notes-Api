﻿using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface IRepositoryLayer
    {
        List<Employee> GetEmployeeDetails();

        bool AddEmployee(EmployeeDetails employee);

        public bool DeleteEmployeeById(string id);

        public bool EditEmployeeDetails(string id, Employee employee);
    }
}
