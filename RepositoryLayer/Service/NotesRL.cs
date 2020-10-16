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

        public NotesRL(IEmployeeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            this._Note = database.GetCollection<Notes>(settings.NotesCollectionName);
        }

        public bool AddNotes(AddNotesModel addNotesModel)
        {
            try {
                Notes note = new Notes()
                {
                    Title = addNotesModel.Title,
                    Message = addNotesModel.Message,
                    Image = addNotesModel.Image,
                    Color = addNotesModel.Color,
                    IsPin = addNotesModel.IsPin,
                    IsArchive = addNotesModel.IsArchive,
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

        public List<Notes> Display()
        {
            return this._Note.Find(note => true).ToList();
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
