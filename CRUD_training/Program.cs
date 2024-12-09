using CRUD_training.Services;

namespace CRUD_training
{
    class Program
    {
        internal static void Main(string[] args)
        {
            var substanceService = new SubstanceService();
            substanceService.CreateDatabase();

            while (true)
            {
                Console.WriteLine("\nKratom tracker - menu.");
                Console.WriteLine("1. Add dose");
                Console.WriteLine("2. View all dosages");
                Console.WriteLine("3. Delete recorded dosage");
                Console.WriteLine("4. Exit");

                Console.Write("Enter number of operation you want to do: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter name of substance: ");
                        string value = Console.ReadLine();
                        Console.WriteLine("Enter dosage: ");
                        string dosage = Console.ReadLine();
                        substanceService.AddSubstance(value, dosage);
                        break;
                    case "2":
                        substanceService.ListSubstances();
                        break;
                    case "3":
                        Console.Write("Enter taken dosage ID to delete a record: ");
                        int id = int.Parse(Console.ReadLine());
                        substanceService.DeleteSubstance(id);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}