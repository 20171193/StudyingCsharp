using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12._PathFinding
{

    internal class Training
    {
        
        char[,] tileMap2 = new char[9, 9]
        {
            { '■', '■', '■', '■', '■', '■', '■', '■', '■' },
            { '■', 'S', '■', '■', ' ', ' ', '■', '■', '■' },
            { '■', '*', '■', '■', ' ', '■', '■', ' ', '■' },
            { '■', '*', '■', '■', ' ', '■', '■', ' ', '■' },
            { '■', '*', '■', '*', '*', '*', '*', '*', '■' },
            { '■', '*', '■', '*', '■', '■', '■', '*', '■' },
            { '■', '*', '■', '*', '■', '■', '■', '*', '■' },
            { '■', '*', '*', '*', '■', '■', '■', 'E', '■' },
            { '■', '■', '■', '■', '■', '■', '■', '■', '■' },
        };
        public struct Point
        {
            public int x;
            public int y;
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        static Point[] Direction =
        {
            new Point(0, 1),
            new Point(0, -1),
            new Point(-1, 0),
            new Point(1, 0),
            new Point(-1, 1),
            new Point(-1, -1),
            new Point(1, 1),
            new Point(1, -1)
        };

        class Astar
        {
            const int CostStraight = 10;    //   직선 이동거리
            const int CostDiagonal = 14;    // 대각선 이동거리

            public static bool PathFinding(bool[,] tileMap, Point start, Point end, out List<Point> path)
            {
                int xSize = tileMap.GetLength(0);
                int ySize = tileMap.GetLength(1);

                ASNode[,] nodes = new ASNode[ySize, xSize];
                bool[,] visited = new bool[ySize, xSize];
                PriorityQueue<ASNode, int> nextPointPQ = new PriorityQueue<ASNode, int>();

                ASNode startNode = new ASNode(start, new Point(), 0, Heuristic(start, end));
                nodes[startNode.pos.y, startNode.pos.x] = startNode;
                nextPointPQ.Enqueue(startNode);
                
                while(nextPointPQ.Count > 0)
                {
                    ASNode nextNode = nextPointPQ.Dequeue();
                    visited[nextNode.pos.y, nextNode.pos.x] = true;

                    // 도착지 판단
                    if(nextNode.pos.x == end.x && nextNode.pos.y == end.y)
                    {
                        path = new List<Point>();

                        Point point = end;
                        while (point.x != start.x && point.y != start.y)
                        {
                            path.Add(point);
                            point = nodes[point.y, point.x].parent;
                        }
                        path.Add(start);
                        path.Reverse();
                        return true;
                    }

                    // 탐색
                    for(int i=0; i<Direction.Length; i++)
                    {
                        int x = nextNode.pos.x + Direction[i].x;
                        int y = nextNode.pos.y + Direction[i].y;

                        if (x < 0 || x >= xSize || y < 0 || y >= ySize) continue;
                        if (!tileMap[y, x]) continue;
                        if (visited[y, x]) continue;
                        // 대각선으로 이동 시 이동이 불가능한 지역일 때
                        //  ex) 좌상 이동 시 좌측 또는 위측이 이동이 불가능한 지역일 경우
                        if (i >= 4 && !tileMap[y, nextNode.pos.x] && !tileMap[nextNode.point.y, x]) continue;

                        int g = nextNode.g + ((nextNode.pos.x == x || nextNode.pos.y == y) ? CostStraight : CostDiagonal);
                        int h = Heuristic(new Point(x, y), end);

                        ASNode newNode = new ASNode(new Point(x, y), nextNode.pos, g, h);

                        if (nodes[y,x] == null || nodes[y,x].f > newNode.f)
                        {
                            nodes[y, x] = newNode;
                            nextPointPQ.Enqueue(newNode, newNode.f);
                        }
                    }
                }
                path = null;
                return false;
            }
            public static int Heuristic(Point start, Point end)
            {
                int xSize = Math.Abs(start.x - end.x);
                int ySize = Math.Abs(start.y - end.y);

                int starightCount = Math.Abs(xSize - ySize);
                int diagonalCount = Math.Max(xSize, ySize) - starightCount;
                return CostStraight * starightCount + CostDiagonal * diagonalCount;
            }

            public class ASNode
            {
                public Point pos;       // 해당 정점의 위치
                public Point parent;    // 해당 정점을 탐색한 정점의 위치

                public int f;
                public int g;
                public int h;

                public ASNode(Point pos, Point parent, int g, int h)
                {
                    this.pos = pos;
                    this.parent = parent;
                    this.f = g + h;
                    this.g = g;
                    this.h = h;
                }
            }
        }
    }


}

