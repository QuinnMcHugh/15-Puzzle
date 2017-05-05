using System;

namespace Puzzle
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Puzzle puzzle = new Puzzle();
            puzzle.Scramble();
            Console.WriteLine(puzzle);
        }
    }
}
