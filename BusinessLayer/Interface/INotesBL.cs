using CommonLayer.Model;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface INotesBL
    {       
        bool AddNotes(NotesModel addNotesModel, string accountID);

        List<Notes> Display(string accountID);

        bool EditNotes(string noteId, Notes note);

        bool DeleteNote(string noteId);
    }
}
