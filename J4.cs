using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class J4
{
    static string[] grid = File.ReadAllLines("../../../inputs/j4.txt");
    int width = grid[0].Length;
    int height = grid.Length;
    string xmas = "XMAS";

    public int DoTile(int i, int j)
    {

        int count = 0;
        int[] dx = [-1, -1, -1, 0, 0, 1, 1, 1];
        int[] dy = [-1, 0, 1, -1, 1, -1, 0, 1];
            
        for(int dir = 0; dir < 8; dir ++)
        {
            int x = i, y = j;
            int k;

            for(k = 0; k < xmas.Length; k++)
            {
                if(x >= 0 && x < height && y >= 0 && y <width)
                {
                    if(grid[x][y] != xmas[k])
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }

                x += dx[dir];
                y += dy[dir];
            }
            if(k == xmas.Length)
            {
                count++;
            }
        }
            
        return count ;
    }

    public int DoLittleTile(int i, int j)
    {
        int count = 0;

        if (grid[i][j] != 'A')
            return 0;

        if (i - 1 >= 0 && i + 1 < height && j - 1 >= 0 && j + 1 < width)
        {
            string firstBranch = $"{grid[i - 1][j + 1]}{grid[i][j]}{grid[i + 1][j - 1]}";
            string secondBranch = $"{grid[i - 1][j - 1]}{grid[i][j]}{grid[i + 1][j + 1]}";

            if ((firstBranch == "MAS" || firstBranch == "SAM")
                && (secondBranch == "MAS" || secondBranch == "SAM"))
            {
                count++;
            }
        }

        return count;
    }

    public void Read()
    {
        int xmasSum = 0;
        int masSum = 0;

        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                if(grid[i][j] == 'X')
                {
                    xmasSum += DoTile(i, j);

                }else if(grid[i][j] == 'A')
                {
                    masSum += DoLittleTile(i, j);
                }
            }
        }
            Console.WriteLine(xmasSum);
            Console.WriteLine(masSum);
        }
    }
