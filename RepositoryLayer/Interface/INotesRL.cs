using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        bool AddNotes(NotesModel addNotesModel, string accountID);

        List<Notes> Display(string accountID);

        bool EditNotes(string noteId, Notes note);

        bool DeleteNote(string noteId);
    }
}
