using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dijkstra_test
{
    class Program
    {
        static DijkstraPath[] DijkstraList;
        static string[] DijkstraNodeList;
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"D:\Henry\Desktop/dVDijkstra.txt");
            DijkstraList = new DijkstraPath[lines.Length * 2];
            DijkstraNodeList = new string[lines.Length * 2];
            int i = 0;
            foreach(string line in lines)
            {
                
                DijkstraList[i] = new DijkstraPath(DijkstraReader.readNode(line), DijkstraReader.readPrevNode(line), DijkstraReader.readDistance(line));
                i++;
                DijkstraList[i] = new DijkstraPath(DijkstraReader.readPrevNode(line), DijkstraReader.readNode(line), DijkstraReader.readDistance(line));
                /*a.setNext(DijkstraList);
                DijkstraList = a;*/
                i++;
            }
            Dijkstra path = new Dijkstra(DijkstraList, "KEO");
            Console.WriteLine(path.shortestDistance("MinmusIC"));
            Console.ReadKey();
            
            
           
        }
    }
}
