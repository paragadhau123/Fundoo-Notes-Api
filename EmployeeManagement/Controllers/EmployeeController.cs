using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;

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
    }
}
