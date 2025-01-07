public class J1
{
    string line = "";
    List<long> list1 = new();
    List<long> list2 = new();

    private (string, string) Parse(string line)
    {
        string l1 = line.Split("   ")[0];
        string l2 = line.Split("   ")[1];
        return (l1, l2);
    }

    private long ComputeDist(long[] a, long[] b)
    {
        long sum = 0;
        Console.WriteLine(a.Length);
        for (int i = 0; i < a.Length; i++)
            sum += Math.Abs(a[i] - b[i]);
        
        return sum;
    }

    private long ComputeSimilarity(List<long> a, List<long> b)
    {
        long sum = 0;
        foreach (var x in a)
        {
            int cpt = 0;
            foreach (var y in b)
            {
                if(x == y) cpt +=1;
            }
            sum += x*cpt;
        }

        return sum;
    }

    public void Read()
    {
        try
        {
            StreamReader sr = new("../../../inputs/j1.txt");
            
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var (sku, ski) = this.Parse(line);

                    list1.Add(Convert.ToInt64(sku));
                    list2.Add(Convert.ToInt64(ski));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
            }
            sr.Close();

            list1.Sort();
            list2.Sort();

            Console.WriteLine(this.ComputeDist(list1.ToArray(), list2.ToArray()));   
            Console.WriteLine(this.ComputeSimilarity(list1, list2));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

}
