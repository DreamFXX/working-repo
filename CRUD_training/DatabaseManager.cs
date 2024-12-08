using Microsoft.Data.Sqlite;

namespace CRUD_training;

internal class DatabaseManager
{
    private string? _connectionString = @"Data Source = CrudApp_Db.db";

    public void CreateDatabase()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var command = new SqliteCommand();
            command.CommandText = @"CREATE TABLE IF NOT EXISTS Dosages (
                                   Id PRIMARY KEY AUTOINCREMENT,
                                   Dose INTEGER,
                                   Date TEXT)";
            command.ExecuteNonQuery();
        }
    }

}