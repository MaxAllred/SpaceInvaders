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

        private const int Level1Ships = 2;
        private const int Level2Ships = 4;
        private const int Level3Ships = 6;
        private const int Level4Ships = 8;

        private const int minSteps = -2;
        private const int maxSteps = 18;

        public readonly Collection<Enemy> AllEnemies;
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
            this.AllEnemies = new Collection<Enemy>();
            this.backgroundHeight = this.background.Height;
            this.backgroundWidth = this.background.Width;
            this.countSteps = maxSteps / 2;
        }

        #endregion

        #region Methods

        /// <summary>Moves all enemies.</summary>
        public void MoveAllEnemies()
        {
            if (MoveRight)
            {
                foreach (var currentEnemy in this.AllEnemies)
                {
                    currentEnemy.MoveRight();
                }
                this.countSteps++;
            }
            else
            {
                foreach (var currentEnemy in this.AllEnemies)
                {
                    currentEnemy.MoveLeft();
                }
                this.countSteps--;
            }

            if (countSteps == maxSteps || countSteps == minSteps)
            {
                MoveRight = !MoveRight;
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
            for (var i = 0; i < Level1Ships; i++)
            {
                var level1Enemy = new Enemy(EnemyShipVersion.LevelOne);
                this.background.Children.Add(level1Enemy.Sprite);
                this.AllEnemies.Add(level1Enemy);
            }
        }

        private void createLevel2Ships()
        {
            for (var i = 0; i < Level2Ships; i++)
            {
                var level2Enemy = new Enemy(EnemyShipVersion.LevelTwo);
                this.background.Children.Add(level2Enemy.Sprite);
                this.AllEnemies.Add(level2Enemy);
            }
        }

        private void createLevel3Ships()
        {
            for (var i = 0; i < Level3Ships; i++)
            {
                var level3Enemy = new Enemy(EnemyShipVersion.LevelThree);
                this.background.Children.Add(level3Enemy.Sprite);
                this.AllEnemies.Add(level3Enemy);
            }
        }

        private void createLevel4Ships()
        {
            for (var i = 0; i < Level4Ships; i++)
            {
                var level4Enemy = new Enemy(EnemyShipVersion.LevelFour);
                this.background.Children.Add(level4Enemy.Sprite);
                this.AllEnemies.Add(level4Enemy);
            }
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
                if (current.EnemyVersion.Equals(EnemyShipVersion.LevelThree) ||
                    current.EnemyVersion.Equals(EnemyShipVersion.LevelFour))
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