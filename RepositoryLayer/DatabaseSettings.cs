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
    public class EmployeeDatabaseSettings : IEmployeeDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string EmployeeCollectionName { get; set; }


        public string NotesCollectionName { get; set; }
    }

    public interface IEmployeeDatabaseSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

        string EmployeeCollectionName { get; set; }

        string NotesCollectionName { get; set; }

     
    }
}