using CommonLayer.Model;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface INotesBL
    {       
        bool AddNotes(AddNotesModel addNotesModel);

        List<Notes> Display();

        bool EditNotes(string noteId, Notes note);

        bool DeleteNote(string noteId);
    }
}
