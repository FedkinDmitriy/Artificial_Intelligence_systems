using AI_lab1.Lib;
using System;
using System.Collections.Generic;
using System.Linq;

public static class ExperimentRunner
{
    public static Dictionary<string, List<(int d, int iter, int gen, int mem)>> results = new();

    public static Dictionary<string, List<(int D, double averageN)>> averageNodes = new();

    public static Dictionary<string, List<(int D, double averageN, double bStar)>> branching = new();

    private static readonly int[] depths = { 2, 3, 4, 5, 6 };

    public static void RunExperiments()
    {
        
        var tests = TestGenerator();

        results = new Dictionary<string, List<(int d, int iter, int gen, int mem)>>()
    {
        { "BFS", new() },
        { "IDS", new() },
        { "A*(Common)", new() },
        { "A*(Manhattan)", new() }
    };

        foreach (var (start, target, grid) in tests)
        {
            RunFullTest("BFS", (g, s, t) => g.FindPathBFS(s, t), grid, start, target, results);
            RunFullTest("IDS", (g, s, t) => g.FindPathIterativeDeepening(s, t, 64), grid, start, target, results);
            RunFullTest("A*(Common)", (g, s, t) => g.FindPathAStar(s, t, g.CommonHeuristic), grid, start, target, results);
            RunFullTest("A*(Manhattan)", (g, s, t) => g.FindPathAStar(s, t, g.ManhattanHeuristic), grid, start, target, results);
        }


        averageNodes.Clear();
        foreach (var kvp in results)
        {
            var algoResults = kvp.Value;
            var averagesForDepths = new List<(int D, double averageN)>();

            foreach (var d in depths)
            {
                var itemsForD = algoResults.Where(r => r.d == d).ToList();
                if (itemsForD.Count > 0)
                {
                    double avgNodes = itemsForD.Average(r => r.gen);
                    averagesForDepths.Add((d, avgNodes));
                }
            }
            averageNodes[kvp.Key] = averagesForDepths;
        }

        foreach (var kvp in averageNodes)
        {
            Console.WriteLine($"Алгоритм: {kvp.Key}");
            foreach (var (d, avg) in kvp.Value)
                Console.WriteLine($"  d={d} -> среднее число узлов: {avg:F1}");
        }

        branching.Clear();
        foreach (var kvp in averageNodes)
        {
            var bList = new List<(int D, double averageN, double bStar)>();
            foreach (var (d, avgN) in kvp.Value)
            {
                double bStar = ComputeBStar(avgN, d);
                bList.Add((d, avgN, bStar));
            }
            branching[kvp.Key] = bList;
        }
        Console.WriteLine("\n=== Эффективный коэффициент ветвления b* ===");
        foreach (var kvp in branching)
        {
            Console.WriteLine($"Алгоритм: {kvp.Key}");
            foreach (var (d, avgN, bStar) in kvp.Value)
            {
                Console.WriteLine($"  d={d}, N={avgN:F1} → b*={bStar:F1}");
            }
        }


    }

    private static void RunFullTest(
        string name,
        Func<Solver, (int x, int y), (int x, int y), List<Node>?> searchFunc,
        States[,] baseGrid,
        (int x, int y) start,
        (int x, int y) target,
        Dictionary<string, List<(int d, int iter, int gen, int mem)>> results)
    {

        var grid = (States[,])baseGrid.Clone();
        var solver = new Solver(grid);

        // путь к королю
        var pathToKing = searchFunc(solver, start, target);
        if (pathToKing == null)
            return; 

        int totalD = pathToKing.Count - 1;

        results[name].Add((totalD, solver.Iterations, solver.GeneratedStates, solver.MaxOpenCount));
    }

    private static List<((int, int) start, (int, int) target, States[,])> TestGenerator()
    {
        const int SIZE = 8;
        var target = (x: 7, y: 7);
        var rnd = new Random();
        var tests = new List<((int, int), (int, int), States[,])>();

        //int[] depths = { 2, 3, 4, 5, 6 };

        Console.WriteLine("=== Генерация тестовых данных ===");

        foreach (int d in depths)
        {
            Console.WriteLine($"\n Генерация тестов для d = {d}");
            int found = 0;

            while (found < 10)
            {
                var grid = new States[SIZE, SIZE];
                var start = (x: rnd.Next(SIZE), y: rnd.Next(SIZE));

                // рандомно поджигаем клетки
                int burns = rnd.Next(2, 6);
                List<(int, int)> burningCells = new();

                for (int i = 0; i < burns; i++)
                {
                    int bx = rnd.Next(SIZE);
                    int by = rnd.Next(SIZE);
                    if ((bx, by) != start && (bx, by) != target && grid[bx, by] == States.Empty)
                    {
                        grid[bx, by] = States.Burning;
                        burningCells.Add((bx, by));
                    }
                }

                var solver = new Solver(grid);
                var path = solver.FindPathBFS(start, target);

                if (path == null) continue;

                int realD = path.Count - 1;
                if (realD == d)
                {
                    string burnsStr = string.Join(", ", burningCells.Select(c => $"({c.Item1},{c.Item2})"));
                    Console.WriteLine($" -> [{found + 1, -2}] start=({start.x},{start.y}), burns={burningCells.Count}: {burnsStr}");

                    tests.Add((start, target, grid));
                    found++;
                }
            }

            Console.WriteLine($" Сгенерировано 10 тестов для d = {d}");
        }

        Console.WriteLine("\n=== Генерация завершена ===");
        return tests;
    }

    public static void ShowFullResults()
    {
        Console.WriteLine("\nРезультаты экспериментов:");
        foreach (var kvp in results)
        {
            if (kvp.Value.Count == 0) return;

            Console.WriteLine("Алгоритм поиска: " + kvp.Key);
            foreach (var item in kvp.Value)
            {
                Console.WriteLine($"Глубина: {item.d} Итерации: {item.iter,3} Состояния: {item.gen,3} Узлов в памяти: {item.mem,3}");
            }
        }
    }

    public static double ComputeBStar(double N, int d)
    {
        if (d == 0) return 0;

        double low = 1.0001;  // нижняя граница (ветвление не меньше 1)
        double high = 10;     // верхняя граница 
        double mid;

        while (high - low > 1e-6)
        {
            mid = (low + high) / 2;
            double f = (Math.Pow(mid, d + 1) - 1) / (mid - 1) - N;

            if (f > 0)
                high = mid;
            else
                low = mid;
        }

        return (low + high) / 2;
    }

}
