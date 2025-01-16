public class J6
{
    static string[] grid = File.ReadAllLines("../../../inputs/j6.txt");
    int width = grid[0].Length;
    int height = grid.Length;
    char obstacle = '#';

    HashSet<(int x, int y)> visitedPositions = [];

    public (int x, int y, char dir) InitPosition()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                char current = grid[i][j];
                if ((current == '<') || (current == '>') || (current == '^') || (current == 'v'))
                {
                    return (i, j, current);
                }
            }
        }
        throw new InvalidOperationException("Initial position not found.");
    }
    public void Read()
    {
        var (x, y, dir) = InitPosition();
        visitedPositions.Add((x, y));

        while (x >= 0 && x < height && y >= 0 && y < width)
        {
            int newX = x, newY = y;
            switch (dir)
            {
                case '^': newX--; break;
                case 'v': newX++; break;
                case '<': newY--; break;
                case '>': newY++; break;
            }

            if (newX <0 || newX >= height || newY <0 || newY >= width)
            {
                break;
            }

            if (grid[newX][newY] == obstacle)
            {
                dir = dir switch
                {
                    '^' => '>',
                    '>' => 'v',
                    'v' => '<',
                    '<' => '^',
                    _ => dir
                };
            }
            else
            {
                x = newX;
                y = newY;
                visitedPositions.Add((x, y));
            }
        }
        Console.WriteLine(visitedPositions.Count);
    }
}