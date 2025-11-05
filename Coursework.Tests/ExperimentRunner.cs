using AI_lab1.Lib;
using System;
using System.Collections.Generic;
using System.Linq;

public static class ExperimentRunner
{
    public static void RunExperiments(int numTests)
    {
        var rnd = new Random();
        var target = (x: 7, y: 7);

        var results = new Dictionary<string, List<(int d, int iter, int gen, int mem)>>()
        {
            { "BFS", new() },
            { "IDS", new() },
            { "A*(Common)", new() },
            { "A*(Manhattan)", new() }
        };

        for (int i = 0; i < numTests; i++)
        {
            //var start = (x: rnd.Next(7), y: rnd.Next(7));
            var start = (i, i);
            var grid = new States[8, 8];

            // BFS
            RunFullTest("BFS", (g, s, t) => g.FindPathBFS(s, t), grid, start, target, results);

            // IDS
            RunFullTest("IDS", (g, s, t) => g.FindPathIterativeDeepening(s, t, 64), grid, start, target, results);

            // A*(Common)
            RunFullTest("A*(Common)", (g, s, t) => g.FindPathAStar(s, t, g.CommonHeuristic), grid, start, target, results);

            // A*(Manhattan)
            RunFullTest("A*(Manhattan)", (g, s, t) => g.FindPathAStar(s, t, g.ManhattanHeuristic), grid, start, target, results);

        }

        Console.WriteLine($"Результаты {numTests} тестов:\n");

        foreach (var res in results)
        {
            Console.WriteLine(res.Key);

            foreach (var item in res.Value)
            {
                Console.WriteLine(item);
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

        // отмечаем клетки, по которым прошёл конь
        foreach (var node in pathToKing.Skip(1))
        {
            if (grid[node.X, node.Y] == States.Empty)
                grid[node.X, node.Y] = States.Visited;
        }

        // путь обратно
        var pathBack = searchFunc(solver, target, start);
        if (pathBack == null)
            return; // не вернулся — задача не решена, ничего не сохраняем

        // если дошёл и вернулся — сохраняем результат
        int totalD = (pathToKing.Count - 1) + (pathBack.Count - 1);
        results[name].Add((totalD, solver.Iterations, solver.GeneratedStates, solver.MaxOpenCount));
    }

}
