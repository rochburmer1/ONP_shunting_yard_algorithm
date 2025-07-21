namespace ONP_shunting_yard_algorithm;

public class ONP
{
    public string UserInput()
    {
        Console.WriteLine("Please put in your mathematical expression: ");
        return Console.ReadLine();
    }

    public List<string> Tokenize(string input)
    {
            List<string> Tokens = new List<string>();
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
                        Tokens.Add(number);
                        number = "";
                    }

                    if ("+-*/^()".Contains(c))
                    {
                        Tokens.Add(c.ToString());
                    }
                }
            }

            if (number != "")
            {
                Tokens.Add(number);
            }
            return Tokens;
    }
}