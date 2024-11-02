// See https://aka.ms/new-console-template for more information
Console.WriteLine("Metody - MS Learn");


void displayRandomNumbers() 
{
Random random = new Random();

for(int i = 0; i < 5; i++)
{
    Console.Write($"{random.Next(1, 100)} ");
}

}
