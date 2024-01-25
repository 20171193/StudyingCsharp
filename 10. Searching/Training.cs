using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace _10._Searching
{
    internal class Training
    {
        class Graph
        {
            public const int INF = int.MaxValue;

            // 1. 양방향 연결그래프
            public bool[,] graph1 = new bool[8, 8]
            {
                { false, false, false, true, true, false, false, false},
                { false, false, false, true, false, true, true, false},
                { false, false, false, false, false, false, true, false},
                { true, true, false, false, false, true, false, true},
                { true, false, false, false, false, false, true, false},
                { false, true, false, true, false, false, true, true},
                { false, true, true, false, true, true, false, true},
                { false, false, false, true, false, true, true, false}
            };

            // 2. 단방향 연결그래프
            public bool[,] graph2 = new bool[8, 8]
            {
                { false, false, false, false, false, false, false, false},
                { false, false, false, false, false, false, false, false},
                { false, false, false, false, true, true, false, false},
                { false, true, false, false, false, true, false, true},
                { false, false, false, false, false, false, false, false},
                { false, true, false, false, false, false, false, false},
                { false, false, true, false, false, true, false, false},
                { false, false, false, false, false, false, true, false}
            };

            // 3. 양방향 가중치 그래프
            public int[,] graph3 = new int[8, 8]
            {
                { INF, 4, INF, INF, 6, INF, INF,INF},
                { 4, INF, 5, 4, INF, 8, 2,INF},
                { INF, 5, INF, INF, 9, INF, INF,INF},
                { INF, 4, INF, INF, INF, INF, INF,INF},
                { 6, INF, 9, INF, INF, INF, 5,INF},
                { INF, 8, INF, INF, INF, INF, INF,1},
                { INF, 2, INF, INF, 5, INF, INF,INF},
                { INF, INF, INF, INF, INF, 1, INF,INF}
            };
        }
        class GraphSearch
        {
            public void DFS(bool[,] graph, bool[] visited, int prev, int start, int[] parents)
            {
                if (visited[start]) return;

                visited[start] = true;
                if (prev >= 0) { parents[start] = prev; }

                for (int i = 0; i < graph.GetLength(0); i++)
                {
                    if (!graph[start, i]) continue;
                    if (visited[i]) continue;

                    DFS(graph, visited, start, i, parents);
                }
            }


            public void BFS(bool[,] graph, bool[] visited, int start, int[] parents)
            {
                Queue<int> q = new Queue<int>();
                q.Enqueue(start);
                visited[start] = true;

                while (q.Count > 0)
                {
                    int temp = q.Dequeue();
                    for (int i = 0; i < graph.GetLength(0); i++)
                    {
                        if (!graph[temp, i]) continue;
                        if (visited[i]) continue;

                        parents[i] = temp;
                        visited[i] = true;
                        q.Enqueue(i);
                    }

                }
            }
        }
        class Program
        {
            void Main6()
            {
                Graph graph = new Graph();
                GraphSearch graphSearch = new GraphSearch();

                int size = graph.graph1.GetLength(0);
                int[] dParents = new int[size];
                graphSearch.DFS(graph.graph1, new bool[size], -1, 0, dParents);
                foreach (int pr in dParents)
                {
                    Console.Write($"{pr} ");
                }

                Console.WriteLine();

                bool[] visited = new bool[size];
                int[] bParents = new int[size];
                graphSearch.BFS(graph.graph1, new bool[size], 0, bParents);

                foreach (int pr in bParents)
                {
                    Console.Write($"{pr} ");
                }

                Console.WriteLine();
            }
        }

        class BOJ1388
        {
            static int n = 0, m = 0, ret = 0;
            static string[] arr;
            static bool[,] visited;

            static void DFS(int y, int x, char c)
            {
                if (x >= m || y >= n) return;
                if (visited[y, x]) return;
                if (arr[y][x] != c) return;

                visited[y, x] = true;

                if (c == '-')
                    DFS(y, x + 1, c);
                else // c == '|'
                    DFS(y + 1, x, c);
            }

            static void Main(string[] argc)
            {
                string[] temp = Console.ReadLine().Split(' ');
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);

                arr = new string[n];
                visited = new bool[n, m];

                for(int i =0; i<n; i++)
                {
                    arr[i] = Console.ReadLine();
                }

                int answer = 0;
                for(int i=0; i<n; i++)
                {
                    for(int j =0; j<m; j++)
                    {
                        if(!visited[i, j])
                        {
                            answer++;
                            DFS(i, j, arr[i][j]);
                        }
                    }
                }
                Console.WriteLine(answer);
            }
        }
    }
}

