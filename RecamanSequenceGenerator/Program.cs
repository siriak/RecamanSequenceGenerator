using System;

namespace RecamanSequenceGenerator
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WindowWidth = 50;
            Console.WindowHeight = 8;
            Console.BufferWidth = 50;
            Console.BufferHeight = 8;
            Runner.Load().Run();
        }
    }
}
