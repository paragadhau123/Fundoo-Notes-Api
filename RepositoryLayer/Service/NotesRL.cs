using CommonLayer.Model;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly IMongoCollection<Notes> _Note;

        public NotesRL(IFundooNotesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            this._Note = database.GetCollection<Notes>(settings.NotesCollectionName);
        }

        public bool AddNotes(NotesModel addNotesModel, string accountID)
        {
            try {
                Notes note = new Notes()
                {
                    Title = addNotesModel.Title,
                    AccountId = accountID,
                    Message = addNotesModel.Message,
                    Image = addNotesModel.Image,
                    Color = addNotesModel.Color,
                    IsPin = addNotesModel.IsPin,
                    IsArchive = addNotesModel.IsArchive,
                    IsTrash=addNotesModel.IsTrash
                };
                this._Note.InsertOne(note);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool DeleteNote(string noteId)
        {
            try
            {
                this._Note.DeleteOne(note => note.NoteId == noteId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Notes> Display(string accountID)
        {
            return this._Note.Find(note => note.AccountId == accountID).ToList();
        }

        public bool EditNotes(string noteId, Notes note)
        {
            try
            {
                this._Note.ReplaceOne(note => note.NoteId == noteId, note);
                return true;
            }
            catch (Exception e) 
            {
                return false;
            }
           
        }

       
        
    }
}
