
using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;
using System;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IBusinessLayer businessLayer;

        public EmployeeController(IBusinessLayer businessLayer)
        {
            this.businessLayer = businessLayer;
        }

        [HttpGet]
        public IActionResult GetAllEmployeeDetails()
        {
           var result = this.businessLayer.GetEmployeeDetails();
            return this.Ok(new { sucess = true, message="Records are displayed below succesfully", data = result });
        }

        [HttpPost]
        public IActionResult AddEmployeeDetails(EmployeeDetails employee)
        {
            bool result = this.businessLayer.AddEmployee(employee);

            if (result == true)
            {
                return this.Ok(new { sucess = true, message = "Record Added" });
            }
            else
            {
                return this.BadRequest(new { sucess = false, message = "Record Not Added" });
            }
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteEmployeeDetails(string id)

        {
            bool result = this.businessLayer.DeleteEmployeeById(id);

            if (result == true)
            {
                return this.Ok(new { sucess = true, message = "Record Deleted" });
            }
            else
            {
                return this.BadRequest(new { sucess = false, message = "Record Not Deleted" });
            }
          }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateEmployeeDetails(String id, Employee employee)
        {
            bool result = this.businessLayer.EditEmployeeDetails(id, employee);

            if (result == true)
            {
                return this.Ok(new { sucess = true, message = "Record Updated" });
            }
            else
            {
                return this.BadRequest(new { sucess = false, message = "Record not Updated" });
            }
        }
    }
}
