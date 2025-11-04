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
                if (O.Count > MaxOpenCount) MaxOpenCount = O.Count;

                

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
        /// Поиск пути DFS
        /// </summary>
        public List<Node>? FindPathDFS((int x, int y) start, (int x, int y) target)
        {
            var O = new Stack<Node>();
            var C = new HashSet<(int, int)>();

            Iterations = 0;
            GeneratedStates = 0;
            MaxOpenCount = 0;

            O.Push(new Node(start.x, start.y));

            while (O.Count > 0)
            {
                if (O.Count > MaxOpenCount) MaxOpenCount = O.Count;

                

                var current = O.Pop();

                if ((current.X, current.Y) == target) return ReconstructPath(current);

                if (C.Contains((current.X, current.Y))) continue;

                C.Add((current.X, current.Y));

                foreach (var (dx, dy) in KnightMoves)
                {
                    Iterations++;
                    int nx = current.X + dx;
                    int ny = current.Y + dy;

                    if (nx >= 0 && ny >= 0 && nx < SIZE_BOARD && ny < SIZE_BOARD && grid[nx, ny] != States.Burning && grid[nx, ny] != States.Visited && !C.Contains((nx, ny)))
                    {
                        O.Push(new Node(nx, ny, current));
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
                
                if (O.Count > DFS_MaxOpenCount) DFS_MaxOpenCount = O.Count;

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

        /// <summary>
        /// Двунаправленный поиск BFS
        /// </summary>
        public List<Node>? FindPathBidirectionalBFS((int x, int y) start, (int x, int y) target)
        {
            var forwardQueue = new Queue<Node>(); // от начальной позиции коня
            var backwardQueue = new Queue<Node>(); // от позиции короля

            var forwardVisited = new Dictionary<(int, int), Node>(); // узлы, достигнутые из начала
            var backwardVisited = new Dictionary<(int, int), Node>(); // узлы, достигнутые от цели

            //метрики
            Iterations = 0;
            GeneratedStates = 0;
            MaxOpenCount = 0;

            
            forwardQueue.Enqueue(new Node(start.x, start.y));
            forwardVisited[(start.x, start.y)] = new Node(start.x, start.y);

            backwardQueue.Enqueue(new Node(target.x, target.y));
            backwardVisited[(target.x, target.y)] = new Node(target.x, target.y);


            while (forwardQueue.Count > 0 && backwardQueue.Count > 0)
            {
                // Обновление метрик
                int totalOpen = forwardQueue.Count + backwardQueue.Count;
                if (totalOpen > MaxOpenCount) MaxOpenCount = totalOpen;
                


                // Раскрытие узла с начала
                if (TryExpandBidirectional(forwardQueue, forwardVisited, backwardVisited, out var meetingNode, true))
                {
                    //meetingNode – конкретная точка, где встреча произошла
                    return ReconstructBidirectionalPath(meetingNode, forwardVisited, backwardVisited);
                }

                // Раскрытие узла с конца
                if (TryExpandBidirectional(backwardQueue, backwardVisited, forwardVisited, out meetingNode, false))
                {
                    return ReconstructBidirectionalPath(meetingNode, forwardVisited, backwardVisited);
                }
            }

            return null;
        }

        // Пробуем раскрыть один уровень поиска
        private bool TryExpandBidirectional(Queue<Node> queue, Dictionary<(int, int), Node> visited, Dictionary<(int, int), Node> oppositeVisited, out Node meetingNode, bool isForward)
        {
            meetingNode = null!;

            if (queue.Count == 0) return false;

            var current = queue.Dequeue();

            foreach (var (dx, dy) in KnightMoves)
            {
                Iterations++;
                int nx = current.X + dx;
                int ny = current.Y + dy;

                if (nx >= 0 && ny >= 0 && nx < SIZE_BOARD && ny < SIZE_BOARD && grid[nx, ny] != States.Burning && grid[nx, ny] != States.Visited && !visited.ContainsKey((nx, ny)))
                {
                    var nextNode = new Node(nx, ny, current);
                    queue.Enqueue(nextNode);
                    visited[(nx, ny)] = nextNode;
                    GeneratedStates++;


                    if (oppositeVisited.ContainsKey((nx, ny)))
                    {
                        meetingNode = nextNode;
                        return true;
                    }
                }
            }
            return false;
        }

        // Восстановление пути после встречи
        private List<Node> ReconstructBidirectionalPath(Node meetingNode,Dictionary<(int, int), Node> forwardVisited,Dictionary<(int, int), Node> backwardVisited)
        {
            var pathForward = ReconstructPath(meetingNode);

            var meetingPos = (meetingNode.X, meetingNode.Y);
            var pathBackward = new List<Node>();
            var backwardNode = backwardVisited[meetingPos].Parent; // не включаем meetingNode второй раз
            while (backwardNode != null)
            {
                pathBackward.Add(backwardNode);
                backwardNode = backwardNode.Parent;
            }
            pathBackward.Reverse();

            pathForward.AddRange(pathBackward);
            return pathForward;
        }

        #endregion Second Part of First LabWork

        #region Third Part of LabWork

        /// <summary>
        /// A* поиск пути коня
        /// Добавлено максимальное значения для узлов в памяти, по умолчанию = int.MaxValue для SMA* (Simplified Memory-Bounded A*)
        /// </summary>
        public List<Node>? FindPathAStar((int x, int y) start, (int x, int y) target, Func<int,int,int,int,int> Heuristic, int maxNodes = int.MaxValue)
        {
            Iterations = 0;
            GeneratedStates = 0;
            MaxOpenCount = 0;

            var closed = new HashSet<(int, int)>();

            bool memoryLimited = maxNodes < int.MaxValue;
            PriorityQueue<Node, int>? openPQ = null;
            SortedSet<Node>? openSS = null;

            if (memoryLimited)
            {
                openSS = new SortedSet<Node>(Comparer<Node>.Create((a, b) =>
                {
                    int cmp = a.F.CompareTo(b.F); // приоритет по F
                    if (cmp == 0) cmp = a.G.CompareTo(b.G); // если F равны, то по G
                    if (cmp == 0) cmp = (a.X, a.Y).CompareTo((b.X, b.Y)); // если равны G, то по координатам
                    return cmp;
                }));
            }
            else
            {
                openPQ = new PriorityQueue<Node, int>();
            }

            var startNode = new Node(start.x, start.y)
            {
                G = 0,
                H = Heuristic(start.x, start.y, target.x, target.y) // эвристика
            };
            
            if (memoryLimited)
                openSS!.Add(startNode);
            else
                openPQ!.Enqueue(startNode, startNode.F);


            while ((memoryLimited ? openSS!.Count : openPQ!.Count) > 0)
            {
                

                if ((memoryLimited ? openSS!.Count : openPQ!.Count) > MaxOpenCount) MaxOpenCount = memoryLimited ? openSS!.Count : openPQ!.Count;

                //берём узел с минимальным F
                Node current = memoryLimited ? openSS!.Min! : openPQ!.Dequeue();

                if (memoryLimited) openSS?.Remove(current); // min в SortedSet не извлекает, а показывает

                if ((current.X, current.Y) == target) return ReconstructPath(current);

                if (closed.Contains((current.X, current.Y))) continue;

                closed.Add((current.X, current.Y));

                foreach (var (dx, dy) in KnightMoves)
                {
                    Iterations++;
                    int nx = current.X + dx;
                    int ny = current.Y + dy;

                    if (nx >= 0 && ny >= 0 && nx < SIZE_BOARD && ny < SIZE_BOARD && grid[nx, ny] != States.Burning && grid[nx, ny] != States.Visited && !closed.Contains((nx, ny)))
                    {
                        var g = current.G + 1;
                        var h = Heuristic(nx, ny, target.x, target.y);

                        var neighbor = new Node(nx, ny, current) { G = g, H = h };

                        if (memoryLimited)
                            openSS!.Add(neighbor);
                        else
                            openPQ!.Enqueue(neighbor, neighbor.F);

                        GeneratedStates++;
                    }
                }


                // если включен SMA* и превышен лимит памяти
                if (memoryLimited && openSS!.Count > maxNodes)
                {
                    var worst = openSS.Max!;
                    openSS.Remove(worst);

                    //обновляем эвристику родителя
                    if (worst.Parent != null)
                        worst.Parent.H = Math.Max(worst.Parent.H, worst.F);

                    // прекращаем поиск, если лимит превышен
                    if (openSS.Count >= maxNodes)
                        return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Общая эвристика для коня: нижняя граница числа ходов до цели
        /// </summary>
        /// /// <param name="x1">Текущая позиция X</param>
        /// <param name="y1">Текущая позиция Y</param>
        /// <param name="x2">Целевая позиция X</param>
        /// <param name="y2">Целевая позиция Y</param>
        /// <returns>Минимальное предполагаемое число ходов коня до цели</returns>
        public int CommonHeuristic(int x1, int y1, int x2, int y2)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            if (dx < dy)
            {
                int tmp = dx;
                dx = dy;
                dy = tmp;
            }

            int h1 = (dx + 1) / 2;        // минимальные ходы по X
            int h2 = (dx + dy + 2) / 3;   // минимальные ходы по сумме dx+dy
            return Math.Max(h1, h2);
        }

        public int ManhattanHeuristic(int x1, int y1, int x2, int y2)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            return dx + dy;
        }

        public int ChebyshevHeuristic(int x1, int y1, int x2, int y2)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            return Math.Max(dx, dy);
        }


        #endregion Third Part of LabWork


    }
}

