using ONP_shunting_yard_algorithm;

class Program
{
    static void Main(string[] args)
    {
        Onp parser = new Onp();

        string input = parser.UserInput();
        List<string> tokens = parser.Tokenize(input);
        
        Console.WriteLine("Onp view of your expression:");
        List<string> output = parser.ConvertToOpn(tokens);
        Console.WriteLine(string.Join(" ", output));
    }
}
