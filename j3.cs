using System.Text.RegularExpressions;

public class J3
{
    Regex regMul = new(@"mul\(([0-9]{1,3}),([0-9]{1,3})\)|do\(\)|don't\(\)");

    string corruptedFile = File.ReadAllText("../../../inputs/j3.txt");

    public void Read()
    {
        bool isDo = true;

        MatchCollection matches = regMul.Matches(corruptedFile);

        int sum = 0;

        foreach (Match match in matches)
        {
            if (match.Value == "do()")
            {
                isDo = true;
  
            }else if (match.Value == "don't()")
            {
                isDo = false;
            }
            else
            {
                if (isDo)
                {
                    int a = int.Parse(match.Groups[1].Value);
                    int b = int.Parse(match.Groups[2].Value);

                    sum += a * b;
                }
            }
        }

        Console.WriteLine(sum);

    }

}