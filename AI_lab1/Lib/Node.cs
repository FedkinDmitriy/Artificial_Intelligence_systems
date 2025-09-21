using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_lab1.Lib
{
    /// <summary>
    /// конкретное состояние среды (позиция коня + путь)
    /// </summary>
    public class Node
    {
        public int X { get; }
        public int Y { get; }
        public Node? Parent { get; }
        public Node(int x, int y, Node? parent = null)
        {
            X = x;
            Y = y;
            Parent = parent;
        }
    }
}
