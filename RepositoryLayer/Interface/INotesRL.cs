﻿using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        bool AddNotes(AddNotesModel addNotesModel);

        List<Notes> Display();

        bool EditNotes(string noteId, Notes note);

        bool DeleteNote(string noteId);
    }
}