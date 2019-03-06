using System;

namespace SpaceInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Game GameInstance = new Game(50);
                Player PlayerInstance = new Player(GameInstance);

                GameInstance.Start(PlayerInstance);
            } while (true);

        }
    }
}