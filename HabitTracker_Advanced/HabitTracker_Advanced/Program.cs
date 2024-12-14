using System.Configuration;
using System.Data.SQLite;
using System.Globalization;
using HabitTracker_Advanced.Models;

namespace HabitTracker_Advanced;

internal class Program
{
    private static readonly string? connectionString =
        ConfigurationManager.ConnectionStrings["DefaultCnn"].ConnectionString;

    //?!
    private static readonly string? tableName = "Table_HabitTracker";

    private static void Main(string[] args)
    {
        var approvedConn = RunNonQueryOnDatabase(@$"CREATE TABLE IF NOT EXISTS {tableName} (
                                                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                                HabitName TEXT NOT NULL,
                                                                DateAndTime TEXT NOT NULL,
                                                                Quantity REAL
                                                                )");

        if (approvedConn != 0)
        {
            Console.WriteLine(
                "System was unable to create specified database. Check program configuration and try again!\n\n");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Database was created successfully.");

        ShowMenu();
    }

    private static void ShowMenu()
    {
        Console.Clear();

        var closeApp = false;
        while (closeApp == false)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("WELCOME TO HABIT TRACKER - ADVANCED");
            Console.WriteLine("\nPick an operation that you want to do:");
            Console.WriteLine("\n0. Close Application");
            Console.WriteLine("1. View All Records");
            Console.WriteLine("2. Add New Record");
            Console.WriteLine("3. Delete A Record");
            Console.WriteLine("4. Update a Record");
            Console.WriteLine("------------------------------\n");

            var userChoice = Console.ReadKey();
            Console.WriteLine("\n");

            switch (userChoice.KeyChar)
            {
                case '0':
                    Console.WriteLine("Thanks for using our advanced Habit Tracker! Goodbye.");
                    closeApp = true;
                    break;
                case '1':
                    ViewAllRecords();
                    break;
                case '2':
                    InsertNewRecord();
                    break;
                case '3':
                    UpdateRecord();
                    break;
                case '4':
                    DeleteRecord();
                    break;
                default:
                    Console.WriteLine("\nYou entered invalid symbol or number! Try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }
    }

    //
    // Database SubOperations section
    //
    public static int RunNonQueryOnDatabase(string commandText)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = commandText;
        var result = command.ExecuteNonQuery();

        connection.Close();

        return result;
    }

    //[By Date]
    private static int CheckDatabaseForRecord(string date)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = $"SELECT EXISTS(SELECT 1 FROM {tableName} WHERE DateAndTime = '{date}')";
        var query = Convert.ToInt32(command.ExecuteScalar());

        connection.Close();

        return query;
    }

    // By ID of record
    private static int CheckDatabaseForRecord(int id)
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = $"SELECT EXISTS(SELECT 1 FROM {tableName} WHERE Id = '{id}')";
        var query = Convert.ToInt32(command.ExecuteScalar());

        connection.Close();

        return query;
    }

    //
    // Program operations (Controllers)
    //
    private static void ViewAllRecords()
    {
        Console.Clear();

        Console.WriteLine("- All Records List -\n");
        var commandText = $"SELECT * FROM {tableName}";

        using var connection = new SQLiteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = commandText;

        List<Habit> habits = new();
        var reader = command.ExecuteReader();

        if (reader.HasRows)
            while (reader.Read())
                habits.Add(new Habit
                {
                    Id = reader.GetInt32(0),
                    HabitName = reader.GetString(1),
                    DateAndTime = DateTime.ParseExact(reader.GetString(2), "yy-MM-dd HH:mm",
                        CultureInfo.InvariantCulture, DateTimeStyles.None),
                    Quantity = reader.GetDouble(3)
                });
        else
            Console.WriteLine("No records stored in this Application. Start logging your habits!");

        connection.Close();

        foreach (var habit in habits)
            Console.WriteLine(
                $"In Date and Time -> {habit.DateAndTime} you did {habit.HabitName} habit routine! info: Quantity = {habit.Quantity}");
    }

    private static void InsertNewRecord()
    {
        Console.Clear();
        Console.WriteLine("\nAdd new record");
        Console.WriteLine("------------------\n");
        Console.WriteLine("Enter the name of habit that you are creating right now.\n");

        var habitName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(habitName))
        {
            Console.WriteLine("Habit name cannot be empty.");
            return;
        }

        var date = GetDate();
        if (date == "0")
        {
            Console.WriteLine("No record will be added.");
            return;
        }

        var quantityInput = GetDouble(
            "Enter the quantity of your habit session. Pick a unit of measurement in the next step.");
        if (quantityInput == 0)
        {
            Console.WriteLine("Quantity cannot be zero.");
            return;
        }

        var recordsExist = CheckDatabaseForRecord(date);
        while (recordsExist != 0)
        {
            Console.WriteLine("Record with this date already exists! Try again.");
            date = GetDate();
            if (date == "0")
            {
                Console.WriteLine("No record will be added.");
                return;
            }

            recordsExist = CheckDatabaseForRecord(date);
        }

        var commandText =
                $"INSERT INTO {tableName} (HabitName, DateAndTime, Quantity) VALUES ('{habitName}', '{date}', {quantityInput})";
            int success = RunNonQueryOnDatabase(commandText);

            if (success == 0)
                Console.WriteLine("Record was not added to the database.\n");
            else
                Console.WriteLine("Record has been added!\n\n");
    }

    private static void UpdateRecord()
    {
        Console.Clear();

    }

    private static void DeleteRecord()
    {
        Console.Clear();
        ViewAllRecords();

        int id = 
            GetInt("Enter the ID of record that you want to delete from the list above.\n-quick actions -> enter 0 to go back to the menu.\n\nID: ");
        if (id == 0)
        {
            Console.WriteLine("No records were deleted.\n\n");
            return;
        }
        
        int recordExists = CheckDatabaseForRecord(id);
        if (recordExists == 0)
        {
            Console.WriteLine($"Record with  ID: {id} does not exist.\n\n");
            return;
        }

        string commandText = $"DELETE FROM {tableName} WHERE Id = {id}";
        int success = RunNonQueryOnDatabase(commandText);
        if (success == 0)
            Console.WriteLine("A problem has occurred with finding specified ID. Check record IDs and try again.\n\n");
        else
            Console.WriteLine($"Record with {id} has been successfully deleted.\n\n");

    }
    //
    // Get Date and Quantity section
    //
    private static string GetDate()
    {
        var validDate = false;
        DateTime date = new();
        while (validDate == false)
        {
            Console.WriteLine("Quick actions -> Type 0 to go back to the menu // Type 1 to set date to today.");
            Console.WriteLine(
                "Enter the date of your habit record in the format below.\n- (yy-MM-dd HH:mm)\n\nDate and Time:");
            var userInput = Console.ReadLine();

            if (userInput == "0")
                return "0";
            if (userInput == "1")
                return DateTime.Now.ToString("yy-MM-dd HH:mm");

            validDate = DateTime.TryParseExact(userInput, "yy-MM-dd HH:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out date);

            if (validDate)
                break;
        }

        return date.ToString("yy-MM-dd HH:mm");
    }

    private static double GetDouble(string message)
    {
        Console.WriteLine(message);
        double quantity = -1;
        var isValid = false;

        while (isValid == false)
        {
            var input = Console.ReadLine();
            if (double.TryParse(input, out quantity) && quantity >= 0)
                isValid = true;
            else
                Console.WriteLine("Unfortunately, number is not valid. It must be positive number.");
        }

        return quantity;
    }

    private static int GetInt(string message)
    {
        Console.WriteLine(message);
        int number = -1;
        var isValid = false;
        while (isValid == false)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out number) && number >= 0)
                isValid = true;
            else
                Console.WriteLine("Your entered number must be positive full number. Try again!");
        }

        return number;
    }
}