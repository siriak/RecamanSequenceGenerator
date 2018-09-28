using System;

namespace RecamanSequenceFull
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WindowWidth = 50;
            Console.WindowHeight = 8;
            Console.BufferWidth = 50;
            Console.BufferHeight = 8;
            Runner.Load().Run();
        }
    }
}
