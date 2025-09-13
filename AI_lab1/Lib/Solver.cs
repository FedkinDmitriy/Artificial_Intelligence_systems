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

            O.Enqueue(new Node(start.x, start.y));

            while (O.Count > 0)
            {
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

            O.Push(new Node(start.x, start.y));

            while (O.Count > 0)
            {
                var current = O.Pop(); //берём верхний

                if ((current.X, current.Y) == target) return ReconstructPath(current);

                if (C.Contains((current.X, current.Y))) continue;

                C.Add((current.X, current.Y));

                foreach (var (dx, dy) in KnightMoves)
                {
                    int nx = current.X + dx;
                    int ny = current.Y + dy;

                    if (nx >= 0 && ny >= 0 && nx < SIZE_BOARD && ny < SIZE_BOARD &&
                        grid[nx, ny] != States.Burning &&
                        grid[nx, ny] != States.Visited &&
                        !C.Contains((nx, ny)))
                    {
                        O.Push(new Node(nx, ny, current));
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
    }
}
