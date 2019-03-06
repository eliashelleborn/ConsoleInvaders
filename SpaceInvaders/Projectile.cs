using System;

namespace SpaceInvaders
{
    public class Projectile
    {
        public int PositionY;
        public int PositionX;
        public int Direction;

        public Projectile(int positionY, int positionX, int direction)
        {
            PositionY = positionY;
            PositionX = positionX;
            Direction = direction;
        }

        public bool Move()
        {
            var NewPositionY = PositionY - Direction;
            if (NewPositionY < 0 || NewPositionY > Console.WindowHeight)
            {
                return false;
            }
                
              
            
            PositionY = NewPositionY;
            return true;

        }

        public void Draw()
        {
            if (Direction == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(PositionX, PositionY);
                Console.Write("o");
                Console.SetCursorPosition(PositionX, PositionY - 1);
                Console.Write(" ");
              
            } else if (Direction == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                
                Console.SetCursorPosition(PositionX, PositionY);
                Console.Write("|");
                Console.SetCursorPosition(PositionX, PositionY + 1);
                Console.Write(" ");
            }
        }

        public void Clear()
        {
            Console.SetCursorPosition(PositionX, PositionY);
            Console.Write(" ");
        }
    }
}