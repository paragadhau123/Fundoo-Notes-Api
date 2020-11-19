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

        public Notes AddNotes(NotesModel addNotesModel, string accountID)
        {
            try
            {
                Notes note = new Notes()
                {
                    Title = addNotesModel.Title,
                    AccountId = accountID,
                    Message = addNotesModel.Message,
                    Image = addNotesModel.Image,
                    Color = addNotesModel.Color,
                    IsPin = addNotesModel.IsPin,
                    IsArchive = addNotesModel.IsArchive,
                    IsTrash = addNotesModel.IsTrash
                };
                this._Note.InsertOne(note);
                return note;
            }
            catch (Exception e)
            {
                return null;
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
        public bool IsTrash(string id)
        {
            try
            {
                List<Notes> list = this._Note.Find(notes => notes.NoteId == id).ToList();

                if (list[0].IsTrash == true)
                {
                    var filter = Builders<Notes>.Filter.Eq("NoteId", id);
                    var update = Builders<Notes>.Update.Set("IsTrash", false);
                    _Note.UpdateOne(filter, update);
                    return true;
                }

                else
                {
                    var filter = Builders<Notes>.Filter.Eq("NoteId", id);
                    var update = Builders<Notes>.Update.Set("IsTrash", true);
                    _Note.UpdateOne(filter, update);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool IsArchive(string id)
        {
            try
            {
                List<Notes> list = this._Note.Find(notes => notes.NoteId == id).ToList();

                if (list[0].IsArchive == true)
                {
                    var filter = Builders<Notes>.Filter.Eq("NoteId", id);
                    var update = Builders<Notes>.Update.Set("IsArchive", false);
                    _Note.UpdateOne(filter, update);
                    return true;
                }

                else
                {
                    var filter = Builders<Notes>.Filter.Eq("NoteId", id);
                    var update = Builders<Notes>.Update.Set("IsArchive", true);
                    _Note.UpdateOne(filter, update);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

    }
    
}
