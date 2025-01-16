public class J5
{
    string line = "";
    
    List<List<int>> updates = new List<List<int>>();
    List<(int x, int y)> rules = new List<(int x, int y)>();

    public bool IsValid(List<int> update)
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

    public List<int> CorrectOrder(List<int> update)
    {
        var graph = new Dictionary<int, List<int>>();
        var indegree = new Dictionary<int, int>();

        foreach (int page in update)
        {
            graph[page] = new List<int>();
            indegree[page] =0;
        }

        foreach ((int x, int y) in rules)
        {
            if (update.Contains(x) && update.Contains(y))
            {
                graph[x].Add(y); 
                indegree[y]++; 
            }
        }

        var queue = new Queue<int>(indegree.Where(kv => kv.Value ==0).Select(kv => kv.Key));

        var ordered = new List<int>();
        while (queue.Count >0)
        {
            int page = queue.Dequeue();
            ordered.Add(page);

            foreach (int neighbor in graph[page])
            {
                indegree[neighbor]--;
                if (indegree[neighbor] ==0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        return ordered;
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

        int sum = updates.Where(update => !IsValid(update))
                         .Select(update =>
                         {
                            var correctedUpdate = CorrectOrder(update);
                            int middleIndex = correctedUpdate.Count /2;
                            return correctedUpdate[middleIndex];
                         })
                         .Sum();

        Console.WriteLine(sum);

    }
}