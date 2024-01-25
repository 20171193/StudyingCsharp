using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11._ShortestPath
{
    internal class Training
    {
        public const int GRAPH_SIZE = 18;
        public const int INF = int.MaxValue;

        public int[,] graph;

        public Training()
        {
            graph = new int[GRAPH_SIZE, GRAPH_SIZE]
            {
                // 0  1  2  3  4  5  6  7  8  9  10  11  12  13  14  15  16  17  18 
                { INF,6,6,INF,INF,INF,INF,7,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF},       // 0
                { 6,INF,INF,INF,INF,9,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF},     // 1
                { 6,INF,INF,7,INF,INF,8,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF},       // 2
                { INF,INF,7,INF,INF,INF,8,INF,INF,INF,INF,INF,INF,INF,INF,INF,3,INF},       // 3
                { INF,INF,INF,INF,INF,2,INF,7,8,INF,INF,INF,INF,INF,INF,INF,INF,INF},       // 4
                { INF,9,INF,INF,2,INF,1,INF,INF,2,INF,INF,INF,INF,INF,INF,INF,INF},         // 5
                { INF,INF,8,8,INF,1,INF,INF,INF,2,8,INF,INF,INF,INF,INF,INF,INF},           // 6
                { 7,INF,INF,INF,7,INF,INF,INF,4,INF,INF,5,INF,INF,5,INF,INF,INF},           // 7
                { INF,INF,INF,INF,8,INF,INF,4,INF,3,INF,INF,4,INF,INF,INF,INF,INF},         // 8
                { INF,INF,INF,INF,INF,2,2,INF,3,INF,5,INF,1,INF,INF,INF,INF,INF},           // 9
                { INF,INF,INF,INF,INF,INF,8,INF,INF,5,INF,INF,INF,INF,INF,INF,INF,7},       // 10
                { INF,INF,INF,INF,INF,INF,INF,5,INF,INF,INF,INF,INF,INF,3,INF,INF,INF},     // 11
                { INF,INF,INF,INF,INF,INF,INF,INF,4,1,INF,INF,INF,INF,INF,4,7,INF},         // 12
                { INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,1,INF},   // 13
                { INF,INF,INF,INF,INF,INF,INF,5,INF,INF,INF,3,INF,INF,INF,2,INF,INF},       // 14
                { INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,4,INF,2,INF,3,6},         // 15
                { INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,INF,7,1,INF,3,INF,INF},       // 16
                { INF,INF,INF,3,INF,INF,INF,INF,INF,INF,7,INF,INF,INF,INF,6,INF,INF},       // 17
            };

            for(int i=0; i<GRAPH_SIZE; i++)
            {
                graph[i,i] = 0;
            }
            
        }

        public void DijkstraSearch(in int[,] graph, in int start, out int[] distance, out int[] path)
        {
            int size = graph.GetLength(0);
            bool[] visited = new bool[size];
            distance = new int[size];
            path = new int[size];


            for (int i = 0; i < size; i++)
            {
                distance[i] = INF;
                path[i] = -1;
            }
            distance[start] = 0;

            for (int i=0; i< size; i++)
            {
                // 가장 가까운 정점 찾기
                int nextVertex = -1;
                int minDistance = INF;
                for(int j =0; j<size; j++)
                {
                    if (visited[j]) continue;
                    if (distance[j] >= minDistance) continue;

                    nextVertex = j;
                    minDistance = distance[j];
                }

                if (nextVertex < 0) break;

                // 경유했을 때 짧아지는 경우 거리 갱신하기
                for(int j =0; j<size; j++)
                {
                    if (j == start) continue;
                    if (graph[nextVertex, j] == INF) continue;
                    if (distance[j] <= distance[nextVertex] + graph[nextVertex, j]) continue;

                    // 거리 갱신
                    distance[j] = distance[nextVertex] + graph[nextVertex, j];
                    path[j] = nextVertex;
                }

                // 방문체크
                visited[nextVertex] = true;
            }
            
        }
    }

    class MainClass
    {
        static void Main(string[] argc)
        {
            Training tr = new Training();
            tr.DijkstraSearch(tr.graph, 0, out int[] distance, out int[] path);

            int index = 0;
            Console.WriteLine("<Distance>");
            foreach(int i in distance)
            {
                Console.WriteLine($"{index++} : {i}");
            }

            index = 0;
            Console.WriteLine("<Parent>");
            foreach (int i in path)
            {
                Console.WriteLine($"{index++} : {i}");
            }
        }
    }
}
