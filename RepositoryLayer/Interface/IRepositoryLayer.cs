using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface IRepositoryLayer
    {
        List<Employee> SampleGet();
        bool SamplePost(EmployeeDetails emp);
    }
}
