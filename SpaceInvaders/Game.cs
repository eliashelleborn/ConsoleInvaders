using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;

namespace SpaceInvaders
{
    public class Game
    {
        public int Width;
        public int Height = 20;
        public Player Player;
        public List<Projectile> Projectiles = new List<Projectile>();
        private EnemyFleet EnemyFleet;
        private int Speed = 5; // Higher = Slower
        private Timer TickTimer;
        private int Ticks;

        public Game(int width)
        {
            Width = width;
        }

        public void Start(Player player)
        {
            // Instantiate Player
            Player = player;
            Player.Draw();

            EnemyFleet = new EnemyFleet(this);
            EnemyFleet.Instantiate();
            
            // Draw game borders
            for (int i = Console.WindowHeight; i > Console.WindowHeight - Height; i--)
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, i);
                Console.Write("|");
                Console.SetCursorPosition(Width + 1, i);
                Console.Write("|");
            }
            
            // Start Tick method
            TickTimer = new Timer();
            TickTimer.Elapsed += Tick;
            TickTimer.Interval = 100;
            TickTimer.Enabled = true;
            
            // Start listening for player input/keypresses
            KeypressListener();
        }

        private void Tick(object sender, EventArgs e)
        {
            // If all enemies are dead, end game
            if (EnemyFleet.Enemies.TrueForAll(x => !x.IsAlive))
            {
                EndGame(EndGameResult.Win);
            }
            
            // Handle projectiles
            foreach (var projectile in Projectiles)
            {
                if (projectile.Move())
                {
                    projectile.Draw();
                }
                else
                {
                    projectile.Clear();
                    Projectiles.Remove(projectile);
                }
                
                // Detect hits
                // Player projectiles
                if (projectile.Direction == 1)
                {
                    if (EnemyFleet.RowPositions.Any(y => y + EnemyFleet.FleetPositionY == projectile.PositionY))
                    {     
                        Enemy hitEnemy = EnemyFleet.EnemyAtPosition(projectile.PositionX, projectile.PositionY);
                        if (!hitEnemy.Equals(default(Enemy)))
                        {
                            hitEnemy.Clear();
                            hitEnemy.IsAlive = false;
                            projectile.Clear();
                            Projectiles.Remove(projectile);
                        }
                    }
                }
                // Enemy projectiles
                if (projectile.Direction == -1)
                {
                    if (projectile.PositionY == Console.WindowHeight - 1)
                    {
                        if (projectile.PositionX >= Player.Position - 1 && projectile.PositionX <= Player.Position + 1)
                        {
                            EndGame(EndGameResult.GameOver);
                        }
                    }
                }

            }

            // EnemyFleet actions
            if (Ticks % Speed == 0)
            {
                EnemyFleet.Move();

                if (Ticks % 30 == 0)
                {
                    EnemyFleet.Shoot();
                }
            }
         

            Ticks++;
        }

        private void KeypressListener()
        {

            DateTime lastDrawnProjectile = DateTime.MinValue;
            
            ConsoleKey key;
            do
            {
                /*
                 * while (!Console.KeyAvailable) { }
                 */
                

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                    {
                        if (Player.Move(-1))
                            Player.Draw();
                        break;
                    }
                    case ConsoleKey.RightArrow:
                    {
                        if (Player.Move(1))
                            Player.Draw();
                        break;
                    }
                    case ConsoleKey.Spacebar:
                    {
                        if (DateTime.Now > lastDrawnProjectile.AddSeconds(1))
                        {
                            var projectile = new Projectile(Console.WindowHeight - 3,Player.Position, 1);
                            Projectiles.Add(projectile);
                            projectile.Draw();
                            lastDrawnProjectile = DateTime.Now;
                        }

                        break;
                    }
                }

            } while (key != ConsoleKey.Escape);

            Environment.Exit(0);
        }

        public void EndGame(EndGameResult gameResult)
        {
            TickTimer.Stop();
            Projectiles.RemoveAll(x => true);
            Console.Clear();
            if (gameResult == EndGameResult.Win)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(1, 1);
                Console.WriteLine("Congratulations, you've defeated all the alien ships!");
                Console.SetCursorPosition(1, 2);
                Console.WriteLine("A new game will start in 5 seconds...");
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (gameResult == EndGameResult.GameOver)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(1, 1);
                Console.WriteLine("GAME OVER!");
                Console.SetCursorPosition(1, 2);
                Console.WriteLine("A new game will start in 5 seconds...");
                Console.ForegroundColor = ConsoleColor.White;
            }

            System.Threading.Thread.Sleep(5000);
            Console.Clear();
            Start(new Player(this));
        }
    }
}