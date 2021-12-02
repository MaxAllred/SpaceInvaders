using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model
{
    public class EnemyManager
    {
        #region Data members

        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private readonly Canvas background;

        private int Level1ShipCount = 2;
        private int Level2ShipCount = 4;
        private int Level3ShipCount = 6;
        private int Level4ShipCount = 8;
        private const int verticalSpaceBetweenRows = 20;
        private const int bottomBorderForMovement = 120;

        private const int MinSteps = -5;
        private const int MaxSteps = 28;

        public readonly Collection<EnemyShip> AllEnemies;
        public BonusEnemyShip bonusShip;
        public Bullet EnemyBullet;
        public bool MoveRight = true;
        private bool StepCloser = false;
        public bool CeaseFire = false;
        private SoundManager sound;
        
        private int countSteps;
        

        #endregion

        #region Constructors

        public EnemyManager(Canvas background)
        {
            sound = new SoundManager();
            this.background = background;
            this.AllEnemies = new Collection<EnemyShip>();
            this.bonusShip = new BonusEnemyShip();
            this.backgroundHeight = this.background.Height;
            this.backgroundWidth = this.background.Width;
            this.countSteps = MaxSteps / 2;
        }

        #endregion

        #region Methods

        /// <summary>Moves all enemies.</summary>
        public void MoveAllEnemies()
        {
            if (this.MoveRight)
            {
                foreach (var currentEnemy in this.AllEnemies)
                {
                    currentEnemy.Animate();
                    currentEnemy.MoveRight();
                }
                this.countSteps++;
            }
            else
            {
                foreach (var currentEnemy in this.AllEnemies)
                {
                    currentEnemy.Animate();
                    currentEnemy.MoveLeft();
                    if (StepCloser == true)
                    {
                        currentEnemy.MoveDown();
                        if (currentEnemy.Y >= this.backgroundHeight - bottomBorderForMovement)
                        {
                            this.StepCloser = false;
                        }
                    }
                }
                this.countSteps--;
            }

            if (this.countSteps == MaxSteps || this.countSteps == MinSteps)
            {
                this.MoveRight = !this.MoveRight;
            }

            this.bonusShip.MoveRight();
            if (this.bonusShip.X + this.bonusShip.SpeedX + this.bonusShip.Width > this.backgroundWidth)
            {
                this.background.Children.Remove(this.bonusShip.Sprite);
            }
        }

        public void CreateAndPlaceAllEnemyShips()
        {
            this.createAllEnemyShips();
            this.positionEnemies();

            this.createBonusShip();
        }

        private void createAllEnemyShips()
        {
            EnemyFactory factory = new Level4EnemyFactory();
            for (int i = 0; i < Level4ShipCount; i++)
            {
                this.createEnemyShip(factory);
            }

            factory = new Level3EnemyFactory();
            for (int i = 0; i < Level3ShipCount; i++)
            {
                this.createEnemyShip(factory);
            }

            factory = new Level2EnemyFactory();
            for (int i = 0; i < Level2ShipCount; i++)
            {
                this.createEnemyShip(factory);
            }

            factory = new Level1EnemyFactory();
            for (int i = 0; i < Level1ShipCount; i++)
            {
                this.createEnemyShip(factory);
            }
        }

        private void createEnemyShip(EnemyFactory theFactory)
        {
            EnemyShip newShip = theFactory.GetEnemyShip();
            this.background.Children.Add(newShip.Sprite1);
            this.background.Children.Add(newShip.Sprite2);
            this.AllEnemies.Add(newShip);
        }

        private void createBonusShip()
        {
            int startingY = 25;
            this.background.Children.Add(this.bonusShip.Sprite);
            this.bonusShip.X = 0;
            this.bonusShip.Y = startingY;
        }

        private void positionEnemies()
        {
            var shipWidth = this.AllEnemies[0].Width;
            var shipGap = shipWidth / 3;

            for (var shipIndex = 0; shipIndex < this.AllEnemies.Count; shipIndex++)
            {
                int rowNumberFromTop;
                int shipsPerRow;
                int shipNumberInRow = shipIndex;

                if (shipIndex < Level4ShipCount)
                {
                    rowNumberFromTop = 1;
                    shipsPerRow = Level4ShipCount;
                }
                else if (shipIndex < Level4ShipCount + Level3ShipCount)
                {
                    rowNumberFromTop = 2;
                    shipsPerRow = Level3ShipCount;
                    shipNumberInRow -= Level4ShipCount;
                } 
                else if (shipIndex < Level4ShipCount + Level3ShipCount + Level2ShipCount)
                {
                    rowNumberFromTop = 3;
                    shipsPerRow = Level2ShipCount;
                    shipNumberInRow -= Level4ShipCount + Level3ShipCount;
                }
                else
                {
                    rowNumberFromTop = 4;
                    shipsPerRow = Level1ShipCount;
                    shipNumberInRow -= Level4ShipCount + Level3ShipCount + Level2ShipCount;
                }

                double distanceFromLeft =
                    (this.backgroundWidth - shipsPerRow * shipWidth - (shipsPerRow - 1) * shipGap) / 2;
                this.AllEnemies[shipIndex].Y = (this.AllEnemies[shipIndex].Height + verticalSpaceBetweenRows) * rowNumberFromTop;
                this.AllEnemies[shipIndex].X = distanceFromLeft + (shipGap + shipWidth) * shipNumberInRow;
            }
        }

        public void FireEnemyBullet()
        {
            if (CeaseFire)
            {
                return;
            }
            var rand = new Random();
            var countOfFiringEnemies = 0;
            foreach (var current in this.AllEnemies)
            {
                if (current.CanShoot)
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
                sound.enemyShot();
            }

            if (this.EnemyBullet.Y >= this.backgroundHeight)
            {
                this.EnemyBullet.X = enemy.X + enemy.Width / 2 - this.EnemyBullet.Width / 2;
                this.EnemyBullet.Y = enemy.Y;
                sound.enemyShot();

            }
        }

        public void GenerateNewLevel(int level)
        {
            if(level == 2)
            {
                this.Level1ShipCount = 4;
                this.Level2ShipCount = 6;
                this.Level3ShipCount = 8;
                this.Level4ShipCount = 10;
                this.MoveRight = true;
                this.countSteps = MaxSteps / 2;
                this.createAllEnemyShips();
                this.positionEnemies();
            }
            else if (level == 3)
            {
                this.Level1ShipCount = 10;
                this.Level2ShipCount = 8;
                this.Level3ShipCount = 6;
                this.Level4ShipCount = 4;
                this.MoveRight = true;
                this.StepCloser = true;
                this.countSteps = MaxSteps / 2;
                this.createAllEnemyShips();
                this.positionEnemies();
            }
            
        }

        #endregion
    }
}