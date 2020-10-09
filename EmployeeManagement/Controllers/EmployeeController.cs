//-----------------------------------------------------------------------
// <copyright file="EmployeeController.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace EmployeeManagement.Controllers
{
    using System;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Mvc;
    using RepositoryLayer;

    /// <summary>
    /// EmployeeController Class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public IBusinessLayer businessLayer; 

        public EmployeeController(IBusinessLayer businessLayer)
        {
            this.businessLayer = businessLayer;
        }

        [HttpGet]
        public IActionResult GetAllEmployeeDetails()
        {
            try {

                var result = this.businessLayer.GetEmployeeDetails();
                if (!result.Equals(null)) {
                    return this.Ok(new { sucess = true, message = "Records are displayed below succesfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { sucess = true, message = "No Records Are Present"});
                }
            }
            catch (Exception e)
            {
                bool success = false;
                return this.BadRequest(new { success, message = e.Message });
            }
       }
        [HttpPost]
        public IActionResult AddEmployeeDetails(EmployeeDetails employee)
        {
            try {
                bool result = this.businessLayer.AddEmployee(employee);

                if (!result.Equals(false))
                {
                    return this.Ok(new { sucess = true, message = "Record Added" });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Record Not Added"});
                }
            }
            catch(Exception e)
            {
                 return this.BadRequest(new { sucess = false, message =e.Message });
            }
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteEmployeeDetails(string id)
        {
            try
            {
                bool result = this.businessLayer.DeleteEmployeeById(id);

                if (!result.Equals(null))
                {
                    return this.Ok(new { sucess = true, message = "Record Deleted Succesfully" });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "No Records To Delete" });
                }
            }
            catch(Exception e)
            {
                bool success = false;
                return this.BadRequest(new { success, message = e.Message });
            }
           
          }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateEmployeeDetails(string id, Employee employee)
        {
            try
            {
                bool result = this.businessLayer.EditEmployeeDetails(id, employee);

                if (!result.Equals(null))
                {
                    return this.Ok(new { sucess = true, message = "Record Updated" });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "No Records To Updated" });
                }
            }catch(Exception e)
            {
                bool success = false;
                return this.BadRequest(new { success, message = e.Message });
            }
            
        }
    }
}
