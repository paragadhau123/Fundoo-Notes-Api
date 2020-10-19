using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        private INotesRL notesRL;

        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }
        public bool AddNotes(AddNotesModel addNotesModel, string employeeId)
        {
            return this.notesRL.AddNotes(addNotesModel,employeeId);
        }

        public bool DeleteNote(string noteId)
        {
            return this.notesRL.DeleteNote(noteId);
        }

        public List<Notes> Display(string employeeId)
        {
            return this.notesRL.Display(employeeId);
        }

        public bool EditNotes(string noteId, Notes note)
        {
            return this.notesRL.EditNotes(noteId, note);
        }
    }
}
