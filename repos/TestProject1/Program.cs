namespace TestProject1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string first = "Hello";
            string second = "World";
            string result = string.Format("{0} {1}!", first, second);
            Console.WriteLine(result);

            decimal price = 123.45m;
            int discount = 50;
            Console.WriteLine($"Price: {price:C} (Save {discount:C})");



            Console.ReadKey();
        }
    }
}
