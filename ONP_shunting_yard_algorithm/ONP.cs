namespace ONP_shunting_yard_algorithm;

public class Onp
{
    public string UserInput()
    {
        Console.WriteLine("Please put in your mathematical expression: ");
        return Console.ReadLine();
    }
    private bool IsNumber(string token)
    {
        return double.TryParse(token, out _);
    }
    public List<string> Tokenize(string input)
    {
            List<string> tokens = new List<string>();
            string number = "";

            foreach (char c in input)
            {
                if (char.IsWhiteSpace(c)) continue;

                if (char.IsDigit(c) || c == '.')
                {
                    number += c;
                }
                else
                {
                    if (number != "")
                    {
                        tokens.Add(number);
                        number = "";
                    }

                    if ("+-*/^()".Contains(c))
                    {
                        tokens.Add(c.ToString());
                    }
                    else
                    {
                        Console.WriteLine($"Unknown symbol: {c}, use only numbers and operation signs");
                        Environment.Exit(1);
                    }
                }
            }

            if (number != "")
            {
                tokens.Add(number);
            }
            return tokens;
    }

    public Dictionary<string, int> Precedence = new Dictionary<string, int>()
    {
        ["^"] = 4,
        ["*"] = 3,
        ["/"] = 3,
        ["+"] = 2,
        ["-"] = 2
    };

    public List<string> ConvertToOpn(List<string> tokens)
    {
        var output = new List<string>();
        var stack = new Stack<string>();
        int openParens = 0;
        int closeParens = 0;
        string previousToken = null;
        foreach (var token in tokens)
        {
            if ((IsNumber(previousToken) && token == "(") || 
                (previousToken == ")" && IsNumber(token)) || 
                (previousToken == ")" && token == "("))
            {
                Console.WriteLine("Error: Missing operator between number and the paren.");
                Environment.Exit(1);
            }
            if ("+-/*^".Contains(token))
            {
                if (previousToken == null || "+-/*^".Contains(previousToken) || previousToken == "(" ||
                    previousToken == "(")
                {
                    Console.WriteLine($"Error: {token} is not a valid operator after {previousToken}.");
                    Environment.Exit(1);
                }
            }

            if (token == ")" && previousToken == "(")
            {
                Console.WriteLine($"Error: Parens empty.");
                Environment.Exit(1);
            }

            if (token == "(" && previousToken == ")")
            {
                Console.WriteLine("Error: Wrong parens order");
                Environment.Exit(1);
            }
            previousToken = token;
        }
        foreach (var token in tokens)
        {
            if (token == "(") openParens++;
            if(token == ")") closeParens++;
        }
        if (openParens != closeParens)
        {
            Console.WriteLine("Error: number of the opening parens are not the same as the closing parens.");
            Environment.Exit(1);
        }
        foreach (var token in tokens)
        {
            if (double.TryParse(token, out _) == false && !"+-*/^()".Contains(token))
            {
                Console.WriteLine($"Error: {token} is not a number in range nor operation signs");
                Environment.Exit(1);
            }
            
            if (double.TryParse(token, out _))
            {
                output.Add(token);
            }
            
            else if ("+-/*^".Contains(token))
            {
                while (stack.Count > 0 && stack.Peek() != "(" && Precedence[stack.Peek()] >= Precedence[token])
                {
                    output.Add(stack.Pop());
                }
                stack.Push(token);
            }
            else if (token == "(")
            {
                stack.Push(token);
            }
            else if (token == ")")
            {
                while (stack.Count > 0 && stack.Peek() != "(")
                {
                    output.Add(stack.Pop());
                }

                stack.Pop();
            }
        }

        while (stack.Count > 0)
        {
            output.Add(stack.Pop());
        }
        return output;
    }

    public double Calculate(List<string> onpTokens)
    {
        var stack = new Stack<double>();

        foreach (var token in onpTokens)
        {
            if (IsNumber(token))
            {
                stack.Push(double.Parse(token));
            }
            else if ("+-*/^".Contains(token))
            {
                if (stack.Count < 2)
                {
                    Console.WriteLine("Error: Too few operands for operator.");
                    Environment.Exit(1);
                }

                double b = stack.Pop(); 
                double a = stack.Pop(); 

                double result = token switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => b == 0
                        ? throw new DivideByZeroException("Dividing by zero!")
                        : a / b,
                    "^" => Math.Pow(a, b),
                    _ => throw new InvalidOperationException($"Unknown operator: {token}")
                };

                stack.Push(result);
            }
            else
            {
                Console.WriteLine($"Error: Unknown token {token}");
                Environment.Exit(1);
            }
        }

        if (stack.Count != 1)
        {
            Console.WriteLine("Error: Invalid ONP expression – too many values on stack.");
            Environment.Exit(1);
        }

        return stack.Pop();
    }
}