using System.Linq.Expressions;

public class J2
{
    string line = "";

    private int[] Parse(string level)
    {
        return Array.ConvertAll(level.Split(" "), int.Parse);
    }

    private static bool IsStrictlyIncreasing(int[] array)
    {
        return array.Zip(array.Skip(1), (a, b) => a < b).All(x => x);
    }

    private static bool IsStrictlyDecreasing(int[] array)
    {
        return array.Zip(array.Skip(1), (a, b) => a > b).All(x => x);
    }

    private bool isLevelIsSafe(int[] level)
    {
        bool isSafe = true;

        if (!(IsStrictlyIncreasing(level) || IsStrictlyDecreasing(level)))
        {
            isSafe = false;
        }

        if(isSafe)
        {
            for(int i = 0; i < level.Length - 1; i++)
            {
                var abs = Math.Abs(level[i] - level[i + 1]);
                if(abs < 1 || abs > 3)
                {
                    isSafe = false;
                }
            }
        }

        return isSafe;
    }
    public void Read()
    {
        try
        {
            StreamReader sr = new("../../../inputs/j2.txt");
            int cpt = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var level = this.Parse(line);
                    if (isLevelIsSafe(level))
                    {
                        cpt++;
                    } else
                    {
                        for(int i = 0; i < level.Length; i++)
                        {
                            var extractedList = level.Where((_, index) => index != i).ToArray();
                            if(isLevelIsSafe(extractedList))
                            {
                                cpt++;
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            Console.WriteLine(cpt);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
}