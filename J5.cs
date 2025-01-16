public class J5
{
    string line = "";
    
    List<List<int>> updates = new List<List<int>>();
    List<(int x, int y)> rules = new List<(int x, int y)>();

    public bool isValid(List<int> update)
    {
        foreach ((int x, int y) in rules)
        {
            if (update.Contains(x) && update.Contains(y))
            {
                if (update.IndexOf(x) >= update.IndexOf(y))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void Read()
    {
        bool isRule = true;
        
        StreamReader reader = new StreamReader("../../../inputs/J5.txt");
        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isRule = false;
                continue;
            }

            if (isRule)
            {
                var rule = line.Split("|");
                rules.Add((int.Parse(rule[0]), int.Parse(rule[1])));
            }
            else
            {
                var update = line.Split(",").Select(int.Parse).ToList();
                updates.Add(update);
            }

        }


        int sum = updates.Where(update => isValid(update))
                         .Select(update =>
                         {
                            int middleIndex = update.Count / 2;
                            return update[middleIndex];
                         })
                         .Sum();

        Console.WriteLine(sum);

    }
}