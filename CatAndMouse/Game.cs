using System;
using System.IO;

namespace CatAndMouseGame
{
    public class Game
    {
        public static string InputFile { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "ChaseData.txt");
        public static string OutFile { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "PursuitLog.txt");

        public int size;
        public Player cat;
        public Player mouse;
        public GameState state;
        private StreamWriter? writer;

        public Game()
        {
        

            using var sr = new StreamReader(InputFile);
            string? firstLine = sr.ReadLine();
            if (firstLine != null)
            {
                this.size = int.Parse(firstLine.Trim());
            }

            cat = new Player("Cat", this.size);
            mouse = new Player("Mouse", this.size);
            state = GameState.Start;
        }

        public Game(int size)
        {
            this.size = size;
            cat = new Player("Cat", this.size);
            mouse = new Player("Mouse", this.size);
            state = GameState.Start;
        }

        public void Run()
        {
            string[] lines = File.ReadAllLines(InputFile);

            using (writer = new StreamWriter(OutFile))
            {
                writer.WriteLine("Cat and Mouse");
                writer.WriteLine();
                writer.WriteLine("{0,-5} {1,-5}  {2}", "Cat", "Mouse", "Distance");
                writer.WriteLine("-------------------");

                for (int i = 1; i < lines.Length; i++)
                {
                    if (state == GameState.End) break;

                    string line = lines[i].Trim();
                    if (string.IsNullOrEmpty(line)) continue;

                    string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    char command = parts[0][0];

                    if (command == 'P')
                    {
                        DoPrintCommand();
                    }
                    else if (command == 'M' || command == 'C')
                    {
                        int steps = int.Parse(parts[1]);
                        DoMoveCommand(command, steps);

                        if (cat.state == State.Playing && mouse.state == State.Playing && cat.location == mouse.location)
                        {
                            cat.state = State.Winner;
                            mouse.state = State.Looser;
                            state = GameState.End;
                        }
                    }
                }

                if (state != GameState.End)
                {
                    mouse.state = State.Winner;
                    cat.state = State.Looser;
                    state = GameState.End;
                }

                writer.WriteLine("-------------------");
                writer.WriteLine();
                writer.WriteLine();
                writer.WriteLine("Distance traveled:   {0,-8} {1}", "Mouse", "Cat");
                writer.WriteLine("                        {0,2}      {1,2}", mouse.distanceTraveled, cat.distanceTraveled);
                writer.WriteLine();

                if (mouse.state == State.Winner)
                {
                    writer.WriteLine("Mouse evaded Cat");
                }
                else
                {
                    writer.WriteLine($"Mouse caught at:  {cat.location}");
                }
            }
        }

        private void DoMoveCommand(char command, int steps)
        {
            switch (command)
            {
                case 'M': mouse.Move(steps); break;
                case 'C': cat.Move(steps); break;
            }
        }

        private void DoPrintCommand()
        {
            string catStr = cat.state == State.NotInGame ? "??" : cat.location.ToString();
            string mouseStr = mouse.state == State.NotInGame ? "??" : mouse.location.ToString();

            if (cat.state == State.NotInGame || mouse.state == State.NotInGame)
            {
                writer!.WriteLine(" {0,2}    {1,2}", catStr, mouseStr);
            }
            else
            {
                int distance = GetDistance();
                writer!.WriteLine(" {0,2}    {1,2}        {2,2}", catStr, mouseStr, distance);
            }
        }

        private int GetDistance()
        {
            return Math.Abs(cat.location - mouse.location);
        }
    }
}