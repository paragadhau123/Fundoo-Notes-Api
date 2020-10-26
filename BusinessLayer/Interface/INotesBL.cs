using CommonLayer.Model;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface INotesBL
    {
        Notes AddNotes(NotesModel addNotesModel, string accountID);

        List<Notes> Display(string accountID);

        Notes EditNotes(string noteId, Notes note);

        bool DeleteNote(string noteId);
    }
}
