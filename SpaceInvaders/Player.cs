using System;
using System.Collections.Generic;

namespace SpaceInvaders
{
    public class Player
    {
        public int Position { get; private set; }
        private Game GameInstance { get; }

        public Player(Game game)
        {
            GameInstance = game;
            Position = GameInstance.Width / 2;
        }
        
        // Direction:
        // -1 = Left
        // 1 = Right
        public bool Move(int direction)
        {
            var NewPosition = Position + direction;
            
            if (NewPosition + 1 > GameInstance.Width || NewPosition < 2) return false;
            
            Position = NewPosition;
            return true;

        }

        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            
            Console.SetCursorPosition(Position, Console.WindowHeight);
            Console.Write("M");
            Console.SetCursorPosition(Position - 1, Console.WindowHeight);
            Console.Write("M");
            Console.SetCursorPosition(Position + 1, Console.WindowHeight);
            Console.Write("M");
            Console.SetCursorPosition(Position, Console.WindowHeight - 2);
            Console.Write("^");
            
            if (Position - 2 >= 1)
            {
                Console.SetCursorPosition(Position - 2, Console.WindowHeight);
                Console.Write(" ");
            }
            if (Position + 1 < GameInstance.Width)
            {
                Console.SetCursorPosition(Position + 2, Console.WindowHeight);
                Console.Write(" ");
            }
            Console.SetCursorPosition(Position - 1, Console.WindowHeight - 2);
            Console.Write(" ");
            Console.SetCursorPosition(Position + 1, Console.WindowHeight - 2);
            Console.Write(" ");
            
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Clear()
        {
            Console.SetCursorPosition(Position, Console.WindowHeight);
            Console.Write(" ");
            Console.SetCursorPosition(Position - 1, Console.WindowHeight);
            Console.Write(" ");
            Console.SetCursorPosition(Position + 1, Console.WindowHeight);
            Console.Write(" ");
            Console.SetCursorPosition(Position, Console.WindowHeight - 2);
            Console.Write(" ");
        }
    }
}