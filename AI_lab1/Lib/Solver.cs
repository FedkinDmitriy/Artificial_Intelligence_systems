using System.Collections.Generic;

namespace AI_lab1.Lib
{
    public class Solver
    {
        // Возможные ходы коня
        private static readonly (int dx, int dy)[] KnightMoves =
        {
            (2, 1), (1, 2), (-1, 2), (-2, 1),
            (-2, -1), (-1, -2), (1, -2), (2, -1)
        };
        // Предвычисленная матрица минимальных ходов коня между любыми двумя клетками
        private static readonly int[,] KnightDistanceTable = {   
            { 0, 3, 2, 3, 2, 3, 4, 5 },
            { 3, 2, 1, 2, 3, 4, 3, 4 },
            { 2, 1, 4, 3, 2, 3, 4, 5 },
            { 3, 2, 3, 2, 3, 4, 3, 4 },
            { 2, 3, 2, 3, 4, 3, 4, 5 },
            { 3, 4, 3, 4, 3, 4, 5, 4 },
            { 4, 3, 4, 3, 4, 5, 4, 5 },
            { 5, 4, 5, 4, 5, 4, 5, 6 } };

        private const int SIZE_BOARD = 8;
        private States[,] grid;

        public int Iterations { get; private set; } // сколько раз мы берём узел из очереди (Временная сложность)
        public int GeneratedStates { get; private set; } // сколько новых узлов реально добавлено в O (Временная сложность)
        public int MaxOpenCount { get; set; } // Максимальное количество узлов в памяти (Пространственная сложность)

        public Solver(States[,] gridStates)
        {
            grid = gridStates;
        }

        /// <summary>
        /// Поиск пути BFS
        /// </summary>
        public List<Node>? FindPathBFS((int x, int y) start, (int x, int y) target)
        {
            // O – открытые
            var O = new Queue<Node>();
            // C – закрытые
            var C = new HashSet<(int, int)>();

            Iterations = 0; 
            GeneratedStates = 0;
            MaxOpenCount = 0;

            O.Enqueue(new Node(start.x, start.y));

            while (O.Count > 0)
            {
                if (O.Count + C.Count > MaxOpenCount) MaxOpenCount = O.Count + C.Count;


                // x := first(O)
                var current = O.Dequeue();

                // если цель – выходим
                if ((current.X, current.Y) == target) return ReconstructPath(current);

                // проверяем, не обрабатывали ли уже этот узел
                if (C.Contains((current.X, current.Y))) continue;

                // переносим из O в C
                C.Add((current.X, current.Y));

                // P: раскрытие x
                foreach (var (dx, dy) in KnightMoves)
                {
                
                    Iterations++;
                    int nx = current.X + dx;
                    int ny = current.Y + dy;

                    if (nx >= 0 && ny >= 0 && nx < SIZE_BOARD && ny < SIZE_BOARD && grid[nx, ny] != States.Burning && grid[nx,ny] != States.Visited && !C.Contains((nx, ny)))
                    {
                        O.Enqueue(new Node(nx, ny, current));
                        GeneratedStates++;
                    }
                }
            }

            return null; // путь не найден
        }

        /// <summary>
        /// Восстановление пути из родительских ссылок
        /// </summary>
        private List<Node> ReconstructPath(Node node)
        {
            var path = new List<Node>();
            while (node != null)
            {
                path.Add(node);
                node = node.Parent!;
            }
            path.Reverse();
            return path;
        }

        #region Second Part of First LabWork
        /// <summary>
        /// DFS с ограничением глубины
        /// </summary>
        public List<Node>? FindPathDFS_Limited((int x, int y) start, (int x, int y) target, int maxDepth,
            out int SolverIterations, out int SolverGeneratedStates, out int DFS_MaxOpenCount)
        {
            var O = new Stack<(Node node, int depth)>();
            var C = new HashSet<(int, int)>();

            SolverIterations = 0;
            SolverGeneratedStates = 0;
            DFS_MaxOpenCount = 0;

            O.Push((new Node(start.x, start.y), 0));

            while (O.Count > 0)
            {
                
                if (O.Count + C.Count > DFS_MaxOpenCount) DFS_MaxOpenCount = O.Count + C.Count;

                var (current, depth) = O.Pop();

                if ((current.X, current.Y) == target) return ReconstructPath(current);

                if (C.Contains((current.X, current.Y)) || depth >= maxDepth) continue; //если достигнут предел глубины, не раскрываем узел и не добавляем его в C

                C.Add((current.X, current.Y));

                foreach (var (dx, dy) in KnightMoves)
                {
                    SolverIterations++;
                    int nx = current.X + dx;
                    int ny = current.Y + dy;

                    if (nx >= 0 && ny >= 0 && nx < SIZE_BOARD && ny < SIZE_BOARD && grid[nx, ny] != States.Burning && grid[nx, ny] != States.Visited && !C.Contains((nx, ny)))
                    {
                        O.Push((new Node(nx, ny, current), depth + 1));
                        SolverGeneratedStates++;
                    }
                }
            }

            return null;
        }
        /// <summary>
        /// DFS с итеративным ограничением глубины (IDS - Iterative Deepening Search)
        /// </summary>
        public List<Node>? FindPathIterativeDeepening((int x, int y) start, (int x, int y) target, int maxDepth)
        {
            Iterations = 0;
            GeneratedStates = 0;
            MaxOpenCount = 0;

            for (int depth = 1; depth <= maxDepth; depth++)
            {
                int iter, genStates, maxOpen;

                var result = FindPathDFS_Limited(start, target, depth, out iter, out genStates, out maxOpen);

                Iterations += iter;
                GeneratedStates += genStates;
                if (maxOpen > MaxOpenCount) MaxOpenCount = maxOpen;

                if (result != null) return result;
            }
            return null;
        }

        #endregion Second Part of First LabWork

        #region Third Part of LabWork

        /// <summary>
        /// A* поиск пути коня
        /// </summary>
        public List<Node>? FindPathAStar((int x, int y) start, (int x, int y) target, Func<int,int,int,int,int> Heuristic)
        {
            Iterations = 0;
            GeneratedStates = 0;
            MaxOpenCount = 0;

            var closed = new HashSet<(int, int)>();

            PriorityQueue<Node, int>? openPQ = new PriorityQueue<Node, int>();

            var startNode = new Node(start.x, start.y)
            {
                G = 0,
                H = Heuristic(start.x, start.y, target.x, target.y) // эвристика
            };

            openPQ!.Enqueue(startNode, startNode.F);
            
            while (openPQ!.Count > 0)
            {
               
                if (openPQ!.Count + closed.Count > MaxOpenCount) MaxOpenCount = openPQ!.Count + closed.Count;

                //берём узел с минимальным F
                Node current = openPQ!.Dequeue();

                if ((current.X, current.Y) == target) return ReconstructPath(current);

                if (closed.Contains((current.X, current.Y))) continue;

                closed.Add((current.X, current.Y));

                foreach (var (dx, dy) in KnightMoves)
                {
                    Iterations++;
                    int nx = current.X + dx;
                    int ny = current.Y + dy;

                    if (nx >= 0 && ny >= 0 && nx < SIZE_BOARD && ny < SIZE_BOARD && grid[nx, ny] != States.Burning && !closed.Contains((nx, ny)))
                    {
                        var g = current.G + 1;
                        var h = Heuristic(nx, ny, target.x, target.y);

                        var neighbor = new Node(nx, ny, current) { G = g, H = h };

                        openPQ!.Enqueue(neighbor, neighbor.F);

                        GeneratedStates++;
                    }
                }
            }

            return null;
        }


        public int LowBorderHeuristic(int x1, int y1, int x2, int y2)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            // Конь симметричен — упорядочим dx >= dy
            if (dx < dy)
            {
                int tmp = dx;
                dx = dy;
                dy = tmp;
            }

            int h1 = (dx + 1) / 2;      // минимальные ходы по X
            int h2 = (dx + dy + 2) / 3; // минимальные ходы по сумме dx+dy
            return Math.Max(h1, h2);
        }

        public int BestKnightHeuristic(int x1, int y1, int x2, int y2)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            if (dx < dy)
            {
                int tmp = dx;
                dx = dy;
                dy = tmp;
            }

            return KnightDistanceTable[dx, dy];
        }

        #endregion Third Part of LabWork

    }
}

