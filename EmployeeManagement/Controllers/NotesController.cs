using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        public INotesBL notesBL;

        public NotesController(INotesBL notesBL)
        {
            this.notesBL = notesBL;
        }

        [HttpPost]
        public IActionResult AddNotes(AddNotesModel addNotesModel)
        {
            try {
                string employeeId = this.GetEmpId();
                bool result = this.notesBL.AddNotes(addNotesModel, employeeId);
                if (!result.Equals(false)) {
                    return Ok(new { success = result, Message = "Note added" });
                }
                else
                {
                    return this.BadRequest(new { sucess = result, message = "Note Not Added" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { sucess = false, message = e.Message });
            }
}
        [HttpGet]
        public IActionResult Display()
        {
            try {
                string employeeId = this.GetEmpId();
                var result = this.notesBL.Display(employeeId);
                if (!result.Equals(null)) {
                    return Ok(new { success = true, Message = "Note added", Data = result });
                }
                else
                {
                    return this.NotFound(new { sucess = true, message = "No Notes Are Present" });
                }
            }
            catch(Exception e)
            {
                bool success = false;
                return this.BadRequest(new { success, message = e.Message });
            }
}
        [HttpPut("{noteid:length(24)}")]
        public IActionResult EditNotes(string noteId,Notes note)
        {
            try
            {
                bool result = this.notesBL.EditNotes(noteId, note);
                if (!result.Equals(false)) 
                {
                    return Ok(new { success = result, Message = "Note is updated successfully" });

                }
                else
                {
                    return NotFound(new { success = result, Message = "Note is not updated successfully" });
                }
            }catch(Exception e)
            {
                return BadRequest(new { success = false, Message = e.Message });
            }
        }

        [HttpDelete("{noteId:length(24)}")]
        public IActionResult DeleteNote(string noteId)
        {
            try
            {
                bool result = this.notesBL.DeleteNote(noteId);

                if (!result.Equals(false))
                {
                    return this.Ok(new { sucess = true, message = "Note Deleted Succesfully" });
                }
                else
                {
                    return this.NotFound(new { sucess = false, message = "No Note Present To Delete" });
                }
            }
            catch (Exception e)
            {
                bool success = false;
                return this.BadRequest(new { success, message = e.Message });
            }

        }
        private string GetEmpId()
        {
            return User.FindFirst("Id").Value;
        }
    }
}
 