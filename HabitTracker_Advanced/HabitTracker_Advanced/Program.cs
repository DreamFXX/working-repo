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
    private static readonly string tableName = "Table_HabitTracker";

    private static void Main(string[] args)
    {
        var approvedConn = RunNonQueryOnDatabase(@$"CREATE TABLE IF NOT EXISTS {tableName} (
                                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                            HabitName TEXT NOT NULL,
                                                            DateAndTime TEXT NOT NULL,
                                                            Quantity REAL,
                                                            )");

        if (approvedConn != 0)
        {
            Console.WriteLine("System was unable to create specified database. Check program configuration and try again!\n\n");
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
                    //UpdateRecord();
                    break;
                case '4':
                    //DeleteRecord();
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
    private static int RunNonQueryOnDatabase(string commandText)
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
        command.CommandText = $"SELECT EXISTS(SELECT 1 FROM {tableName} WHERE DateAndTime = '{date}'";
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
        command.CommandText = $"SELECT EXISTS(SELECT 1 FROM {tableName} WHERE Id = '{id}'";
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
        Console.WriteLine("------------------");

        var dateInput = GetDate();
        var quantityInput =
            GetQuantity(
                "Enter the quantity of your habit length, dose or anything in any unit you want.\n-next page is about picking the measurement type.");

        var recordsExist = 1;
        string date = null;

        while (recordsExist != 0)
        {
            date = GetDate();
            recordsExist = CheckDatabaseForRecord(date);

            if (recordsExist == 0) break;

            Console.WriteLine("Record with this date already exists! Try again.");
        }

        if (date == "0")
        {
            Console.WriteLine("No record will by added.");
            return;
        }

        var quantity =
            GetQuantity("Enter the quantity of your habit session. Pick a unit of measurement in the next step.\n");
        if (quantity == 0) Console.WriteLine("No records will be added.");
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

    private static double GetQuantity(string message)
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
}