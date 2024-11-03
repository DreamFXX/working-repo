namespace aDebuggingPractice
{

    public class VocalCounter
    {
        public static List<char> _refererenceVowels;

        public static void Vowels(string word) // Česky samohlásky
        { // Put a breakpoint with F9, F10 bere krok po kroku kroky porgramu, 
            // var vowels = new List<char> {'a', 'e', 'i', 'o', 'u'};

            var numberOfVowels = 0;

            foreach (var letter in _refererenceVowels) //´najetím kurzoru na jmeno variable - zobrazeni hodnoty variable.
            {
                foreach (var character in word)
                {
                    if (letter == character)
                    {
                        numberOfVowels++;
                    }
                }
            }

            Console.WriteLine($"Number of vowels in {word}: {numberOfVowels}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            VocalCounter.Vowels("PIKACHU");


            Console.ReadKey();
        }
    }


}
