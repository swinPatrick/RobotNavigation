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
            // get a file from the user using console, and check the file is a valid map file
            string fileName = "";
            bool validFile = false;
            while (!validFile)
            {
                Console.WriteLine("Please enter the name of the map file you wish to use:");
                fileName = Console.ReadLine().Trim('"');
                if (fileName.Length > 4 && fileName.Substring(fileName.Length - 4, 4) == ".txt")
                {
                    validFile = true;
                    try
                    {

                        map environment = readMap.readMapFromFile(fileName);

                        environment.printMap();
                    }
                    catch (IOException e)
                    {
                        // print error to console and exit app
                        Console.WriteLine(e.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Invalid file name, please try again.");
                }
            }


            Console.ReadKey();
        }
    }
}
