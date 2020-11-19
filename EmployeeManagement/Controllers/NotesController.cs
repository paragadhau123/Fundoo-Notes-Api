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
  
       
        [HttpPost("AddNote")]
        public IActionResult AddNotes(NotesModel addNotesModel)
        {
            try {
                string accountID = this.GetAccountId(); ;
                Notes result = this.notesBL.AddNotes(addNotesModel, accountID);
                if (!result.Equals(false)) {
                    return Ok(new { success = result, Message = "Note added succesfully" });
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
        [HttpGet("DisplayNotes")]
        public IActionResult Display()
        {
            try {
                string accountID = this.GetAccountId();
                var result = this.notesBL.Display(accountID);
                if (!result.Equals(null)) {
                    return Ok(new { success = true, Message = "Note displayed below succesfully", Data = result });
                }
                else
                {
                    return this.NotFound(new { sucess = true, message = "No Notes Are Present to display" });
                }
            }
            catch(Exception e)
            {
                bool success = false;
                return this.BadRequest(new { success, message = e.Message });
            }
}
        [HttpPut("{noteId:length(24)}")]
        public IActionResult EditNotes(string noteId,Notes note)
        {
            try
            {
                bool result = this.notesBL.EditNotes(noteId, note);
                if (result==true) 
                {
                    return this.Ok(new { success = result, Message = "Note is updated successfully" });

                }
                else
                {
                    return this.NotFound(new { success = result, Message = "Note is not updated successfully" });
                }
            }catch(Exception e)
            {
                return BadRequest(new { success = false, message = e.Message });
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
        [HttpPost("{noteId:length(24)}")]
        public IActionResult IsTrash(string noteId)
        {
            try
            {

                var result = this.notesBL.IsTrash(noteId);
                if (result == true)
                    return this.Ok(new { sucess = true, message = "Successfully Trashed notes" });
                else
                    return this.BadRequest(new { success = false, meaasage = " notes dosn't trashed Successfully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { sucess = false, message = e.Message });
            }
        }

        [HttpPost("ArchiveNotes")]
        public IActionResult IsArchive(string noteId)
        {
            try
            {

                var result = this.notesBL.IsArchive(noteId);
                if (result == true)
                    return this.Ok(new { sucess = true, message = "Successfully Trashed notes" });
                else
                    return this.BadRequest(new { success = false, meaasage = " notes dosn't trashed Successfully" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { sucess = false, message = e.Message });
            }
        }
        private string GetAccountId()
        {
            return User.FindFirst("Id").Value;
        }
    }
}
 