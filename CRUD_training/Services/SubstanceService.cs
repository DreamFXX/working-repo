using Microsoft.Data.Sqlite;
using Dapper;
using CRUD_training.Models;
namespace CRUD_training.Services;

public class SubstanceService
{
    private readonly string _connectionString = @"Data Source = SubstanceRecords_Data.db";

    public void CreateDatabase()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS TrackedSubstances (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Date TEXT NOT NULL,
                    Dosage INTEGER NOT NULL,
                    Unit TEXT NOT NULL
            );";
            connection.Execute(createTableQuery);
        }
    }

    public void AddSubstance(string name)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            string insertQuery =
                "INSERT INTO TrackedSubstances (Name, Date, Dosage, Unit) VALUES (@Name, @Date, @Dosage, @Unit)";
            // just for the demo try.
            connection.Execute(insertQuery,
                new { Name = name, Date = DateTime.Now.ToString("dd-MM-yyyy"), Dosage = 1, Unit = "mg" });
            Console.WriteLine("Dosage was added to your records.");
        }
    }

    public void ListSubstances()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            string selectQuery = "SELECT * FROM TrackedSubstances";
            List<Substance> substances = connection.Query<Substance>(selectQuery).ToList();
            Console.WriteLine("Your recorded drug intakes:");
            foreach (var substance in substances)
            {
                Console.WriteLine(
                    $"Id: {substance.Id}, Name: {substance.Name}, Date: {substance.Date}, Dose: {substance.Dose}{substance.Unit}");
            }
        }
    }

    public void DeleteSubstance(int id)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            string deleteQuery = "DELETE FROM TrackedSubstances WHERE Id = @Id";
            int rowsAffected = connection.Execute(deleteQuery, new { Id = id });

            if (rowsAffected > 0)
            {
                Console.WriteLine("Habit deleted successfully!");
            }
            else
            {
                Console.WriteLine("No habit found with the specified ID.");
            }
        }
    }
}
    
    

