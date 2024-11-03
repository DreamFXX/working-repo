namespace aDebuggingPractice
{

    public class VocalCounter
    {

        public static void Vowels(string word) // Česky samohlásky

        {   // Put a breakpoint with F9, F10 bere krok po kroku kroky porgramu, 
            var vowels = new List<char> {'a', 'e', 'i', 'o', 'u'};

            var result = word.ToLower().Count(x => vowels.Contains(x));


            Console.WriteLine($"Number of vowels in {word}: {result}.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            VocalCounter.Vowels("PIKACHU"); // watermelon


            Console.ReadKey();
        }
    }


}
