//-----------------------------------------------------------------------
// <copyright file="EmployeeController.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace EmployeeManagement.Controllers
{
    using System;
    using System.Collections.Generic;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RepositoryLayer;

    /// <summary>
    /// EmployeeController Class
    /// </summary>
   /// [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public IEmployeeBL businessLayer; 

        public EmployeeController(IEmployeeBL businessLayer)
        {
            this.businessLayer = businessLayer;
        }

        [Route("login")]
      //  [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                if (model != null)
                {
                    Employee Data = businessLayer.Login(model);
                    if (Data != null)
                    {
                        return Ok(new { Success = true, Message = "Login successful!!", Data });
                    }
                    else
                    {
                        return BadRequest(new { Success = false, Message = "Wrong Email or Password" });
                    }
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Invalid credentials" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, Message = e.Message });
            }

        }

        [HttpPost("forget")]
        public IActionResult ForgetPassword(ForgetPassword forgetPassword)
        {
           
            if (forgetPassword != null)
            {
                string Token = businessLayer.ForgetPassword(forgetPassword);               
                return Ok(new { Suceess = true, Meassage = "Forget password conformation", Token, forgetPassword });   
            }
            else
            {
                return BadRequest(new { Suceess = false, Meassage = "Email Should not be empty" });
            }
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
                    return this.NotFound(new { sucess = true, message = "No Records Are Present"});
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

                if (!result.Equals(false))
                {
                    return this.Ok(new { sucess = true, message = "Record Deleted Succesfully" });
                }
                else
                {
                    return this.NotFound(new { sucess = false, message = "No Records To Delete" });
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

                if (!result.Equals(false))
                {
                    return this.Ok(new { sucess = true, message = "Record Updated" });
                }
                else
                {
                    return this.NotFound(new { sucess = false, message = "No Records To Updated" });
                }
            }catch(Exception e)
            {
                bool success = false;
                return this.BadRequest(new { success, message = e.Message });
            }
            
        }
    }
}
