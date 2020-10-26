using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        Notes AddNotes(NotesModel addNotesModel, string accountID);

        List<Notes> Display(string accountID);

        Notes EditNotes(string noteId, Notes note);

        bool DeleteNote(string noteId);
    }
}
