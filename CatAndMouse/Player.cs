using System;

namespace CatAndMouseGame
{
    public class Player
    {
        public string name;
        public int location;
        public State state = State.NotInGame;
        public int distanceTraveled = 0;
        private readonly int boardSize;

        public Player(string name, int boardSize)
        {
            this.name = name;
            this.location = -1;
            this.boardSize = boardSize;
        }

        public void Move(int steps)
        {
            if (state == State.NotInGame)
            {
                this.location = steps;
                this.state = State.Playing;
                this.distanceTraveled = 0;
            }
            else
            {
                this.distanceTraveled += Math.Abs(steps);

                int zeroBasedPos = this.location - 1;
                zeroBasedPos = (zeroBasedPos + steps) % boardSize;
                if (zeroBasedPos < 0) zeroBasedPos += boardSize;

                this.location = zeroBasedPos + 1;
            }
        }
    }
}