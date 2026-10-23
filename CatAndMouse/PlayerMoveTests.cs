using System;

namespace CatAndMouseGame
{
    public static class PlayerMoveTests
    {
        public static void RunAll()
        {
            Console.WriteLine("===== ТЕСТЫ МЕТОДА Player.Move(int steps) =====");
            Console.WriteLine();

            Test1_FirstMove();
            Test2_NormalMove();
            Test3_WrapAround();

            Console.WriteLine();
            Console.WriteLine("===== ВСЕ ТЕСТЫ ЗАВЕРШЕНЫ =====");
        }

        private static void Test1_FirstMove()
        {
            Console.WriteLine("--- Тест 1. Первый ход (появление на поле) ---");

            Player p = new Player("Test", 10);

            Console.WriteLine($"До хода:    state={p.state}, location={p.location}, distance={p.distanceTraveled}");

            p.Move(4);

            Console.WriteLine($"После Move(4): state={p.state}, location={p.location}, distance={p.distanceTraveled}");

            bool ok =
                p.state == State.Playing &&
                p.location == 4 &&
                p.distanceTraveled == 0;

            Console.WriteLine(ok ? "PASS" : "FAIL");
            Console.WriteLine();
        }

        private static void Test2_NormalMove()
        {
            Console.WriteLine("--- Тест 2. Обычное движение без заворота ---");

            Player p = new Player("Test", 10);
            p.Move(4);

            Console.WriteLine($"После первого хода: state={p.state}, location={p.location}, distance={p.distanceTraveled}");

            p.Move(3);
            Console.WriteLine($"После Move(3):  state={p.state}, location={p.location}, distance={p.distanceTraveled}");

            p.Move(-2);
            Console.WriteLine($"После Move(-2): state={p.state}, location={p.location}, distance={p.distanceTraveled}");

            bool ok =
                p.location == 5 &&
                p.distanceTraveled == 5;

            Console.WriteLine(ok ? "PASS" : "FAIL");
            Console.WriteLine();
        }

        private static void Test3_WrapAround()
        {
            Console.WriteLine("--- Тест 3. Движение с заворотом через край ---");

            Player p = new Player("Test", 10);
            p.Move(1);

            Console.WriteLine($"Старт: location={p.location}, distance={p.distanceTraveled}");

            p.Move(-3);
            Console.WriteLine($"После Move(-3):  location={p.location}, distance={p.distanceTraveled}");

            p.Move(15);
            Console.WriteLine($"После Move(15):  location={p.location}, distance={p.distanceTraveled}");

            bool ok =
                p.location == 3 &&
                p.distanceTraveled == 18;

            Console.WriteLine(ok ? "PASS" : "FAIL");
            Console.WriteLine();
        }
    }
}