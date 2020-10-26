//-----------------------------------------------------------------------
// <copyright file="EmployeeDatabaseSettings.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer
{
    /// <summary>
    /// EmployeeDatabaseSettings Class 
    /// </summary>
    public class FundooNotesDatabaseSettings : IFundooNotesDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string AccountsCollectionName { get; set; }


        public string NotesCollectionName { get; set; }
    }

    public interface IFundooNotesDatabaseSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

        string AccountsCollectionName { get; set; }

        string NotesCollectionName { get; set; }

     
    }
}