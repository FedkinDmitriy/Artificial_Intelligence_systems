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

                Iterations++;

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
            var O = new Stack<Node>(); // стек (вместо очереди)
            var C = new HashSet<(int, int)>();

            Iterations = 0;
            GeneratedStates = 0;
            MaxOpenCount = 0;

            O.Push(new Node(start.x, start.y));

            while (O.Count > 0)
            {
                if (O.Count > MaxOpenCount) MaxOpenCount = O.Count;

                Iterations++;

                var current = O.Pop(); //берём верхний

                if ((current.X, current.Y) == target) return ReconstructPath(current);

                if (C.Contains((current.X, current.Y))) continue;

                C.Add((current.X, current.Y));

                foreach (var (dx, dy) in KnightMoves)
                {
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
                SolverIterations++;
                if (O.Count > DFS_MaxOpenCount) DFS_MaxOpenCount = O.Count;

                var (current, depth) = O.Pop();

                if ((current.X, current.Y) == target) return ReconstructPath(current);

                if (C.Contains((current.X, current.Y)) || depth >= maxDepth) continue; //если достигнут предел глубины, не раскрываем узел и не добавляем его в C

                C.Add((current.X, current.Y));

                foreach (var (dx, dy) in KnightMoves)
                {
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
                Iterations++;


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
    }
}
