using System;
using ONP_shunting_yard_algorithm;

class Program
{
    static void Main(string[] args)
    {
        ONP parser = new ONP();

        string input = parser.UserInput();
        List<string> tokens = parser.Tokenize(input);
        
        Console.WriteLine("Tokens: ");
        foreach (var token in tokens)
        {
            Console.WriteLine(token);
        }
    }
}
