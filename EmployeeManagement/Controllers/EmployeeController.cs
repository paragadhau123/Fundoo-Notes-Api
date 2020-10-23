//-----------------------------------------------------------------------
// <copyright file="EmployeeController.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace EmployeeManagement.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using CommonLayer.MSMQ;
    using Experimental.System.Messaging;
    using MailKit.Security;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MimeKit;
    using MimeKit.Text;
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
        public IActionResult ForgetPassword(ForgetPassword model)
        {
            if (model != null)
            {
                string Token = businessLayer.ForgetPassword(model);

                var passwordResetLink = Url.Action("ResetPassword", "Employee",
                new { email = model.Email, token = Token }, Request.Scheme);

                MailMessage mailMessage = new MailMessage(model.Email, model.Email);

                mailMessage.Subject = "reset password";             
                mailMessage.Body = passwordResetLink;
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential()
                {
                    UserName = model.Email,
                    Password = "parag123#"
                };
                smtpClient.EnableSsl = true;
               
                smtpClient.Send(mailMessage);

                return Ok(new { success = true, message = "mail send to your email", token = Token });   
            }
            else
            {
                return BadRequest(new { Suceess = false, Meassage = "Email Should not be empty" });
            }
        }


       
        [HttpPost("reset")]      
        public IActionResult ResetPassword(ResetPassword resetPassword)
        {
            string Token = ReceiveMessage();
            string employeeId = this.GetEmpId();            
            bool pass = this.businessLayer.ResetPassword(resetPassword, employeeId);
            return this.Ok(new { success = true,pass });
        }
      
        [HttpGet("employee")]
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

        [HttpPost("employee")]
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
        private static string ReceiveMessage()
        {
            using (MessageQueue myQueue = new MessageQueue())
            {
                myQueue.Path = @".\private$\ForgotPassword";
                Message message = new Message();
                message = myQueue.Receive(new TimeSpan(0, 0, 5));
                message.Formatter = new BinaryMessageFormatter();
                string msg = message.Body.ToString();
                return msg;
            }
        }

        private string GetEmpId()
        {
            return User.FindFirst("Id").Value;
        }
    }
}
