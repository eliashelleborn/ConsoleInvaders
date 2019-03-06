using System;

namespace SpaceInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetCursorPosition(1, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            
            Console.WriteLine("          ______                       __    ");
            Console.WriteLine("         / ____/___  ____  _________  / /__  ");
            Console.WriteLine("        / /   / __ \\/ __ \\/ ___/ __ \\/ / _ \\ ");
            Console.WriteLine("       / /___/ /_/ / / / (__  ) /_/ / /  __/ ");
            Console.WriteLine("       \\____/\\____/_/ /_/____/\\____/_/\\___/ ");
            Console.WriteLine();
            Console.WriteLine("        ____                     __              ");
            Console.WriteLine("       /  _/___ _   ______ _____/ /__  __________");
            Console.WriteLine("       / // __ \\ | / / __ `/ __  / _ \\/ ___/ ___/");
            Console.WriteLine("     _/ // / / / |/ / /_/ / /_/ /  __/ /  (__  ) ");
            Console.WriteLine("    /___/_/ /_/|___/\\__,_/\\__,_/\\___/_/  /____/  ");
            Console.WriteLine("                                                 ");
            Console.WriteLine();
            Console.WriteLine("         Press any key to start the game...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            
            Console.Clear();
            
            Game GameInstance = new Game(50);
            Player PlayerInstance = new Player(GameInstance);

            GameInstance.Start(PlayerInstance);
        
        }
    }
}