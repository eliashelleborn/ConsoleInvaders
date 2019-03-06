using System;

namespace SpaceInvaders
{
    public class Enemy
    {
        public bool IsAlive = true;
        public int RelativePositionX;
        public int RelativePositionY;
        public Enemy(int posX, int posY)
        {
            RelativePositionX = posX;
            RelativePositionY = posY;
        }

        public void Draw(int FleetPosX = 0, int FleetPosY = 0)
        {
            if (IsAlive)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                
                Console.SetCursorPosition(FleetPosX + RelativePositionX, FleetPosY + RelativePositionY);
                Console.Write("W");
                Console.SetCursorPosition(FleetPosX + RelativePositionX + 1, FleetPosY + RelativePositionY);
                Console.Write("W");
                Console.SetCursorPosition(FleetPosX + RelativePositionX - 1, FleetPosY + RelativePositionY);
                Console.Write("W");
                Console.SetCursorPosition(FleetPosX + RelativePositionX, FleetPosY + RelativePositionY - 1);
                Console.Write("_");
                
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        
        public void Clear(int FleetPosX = 0, int FleetPosY = 0)
        {
            Console.SetCursorPosition(FleetPosX + RelativePositionX, FleetPosY + RelativePositionY);
            Console.Write(" ");
            Console.SetCursorPosition(FleetPosX + RelativePositionX + 1, FleetPosY + RelativePositionY);
            Console.Write(" ");
            Console.SetCursorPosition(FleetPosX + RelativePositionX - 1, FleetPosY + RelativePositionY);
            Console.Write(" ");
            Console.SetCursorPosition(FleetPosX + RelativePositionX, FleetPosY + RelativePositionY - 1);
            Console.Write(" ");
        }
    }
}