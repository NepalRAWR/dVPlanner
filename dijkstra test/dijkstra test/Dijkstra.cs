using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace dijkstra_test
{
    class DijkstraPath
    {
        public string node { get; set; }
        public string neighborNode { get; set; }
        public int distance { get; set; }

        public DijkstraPath(string Node,string PreviousNode,int Distance)
        {
            node = Node;
            neighborNode = PreviousNode;
            distance = Distance;
            
        }
        
        
    }
    static class DijkstraReader
    {

        static public string readNode(string line)
        {
            line = line.Replace(" ", "");
            line = line.Replace("\t", "");
            int index = line.LastIndexOf(",");
            if (index > 0)
                line = line.Substring(0, index);
            return line;
        }

        static public string readPrevNode(string line)
        {
            line = line.Replace(" ", "");
            line = line.Replace("\t", "");
            int index = line.LastIndexOf(",") + 1;
            int index2 = line.LastIndexOf(":") - index;
            if (index > 0 && index2 > 0)
                line = line.Substring(index, index2);
            return line;
        }

        static public int readDistance(string line)
        {
            line = line.Replace(" ", "");
            line = line.Replace("\t", "");
            int index = line.LastIndexOf(":") + 1;
            if (index > 0)
                line = line.Substring(index);
            return int.Parse(line);
        }
    }   

    class Dijkstra
        {
            DijkstraPath[] Way;
            string[] Q;
            public Dijkstra(DijkstraPath[] Graph, string Start)
            {
                init(Graph, Start);
                while (true)
                {
                    string currentNode;
                    int minDist = int.MaxValue;
                    int found = -1;
                    for (int i = 0; i < Q.Length; i++)
                    {
                        if (Q[i] != null)
                        {
                            if (Way[i].distance < minDist)
                            {
                                minDist = Way[i].distance;
                                found = i;
                            }
                        }
                    }
                    if (found == -1)
                        return;
                    currentNode = Q[found];
                    Q[found] = null;
                    foreach(DijkstraPath g in Graph)
                    {
                        if(g.node == currentNode)
                        {
                            string n = g.neighborNode;
                            bool inQ = false;
                            foreach(string q in Q)
                            {
                                if (q == n)
                                    inQ = true;
                            }
                            if (inQ)
                                distUpdate(currentNode, n, g.distance);
                        }
                    }
                }
            }
            void init(DijkstraPath[] g, string s)
            {
                int i = 0;
                string[] nodes = new string[g.Length];
                foreach (DijkstraPath node in g)
                {
                    bool found = false;
                    foreach (string n in nodes)
                    {
                        if (n == node.node)
                            found = true;
                    }
                    if (!found)
                    {
                        nodes[i] = node.node;
                        i++;
                    }
                }
                
                Way = new DijkstraPath[i];
                Q = new string[i];
                for (i = 0;i<Q.Length; i++)
                {
                    Way[i] = new DijkstraPath(nodes[i], null, (nodes[i] == s) ? 0 : int.MaxValue);
                    Q[i] = nodes[i];
                   
                } 
            }
            void distUpdate(string u, string v, int d)
            {
                int alt;
                int WayU = 0;
                int WayV = 0;
                DijkstraPath nb = null;
                foreach(DijkstraPath p in Way)
                {
                    if(p.node == u)
                    {
                        WayU = p.distance;
                    }
                    if (p.node == v)
                    {
                        WayV = p.distance;
                        nb = p;
                    }
                    
                }Debug.Assert(nb != null);
                alt = WayU + d;
                if (alt < WayV)
                {
                    nb.distance = alt;
                    nb.neighborNode = u;
                }
            }
            public int shortestDistance(string dest)
            {
                foreach (DijkstraPath p in Way)
                    if (p.node == dest)
                        return p.distance;
                return -1;
            }
        }

}
