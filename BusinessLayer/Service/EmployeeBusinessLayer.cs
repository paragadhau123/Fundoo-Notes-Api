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

        public List<Employee> SampleGet()
        {
            return this.repositoryLayer.SampleGet();
        }

        public bool SamplePost(EmployeeDetails emp)
        {
            return this.repositoryLayer.SamplePost(emp);
        }
    }
}
