using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Timers;

namespace SpaceInvaders
{
    public class EnemyFleet
    {
        public List<Enemy> Enemies = new List<Enemy>();
        public List<int> RowPositions = new List<int>();
        public int FleetPositionY;
        public int FleetPositionX;
        private Game Game;
        private int Amount = 16;
        private Timer TickTimer;
        private int SideMovementSteps = 10;
        private int CurrentDirection = 1;

        public EnemyFleet(Game game)
        {
            Game = game;
            FleetPositionY = Console.WindowHeight - Game.Height;
        }

        public void Instantiate()
        {            
            var posX = 3;
            var posY = 1;
            var marginX = 5;
            var marginY = 2;
            var addedEnemies = 0;
            var moveableSpace = 10;
            
            RowPositions.Add(posY);

            while (true)
            {
                if (addedEnemies == Amount) break;
                
                if (posX >= Game.Width - moveableSpace)
                {
                    posY += marginY;
                    RowPositions.Add(posY);
                    posX = 3;
                }
                
                var enemy = new Enemy(posX, posY);
                Enemies.Add(enemy);
                enemy.Draw(FleetPositionX, FleetPositionY);
                
                posX += marginX;
                addedEnemies++;
            }
        }

        public void Move()
        {
            foreach (var enemy in Enemies)
            {
                enemy.Clear(FleetPositionX, FleetPositionY);
            }
            
            if (CurrentDirection == 1 && FleetPositionX == SideMovementSteps)
            {
                FleetPositionY++;
                CurrentDirection = -1;
            } else if (CurrentDirection == -1 && FleetPositionX == 0)
            {
                FleetPositionY++;
                CurrentDirection = 1;
            }
            else
            {
                FleetPositionX += CurrentDirection;
            }

            foreach (var enemy in Enemies)
            {
                enemy.Draw(FleetPositionX, FleetPositionY);
            }
            
            // End game when Enemy fleet reaches player
            int lowestEnemyPosition = Enemies.FindAll(x => x.IsAlive).Max(x => x.RelativePositionY);
            if (FleetPositionY + lowestEnemyPosition >= Console.WindowHeight - 3)
            {
                Game.EndGame(IEndGameResult.GameOver);
            }
        }

        public void Shoot()
        {
            Random rnd = new Random();
            int r = rnd.Next(Enemies.FindAll(x => x.IsAlive).Count);
            Enemy randomEnemy = Enemies.FindAll(x => x.IsAlive)[r];

            var projectile = new Projectile(
                FleetPositionY + randomEnemy.RelativePositionY,
                FleetPositionX + randomEnemy.RelativePositionX,
                -1
            );
            Game.Projectiles.Add(projectile);
            projectile.Draw();
        }

        public Enemy EnemyAtPosition(int x, int y)
        {
           var hit = Enemies.Find(enemy =>
           {
               if (!enemy.IsAlive) return false;
               return x >= FleetPositionX + enemy.RelativePositionX - 1 &&
                      x <= FleetPositionX + enemy.RelativePositionX + 1 &&
                      y == FleetPositionY + enemy.RelativePositionY;
           });
    
           return hit;
        }
    }
}