
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    internal class Program
    {
        private static List<SearchMethod> s_searchMethods;
        static void Main(string[] args)
        {
            // Create list of search methods
            InitSearchMethods();

            // args contains:
            //  [0] is the name of the map file
            //  [1] is the method used to find the path
            //
            //  [2] is optional extras
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: RobotNavigation.exe <file name> <search method> <optional>");
                Console.WriteLine("Search methods:");
                foreach (SearchMethod method in s_searchMethods)
                {
                    Console.WriteLine(String.Format("\t{0} ({1})", method.Code, method.Description));
                }
                Console.WriteLine("<optional> can be left empty, or may be set to:");
                Console.WriteLine("\tC - Use Completionist (False by default)");
                Console.WriteLine("\tJ - Use Jumping (False by default)");
                Environment.Exit(0);
            }

            // create map from file
            Map environment = ReadMapFromFile(args[0]);

            // get search method
            SearchMethod searchMethod = GetSearchMethod(args[1]);

            // check if theres a third argument
            if(args.Length > 2)
            {
                if (args[2].Contains("C"))
                    searchMethod.Completionist = true;
                if (args[2].Contains("J"))
                    searchMethod.UseJumping = true;
            }

            // Initialise search method
            searchMethod.Initialise(environment);

            // find path
            List<Node> solution = searchMethod.FindPath();

            if(solution != null )
            {
                PrintList(solution);
            }
            else
            {
                Console.WriteLine("No solution found.");
            }
        }

        private static void InitSearchMethods()
        {
            s_searchMethods = new List<SearchMethod>();
            s_searchMethods.Add(new DepthFirstSearch());
            s_searchMethods.Add(new BreadthFirstSearch());
            s_searchMethods.Add(new GreedyBestFirstSearch());
            s_searchMethods.Add(new AStarSearch());
            s_searchMethods.Add(new LowestCostFirstSearch());
        }

        private static SearchMethod GetSearchMethod(string code)
        {
            SearchMethod method = s_searchMethods.Find(m => m.Code == code);
            // check method has been found
            if (method == null)
            {
                Console.WriteLine("Error: Search method not found.");
                Console.WriteLine("Search methods:");
                foreach (SearchMethod m in s_searchMethods)
                {
                    Console.WriteLine(String.Format("\t{0} ({1})", m.Code, m.Description));
                }
                Environment.Exit(0);
            }
            return method;
        }

        private static Map ReadMapFromFile(string fileName)
        {
            try
            {
                // char array of characters to remove from begining and end of lines
                char[] trimChars = { ' ', '(', ')', '[', ']' };
                string[] lines = null;

                lines = System.IO.File.ReadAllLines(fileName);
                

                // the first line contains the map size in the format [heigh,width]
                string[] size = lines[0].Trim(trimChars).Split(',');
                int mapWidth = int.Parse(size[1]);
                int mapHeight = int.Parse(size[0]);

                // the second line contains the start position in the format (x,y)
                string[] start = lines[1].Trim(trimChars).Split(',');
                int startX = int.Parse(start[0]);
                int startY = int.Parse(start[1]);
                Cell cellStart = new Cell(startX, startY, CellType.START);

                // the third line contains the end position(s) in the format (x,y) | (x,y)
                string[] ends = lines[2].Split('|');
                List<Cell> cellEnds = new List<Cell>();
                foreach (string end in ends)
                {
                    string[] endPos = end.Trim(trimChars).Split(',');
                    int endX = int.Parse(endPos[0]);
                    int endY = int.Parse(endPos[1]);
                    cellEnds.Add(new Cell(endX, endY, CellType.END));
                }

                // the remaining lines contain the walls in the format (startX, startY, width, height)
                List<Cell> cellWalls = new List<Cell>();
                for (int i = 3; i < lines.Length; i++)
                {
                    string[] wall = lines[i].Trim(trimChars).Split(',');
                    int wallX = int.Parse(wall[0]);
                    int wallY = int.Parse(wall[1]);
                    int wallWidth = int.Parse(wall[2]);
                    int wallHeight = int.Parse(wall[3]);
                    for (int x = wallX; x < wallX + wallWidth; x++)
                    {
                        for (int y = wallY; y < wallY + wallHeight; y++)
                        {
                            cellWalls.Add(new Cell(x, y, CellType.WALL));
                        }
                    }
                }

                Map lMap = new Map(width: mapWidth, height: mapHeight, start: cellStart, ends: cellEnds, walls: cellWalls);
                return lMap;
            }
            catch(FileNotFoundException)
            {
                //The file didn't exist, show an error
                Console.WriteLine("Error: File \"" + fileName + "\" not found.");
                Console.WriteLine("Please check the path to the file.");
                Environment.Exit(0);
            }
            catch (IOException)
            {
                //There was an IO error, show and error message
                Console.WriteLine("Error in reading \"" + fileName + "\". Try closing it and programs that may be accessing it.");
                Console.WriteLine("If you're accessing this file over a network, try making a local copy.");
                Environment.Exit(0);
            }

            return null;
        }

        private static void PrintList(List<Node> l)
        {
            string s = string.Empty;
            int temp;
            foreach (Node i in l)
            {
                if (i.Connection != null)
                {
                    s += i.Connection.Direction.ToString().ToLower();
                    if (i.Connection.Cost > 1)
                    {
                        temp = (int)Math.Sqrt(i.Connection.Cost);
                        s += "(" + temp.ToString() + ")";
                    }
                    s += ", ";
                }
            }
            s = s.TrimEnd(' ').TrimEnd(',');
            Console.WriteLine(s);
        }
    }
}
