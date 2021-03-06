﻿using BusinessLayer.Interface;
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
        public Notes AddNotes(NotesModel addNotesModel, string accountID)
        {
            return this.notesRL.AddNotes(addNotesModel, accountID);
        }

        public bool DeleteNote(string noteId)
        {
            return this.notesRL.DeleteNote(noteId);
        }

        public List<Notes> Display(string accountID)
        {
            return this.notesRL.Display(accountID);
        }

        public bool EditNotes(string noteId, Notes note)
        {
            return this.notesRL.EditNotes(noteId, note);
        }
        public bool IsTrash(string id)
        {
            return this.notesRL.IsTrash(id);
        }
        public bool IsArchive(string id)
        {
            return this.notesRL.IsArchive(id);
        }
    }
}
