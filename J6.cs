public class J6
{
    static string[] grid = File.ReadAllLines("../../../inputs/j6.txt");
    int width = grid[0].Length;
    int height = grid.Length;
    char obstacle = '#';

    public (int x, int y, char dir) InitPosition()
    {
        for (int i =0; i < height; i++)
        {
            for (int j =0; j < width; j++)
            {
                char current = grid[i][j];
                if (current == '<' || current == '>' || current == '^' || current == 'v')
                {
                    return (i, j, current);
                }
            }
        }
        throw new InvalidOperationException("Initial position not found.");
    }

    public bool SimulateWithObstruction(int x, int y)
    {
        var (posX, posY, direction) = InitPosition();
        var visitedStates = new HashSet<(int, int, char)>();

        string originalGridLine = grid[x];

        grid[x] = grid[x].Remove(y, 1).Insert(y, "O");

        while (true)
        {
            if (visitedStates.Contains((posX, posY, direction)))
            {
                grid[x] = originalGridLine;
                return true;
            }

            visitedStates.Add((posX, posY, direction));

            int newX = posX, newY = posY;

            switch (direction)
            {
                case '^': newX--; break;
                case 'v': newX++; break;
                case '<': newY--; break;
                case '>': newY++; break;
            }

            if (newX <0 || newX >= height || newY <0 || newY >= width)
            {
                grid[x] = originalGridLine;
                return false;
            }

            if (grid[newX][newY] == obstacle || grid[newX][newY] == 'O')
            {
                direction = direction switch
                {
                    '^' => '>',
                    '>' => 'v',
                    'v' => '<',
                    '<' => '^',
                    _ => direction
                };
            }
            else
            {
                posX = newX;
                posY = newY;
            }
        }
    }

    public void Read()
    {
        int cpt =0;
        var initialVisitedState = new HashSet<(int x, int y)>();
        var (startX, startY, _) = InitPosition();
        initialVisitedState.Add((startX, startY));

        for (int i =0; i < height; i++)
        {
            for (int j =0; j < width; j++)
            {
                if (grid[i][j] == '.' && !initialVisitedState.Contains((i, j)))
                {
                    if (SimulateWithObstruction(i, j))
                    {
                        cpt++;
                    }
                }
            }
        }

        Console.WriteLine($"Nombre de positions possibles pour une boucle: {cpt}");
    }
}