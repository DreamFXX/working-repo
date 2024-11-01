namespace TestProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string[] pallets = ["B14", "A11", "B12", "A13"];
            //Console.WriteLine("");

            //Array.Clear(pallets, 0, 2);
            //Console.WriteLine($"Clearing 2 ... count: {pallets.Length}");
            //foreach (var pallet in pallets)
            //{
            //    Console.WriteLine($"-- {pallet}");
            //}

            //Console.WriteLine($"Before: {pallets[0]}");
            //Array.Clear(pallets, 0, 2);
            //Console.WriteLine($"After: {pallets[0]}");

            //string value = "abc123";
            //char[] valueArray = value.ToCharArray();
            //Array.Reverse(valueArray);
            //// string result = new string(valueArray);
            //string result = String.Join(",", valueArray);
            //Console.WriteLine(result);

            //string[] items = result.Split(',');
            //foreach (string item in items)
            //{
            //    Console.WriteLine(item);
            //}

            string pangram = "The quick brown fox jumps over the lazy dog";

            // Step 1
            string[] message = pangram.Split(' ');

            //Step 2
            string[] newMessage = new string[message.Length];

            // Step 3
            for (int i = 0; i < message.Length; i++)
            {
                char[] letters = message[i].ToCharArray();
                Array.Reverse(letters);
                newMessage[i] = new string(letters);
            }

            //Step 4
            string result = String.Join(" ", newMessage);
            Console.WriteLine(result);

            Console.ReadKey();
            
        }
    }
}
