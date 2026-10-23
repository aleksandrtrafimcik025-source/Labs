using System;

namespace CatAndMouseGame
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayerMoveTests.RunAll();

            Console.WriteLine("Нажми любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
