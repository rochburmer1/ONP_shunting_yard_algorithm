namespace ONP_shunting_yard_algorithm;

public class Onp
{
    public string UserInput()
    {
        Console.WriteLine("Please put in your mathematical expression: ");
        return Console.ReadLine();
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
  
        foreach (var token in tokens)
        {
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
}