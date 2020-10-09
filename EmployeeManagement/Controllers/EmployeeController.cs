
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
        public IActionResult Get()
        {
           var result = businessLayer.GetEmployeeDetails();
            return this.Ok(new { sucess = true, data = result });
        }

        [HttpPost]
        public IActionResult Post(EmployeeDetails emp)
        {
            bool result = this.businessLayer.AddEmployee(emp);

            if (result == true)
                return this.Ok(new { sucess = true, message = "record added" });
            else
                return this.BadRequest(new { sucess = false, message = "record not added" });
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)

        {
            bool result = this.businessLayer.DeleteEmployeeById(id);

            if (result == true)
                return this.Ok(new { sucess = true, message = "record deleted" });
            else
                return this.BadRequest(new { sucess = false, message = "record not deleted" });
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(String id, Employee employee)
        {
            bool result = this.businessLayer.EditEmployeeDetails(id, employee);

            if (result == true)
                return this.Ok(new { sucess = true, message = "record updated" });
            else
                return this.BadRequest(new { sucess = false, message = "record not updated" });

        }
    }
}
