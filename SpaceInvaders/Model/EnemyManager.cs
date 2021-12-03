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
            this.EnemyBullet = new Bullet();
        }

        #endregion

        #region Methods

        /// <summary>Moves all elements.</summary>
        public void MoveAllElements()
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
                    if (this.StepCloser)
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


            if (this.background.Children.Contains(this.bonusShip.Sprite))
            {
                this.bonusShip.MoveRight();
            }

            if (this.bonusShip.X + this.bonusShip.SpeedX + this.bonusShip.Width > this.backgroundWidth)
            {
                this.background.Children.Remove(this.bonusShip.Sprite);
            }

            if (this.background.Children.Contains(this.EnemyBullet.Sprite))
            {
                this.EnemyBullet.MoveDown();

                if (this.EnemyBullet.Y > (this.backgroundHeight))
                {
                    this.background.Children.Remove(this.EnemyBullet.Sprite);
                }

            }

        }

        public void CreateAndPlaceAllEnemyShips()
        {
            this.createAllEnemyShips();
            this.positionEnemies();
        }

        public void OnTick()
        {
            this.randomShot();
            this.randomBonusEnemy();
            this.MoveAllElements();
        }

        private void randomShot()
        {
            var rand = new Random();
            if (rand.Next(10) == 1)
            {
                this.ChooseEnemyAndShoot();
            }
        }

        private void randomBonusEnemy()
        {
            if (this.background.Children.Contains(this.bonusShip.Sprite))
            {
                return;
            }

            var rand = new Random();
            if (rand.Next(100) == 1)
            {
                int startingY = 25;
                this.background.Children.Add(this.bonusShip.Sprite);
                this.bonusShip.X = 0;
                this.bonusShip.Y = startingY;
            }
        }

        private void createAllEnemyShips()
        {
            EnemyFactory factory = new Level4EnemyFactory();
            for (int i = 0; i < this.Level4ShipCount; i++)
            {
                this.createEnemyShip(factory);
            }

            factory = new Level3EnemyFactory();
            for (int i = 0; i < this.Level3ShipCount; i++)
            {
                this.createEnemyShip(factory);
            }

            factory = new Level2EnemyFactory();
            for (int i = 0; i < this.Level2ShipCount; i++)
            {
                this.createEnemyShip(factory);
            }

            factory = new Level1EnemyFactory();
            for (int i = 0; i < this.Level1ShipCount; i++)
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

        private void positionEnemies()
        {
            var shipWidth = this.AllEnemies[0].Width;
            var shipGap = shipWidth / 3;

            for (var shipIndex = 0; shipIndex < this.AllEnemies.Count; shipIndex++)
            {
                int rowNumberFromTop;
                int shipsPerRow;
                int shipNumberInRow = shipIndex;

                if (shipIndex < this.Level4ShipCount)
                {
                    rowNumberFromTop = 1;
                    shipsPerRow = this.Level4ShipCount;
                }
                else if (shipIndex < this.Level4ShipCount + this.Level3ShipCount)
                {
                    rowNumberFromTop = 2;
                    shipsPerRow = this.Level3ShipCount;
                    shipNumberInRow -= this.Level4ShipCount;
                } 
                else if (shipIndex < this.Level4ShipCount + this.Level3ShipCount + this.Level2ShipCount)
                {
                    rowNumberFromTop = 3;
                    shipsPerRow = this.Level2ShipCount;
                    shipNumberInRow -= this.Level4ShipCount + this.Level3ShipCount;
                }
                else
                {
                    rowNumberFromTop = 4;
                    shipsPerRow = this.Level1ShipCount;
                    shipNumberInRow -= this.Level4ShipCount + this.Level3ShipCount + this.Level2ShipCount;
                }

                double distanceFromLeft =
                    (this.backgroundWidth - shipsPerRow * shipWidth - (shipsPerRow - 1) * shipGap) / 2;
                this.AllEnemies[shipIndex].Y = (this.AllEnemies[shipIndex].Height + verticalSpaceBetweenRows) * rowNumberFromTop;
                this.AllEnemies[shipIndex].X = distanceFromLeft + (shipGap + shipWidth) * shipNumberInRow;
            }
        }

        public void ChooseEnemyAndShoot()
        {
            if (this.background.Children.Contains(this.bonusShip.Sprite))
            {
                this.Shoot(this.bonusShip);
                
            }
            else
            {
                EnemyShip enemy = this.chooseRandomEnemy();
                if (enemy != null)
                {
                    this.Shoot(enemy);
                    
                }
            }
        }


        private EnemyShip chooseRandomEnemy()
        {
            int shootingEnemies = 0;

            foreach(var enemy in this.AllEnemies)
            {
                if (enemy.CanShoot) shootingEnemies++;
            }

            if(shootingEnemies > 0)
            {
                Random random = new Random();
                return this.AllEnemies[random.Next(shootingEnemies + 1)];
            }

            return null;
        }

        public void Shoot(EnemyShip enemy)
        {
            if (this.CeaseFire)
            {
                return;
            }

            if (!this.background.Children.Contains(this.EnemyBullet.Sprite))
            {
                this.sound.enemyShot();
                this.background.Children.Add(this.EnemyBullet.Sprite);
                this.EnemyBullet.Y = enemy.Y + enemy.Height + this.EnemyBullet.Height;
                this.EnemyBullet.X = enemy.X + .5 * enemy.Width - .5 * this.EnemyBullet.Width;
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