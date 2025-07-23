using ONP_shunting_yard_algorithm;

class Program
{
    static void Main(string[] args)
    {
        var onp = new Onp();

        string input = onp.UserInput();
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Error: input is empty.");
            return;
        }
        var tokens = onp.Tokenize(input);
        var onpTokens = onp.ConvertToOpn(tokens);
        double result = onp.Calculate(onpTokens);

        Console.WriteLine("Onp view of your expression: " + string.Join(" ", onpTokens));
        Console.WriteLine("Result: " + result);
  
    }
}
