
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            // args[0] is the name of the map file
            // args[1] is the method used to find the path

            if (args.Length == 0)
            {
                args = new string[2];
                Console.WriteLine("Please enter the name of the map file you wish to use:");
                args[0] = Console.ReadLine().Trim('"');

                Console.WriteLine("Please enter the name of the search method you wish to use:");
                args[1] = Console.ReadLine().Trim('"');
            }
            // check the file is a valid map file
            try
            {
                // create map from file
                Map environment = readMap.readMapFromFile(args[0]);

                // print map to console
                environment.printMap();

                // use search to find path
                DepthFirstSearch dfs = new DepthFirstSearch(environment);
                dfs.FindPath();
                Console.WriteLine(dfs.Path());
            }
            catch (IOException e)
            {
                // print error to console and exit app
                Console.WriteLine(e.ToString());
            }

            Console.ReadKey();
        }
    }
}
