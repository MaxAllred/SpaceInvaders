using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model
{
    public class EnemyManager
    {
        #region Data members

        private const double Level1ShipBottomOffset = 140;
        private const double Level2ShipBottomOffset = 240;
        private const double Level3ShipBottomOffset = 340;
        private const double Level4ShipBottomOffset = 440;
        public readonly Collection<GameObject> AllEnemies;
        public Bullet EnemyBullet;
        public bool MoveRight = true;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private readonly Canvas background;
        private int countSteps;
        private readonly int spacingIncrement = 15;

        #endregion

        #region Constructors

        public EnemyManager(Canvas background)
        {
            this.background = background;
            this.AllEnemies = new Collection<GameObject>();
            this.backgroundHeight = this.background.Height;
            this.backgroundWidth = this.background.Width;
        }

        #endregion

        #region Methods

        public void MoveAllEnemiesLeft()
        {
            foreach (var currentEnemy in this.AllEnemies)
            {
                currentEnemy.MoveLeft();
            }

            this.countSteps--;
            if (this.countSteps == -10)
            {
                this.MoveRight = true;
            }
        }

        public void MoveAllEnemiesRight()
        {
            foreach (var currentEnemy in this.AllEnemies)
            {
                currentEnemy.MoveRight();
            }

            this.countSteps++;
            if (this.countSteps == 10)
            {
                this.MoveRight = false;
            }
        }

        public void CreateAndPlaceAllEnemyShips()
        {
            this.createLevel1Ships();
            this.createLevel2Ships();
            this.createLevel3Ships();
            this.createLevel4Ships();
            this.placeAllShips();
        }

        private void createLevel1Ships()
        {
            var enemy1 = new Enemy(1);
            this.background.Children.Add(enemy1.Sprite);
            this.AllEnemies.Add(enemy1);
            var enemy2 = new Enemy(1);
            this.background.Children.Add(enemy2.Sprite);
            this.AllEnemies.Add(enemy2);
        }

        private void createLevel2Ships()
        {
            var enemy1Level2 = new Enemy(2);
            this.background.Children.Add(enemy1Level2.Sprite);
            this.AllEnemies.Add(enemy1Level2);
            var enemy2Level2 = new Enemy(2);
            this.background.Children.Add(enemy2Level2.Sprite);
            this.AllEnemies.Add(enemy2Level2);
            var enemy3Level2 = new Enemy(2);
            this.background.Children.Add(enemy3Level2.Sprite);
            this.AllEnemies.Add(enemy3Level2);
            var enemy4Level2 = new Enemy(2);
            this.background.Children.Add(enemy4Level2.Sprite);
            this.AllEnemies.Add(enemy4Level2);
        }

        private void createLevel3Ships()
        {
            var enemy1Level3 = new Enemy(3);
            this.background.Children.Add(enemy1Level3.Sprite);
            this.AllEnemies.Add(enemy1Level3);
            var enemy2Level3 = new Enemy(3);
            this.background.Children.Add(enemy2Level3.Sprite);
            this.AllEnemies.Add(enemy2Level3);
            var enemy3Level3 = new Enemy(3);
            this.background.Children.Add(enemy3Level3.Sprite);
            this.AllEnemies.Add(enemy3Level3);
            var enemy4Level3 = new Enemy(3);
            this.background.Children.Add(enemy4Level3.Sprite);
            this.AllEnemies.Add(enemy4Level3);
            var enemy5Level3 = new Enemy(3);
            this.background.Children.Add(enemy5Level3.Sprite);
            this.AllEnemies.Add(enemy5Level3);
            var enemy6Level3 = new Enemy(3);
            this.background.Children.Add(enemy6Level3.Sprite);
            this.AllEnemies.Add(enemy6Level3);
        }

        private void createLevel4Ships()
        {
            var enemy1Level4 = new Enemy(4);
            this.background.Children.Add(enemy1Level4.Sprite);
            this.AllEnemies.Add(enemy1Level4);
            var enemy2Level4 = new Enemy(4);
            this.background.Children.Add(enemy2Level4.Sprite);
            this.AllEnemies.Add(enemy2Level4);
            var enemy3Level4 = new Enemy(4);
            this.background.Children.Add(enemy3Level4.Sprite);
            this.AllEnemies.Add(enemy3Level4);
            var enemy4Level4 = new Enemy(4);
            this.background.Children.Add(enemy4Level4.Sprite);
            this.AllEnemies.Add(enemy4Level4);
            var enemy5Level4 = new Enemy(4);
            this.background.Children.Add(enemy5Level4.Sprite);
            this.AllEnemies.Add(enemy5Level4);
            var enemy6Level4 = new Enemy(4);
            this.background.Children.Add(enemy6Level4.Sprite);
            this.AllEnemies.Add(enemy6Level4);
            var enemy7Level4 = new Enemy(4);
            this.background.Children.Add(enemy7Level4.Sprite);
            this.AllEnemies.Add(enemy7Level4);
            var enemy8Level4 = new Enemy(4);
            this.background.Children.Add(enemy8Level4.Sprite);
            this.AllEnemies.Add(enemy8Level4);
        }

        private void placeAllShips()
        {
            var xPlacement = this.spacingIncrement;
            var placeRight = true;
            var counter = 0;
            var midpoint = this.backgroundWidth / 2;
            foreach (var currentShip in this.AllEnemies)
            {
                if (counter == 2 || counter == 6 || counter == 12 || counter == 20)
                {
                    xPlacement = this.spacingIncrement;
                }

                if (counter < 2)
                {
                    this.placeShitToLeftOrRight(placeRight, currentShip, xPlacement, midpoint);
                    currentShip.Y = this.backgroundHeight - Level1ShipBottomOffset;
                    xPlacement += (int) currentShip.Width - this.spacingIncrement;
                }
                else if (counter < 6)
                {
                    this.placeShitToLeftOrRight(placeRight, currentShip, xPlacement, midpoint);
                    currentShip.Y = this.backgroundHeight - Level2ShipBottomOffset;
                    xPlacement += (int) currentShip.Width - this.spacingIncrement;
                }
                else if (counter < 12)
                {
                    this.placeShitToLeftOrRight(placeRight, currentShip, xPlacement, midpoint);

                    currentShip.Y = this.backgroundHeight - Level3ShipBottomOffset;
                    xPlacement += (int) currentShip.Width - this.spacingIncrement;
                }
                else if (counter < 20)
                {
                    this.placeShitToLeftOrRight(placeRight, currentShip, xPlacement, midpoint);

                    currentShip.Y = this.backgroundHeight - Level4ShipBottomOffset;
                    xPlacement += (int) currentShip.Width - this.spacingIncrement;
                }

                counter++;
                placeRight = !placeRight;
            }
        }

        private void placeShitToLeftOrRight(bool placeRight, GameObject currentShip, int xPlacement, double midpoint)
        {
            if (placeRight)
            {
                currentShip.X = xPlacement + midpoint;
            }
            else
            {
                currentShip.X = midpoint - xPlacement;
            }
        }

        public void FireEnemyBullet()
        {
            var rand = new Random();
            var countOfFiringEnemies = 0;
            foreach (var current in this.AllEnemies)
            {
                if (current.Sprite.ToString().Equals("SpaceInvaders.View.Sprites.Level3EnemySprite") ||
                    current.Sprite.ToString().Equals("SpaceInvaders.View.Sprites.Level4EnemySprite"))
                {
                    countOfFiringEnemies++;
                }
            }

            if (countOfFiringEnemies == 0)
            {
                return;
            }

            var enemy = this.AllEnemies[this.AllEnemies.Count - 1 - rand.Next(countOfFiringEnemies)];
            if (!this.background.Children.Contains(this.EnemyBullet.Sprite))
            {
                this.background.Children.Add(this.EnemyBullet.Sprite);
                this.EnemyBullet.Y = this.backgroundHeight + this.EnemyBullet.Height;
            }

            if (this.EnemyBullet.Y >= this.backgroundHeight)
            {
                this.EnemyBullet.X = enemy.X + enemy.Width / 2 - this.EnemyBullet.Width / 2;
                this.EnemyBullet.Y = enemy.Y;
            }
        }

        #endregion
    }
}