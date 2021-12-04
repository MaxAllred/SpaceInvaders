using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages an Enemy Manager for the enemies.
    /// </summary>
    public class EnemyManager
    {
        #region Data members

        private const int VerticalSpaceBetweenRows = 20;
        private const int BottomBorderForMovement = 120;

        private const int MinSteps = -5;
        private const int MaxSteps = 28;

        /// <summary>
        ///     Keeps track of the collection of all enemy ships.
        /// </summary>
        public readonly Collection<EnemyShip> AllEnemies;

        /// <summary>
        ///     Creates a bonus ship.
        /// </summary>
        public BonusEnemyShip BonusShip;

        public Bullet EnemyBullet;
        public bool MoveRight = true;
        public bool CeaseFire = false;
        public bool BonusActive = false;

        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private readonly Canvas background;
        private bool stepCloser;
        private readonly SoundManager sound;
        private int level1ShipCount = 2;
        private int level2ShipCount = 4;
        private int level3ShipCount = 6;
        private int level4ShipCount = 8;
        private int level;

        private int countSteps;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnemyManager" /> class.
        /// </summary>
        /// <param name="background">The background.</param>
        public EnemyManager(Canvas background)
        {
            this.sound = new SoundManager();
            this.background = background;
            this.AllEnemies = new Collection<EnemyShip>();
            this.BonusShip = new BonusEnemyShip();
            this.backgroundHeight = this.background.Height;
            this.backgroundWidth = this.background.Width;
            this.countSteps = MaxSteps / 2;
            this.EnemyBullet = new Bullet();
            this.level = 1;
        }

        #endregion

        #region Methods

        /// <summary>Moves all elements.</summary>
        public void MoveAllElements()
        {
            switch (this.level)
            {
                case 1:
                    this.level1Movement();
                    break;
                case 2:
                    this.level2Movement();
                    break;
                default:
                    this.level3Movement();
                    break;
            }

            if (this.background.Children.Contains(this.BonusShip.Sprite))
            {
                this.BonusShip.MoveRight();
            }

            if (this.BonusShip.X + this.BonusShip.SpeedX + this.BonusShip.Width > this.backgroundWidth)
            {
                this.background.Children.Remove(this.BonusShip.Sprite);
            }

            if (this.background.Children.Contains(this.EnemyBullet.Sprite))
            {
                this.EnemyBullet.MoveDown();

                if (this.EnemyBullet.Y > this.backgroundHeight)
                {
                    this.background.Children.Remove(this.EnemyBullet.Sprite);
                }
            }
        }

        private void level1Movement()
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
                }

                this.countSteps--;
            }

            if (this.countSteps == MaxSteps || this.countSteps == MinSteps)
            {
                this.MoveRight = !this.MoveRight;
            }
        }

        private void level2Movement()
        {
            if (this.MoveRight)
            {
                foreach (var currentEnemy in this.AllEnemies)
                {
                    if (currentEnemy.GetType().ToString() == "SpaceInvaders.Model.Level4Enemy" ||
                        currentEnemy.GetType().ToString() == "SpaceInvaders.Model.Level3Enemy")
                    {
                        currentEnemy.Animate();
                        currentEnemy.MoveRight();
                    }
                    else
                    {
                        currentEnemy.Animate();
                        currentEnemy.MoveLeft();
                    }
                }

                this.countSteps++;
            }
            else
            {
                foreach (var currentEnemy in this.AllEnemies)
                {
                    if (currentEnemy.GetType().ToString() == "SpaceInvaders.Model.Level4Enemy" ||
                        currentEnemy.GetType().ToString() == "SpaceInvaders.Model.Level3Enemy")
                    {
                        currentEnemy.Animate();
                        currentEnemy.MoveLeft();
                    }
                    else
                    {
                        currentEnemy.Animate();
                        currentEnemy.MoveRight();
                    }
                }

                this.countSteps--;
            }

            if (this.countSteps == MaxSteps || this.countSteps == MinSteps)
            {
                this.MoveRight = !this.MoveRight;
            }
        }

        private void level3Movement()
        {
            if (this.MoveRight)
            {
                foreach (var currentEnemy in this.AllEnemies)
                {
                    currentEnemy.Animate();
                    currentEnemy.MoveRight();
                    if (!this.stepCloser)
                    {
                        currentEnemy.MoveUp();
                        if (currentEnemy.Y <= 0 + currentEnemy.Height)
                        {
                            this.stepCloser = true;
                        }
                    }
                }

                this.countSteps++;
            }
            else
            {
                foreach (var currentEnemy in this.AllEnemies)
                {
                    currentEnemy.Animate();
                    currentEnemy.MoveLeft();
                    if (this.stepCloser)
                    {
                        currentEnemy.MoveDown();
                        if (currentEnemy.Y >= this.backgroundHeight - BottomBorderForMovement)
                        {
                            this.stepCloser = false;
                        }
                    }
                }

                this.countSteps--;
            }

            if (this.countSteps == MaxSteps || this.countSteps == MinSteps)
            {
                this.MoveRight = !this.MoveRight;
            }
        }

        /// <summary>
        ///     Creates the and place all enemy ships.
        /// </summary>
        public void CreateAndPlaceAllEnemyShips()
        {
            this.createAllEnemyShips();
            this.positionEnemies();
        }

        /// <summary>
        ///     Called when [tick].
        /// </summary>
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
            if (this.background.Children.Contains(this.BonusShip.Sprite) || this.BonusActive)
            {
                return;
            }

            var rand = new Random();
            if (rand.Next(100) == 1)
            {
                var startingY = 25;
                this.background.Children.Add(this.BonusShip.Sprite);
                this.BonusShip.X = 0;
                this.BonusShip.Y = startingY;
                this.sound.bonusEnemyAppears();
            }
        }

        private void createAllEnemyShips()
        {
            EnemyFactory factory = new Level4EnemyFactory();
            for (var i = 0; i < this.level4ShipCount; i++)
            {
                this.createEnemyShip(factory);
            }

            factory = new Level3EnemyFactory();
            for (var i = 0; i < this.level3ShipCount; i++)
            {
                this.createEnemyShip(factory);
            }

            factory = new Level2EnemyFactory();
            for (var i = 0; i < this.level2ShipCount; i++)
            {
                this.createEnemyShip(factory);
            }

            factory = new Level1EnemyFactory();
            for (var i = 0; i < this.level1ShipCount; i++)
            {
                this.createEnemyShip(factory);
            }
        }

        private void createEnemyShip(EnemyFactory theFactory)
        {
            var newShip = theFactory.GetEnemyShip();
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
                var shipNumberInRow = shipIndex;

                if (shipIndex < this.level4ShipCount)
                {
                    rowNumberFromTop = 1;
                    shipsPerRow = this.level4ShipCount;
                }
                else if (shipIndex < this.level4ShipCount + this.level3ShipCount)
                {
                    rowNumberFromTop = 2;
                    shipsPerRow = this.level3ShipCount;
                    shipNumberInRow -= this.level4ShipCount;
                }
                else if (shipIndex < this.level4ShipCount + this.level3ShipCount + this.level2ShipCount)
                {
                    rowNumberFromTop = 3;
                    shipsPerRow = this.level2ShipCount;
                    shipNumberInRow -= this.level4ShipCount + this.level3ShipCount;
                }
                else
                {
                    rowNumberFromTop = 4;
                    shipsPerRow = this.level1ShipCount;
                    shipNumberInRow -= this.level4ShipCount + this.level3ShipCount + this.level2ShipCount;
                }

                var distanceFromLeft =
                    (this.backgroundWidth - shipsPerRow * shipWidth - (shipsPerRow - 1) * shipGap) / 2;
                this.AllEnemies[shipIndex].Y =
                    (this.AllEnemies[shipIndex].Height + VerticalSpaceBetweenRows) * rowNumberFromTop;
                this.AllEnemies[shipIndex].X = distanceFromLeft + (shipGap + shipWidth) * shipNumberInRow;
            }
        }

        /// <summary>
        ///     Chooses the enemy and shoot.
        /// </summary>
        public void ChooseEnemyAndShoot()
        {
            if (this.background.Children.Contains(this.BonusShip.Sprite))
            {
                this.Shoot(this.BonusShip);
            }
            else
            {
                var enemy = this.chooseRandomEnemy();
                if (enemy != null)
                {
                    this.Shoot(enemy);
                }
            }
        }

        private EnemyShip chooseRandomEnemy()
        {
            var shootingEnemies = 0;

            foreach (var enemy in this.AllEnemies)
            {
                if (enemy.CanShoot)
                {
                    shootingEnemies++;
                }
            }

            if (shootingEnemies > 0)
            {
                var random = new Random();
                return this.AllEnemies[random.Next(shootingEnemies)];
            }

            return null;
        }

        /// <summary>
        ///     Shoots the specified enemy.
        ///     Precondition: enemy != null
        ///     Postcondition: Choose an enemy and let the enemy fire bullets.
        /// </summary>
        /// <param name="enemy">The enemy.</param>
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

        public void GenerateNewLevel(int currentLevel)
        {
            this.level = currentLevel;
            switch (currentLevel)
            {
                case 2:
                    this.level1ShipCount = 4;
                    this.level2ShipCount = 6;
                    this.level3ShipCount = 8;
                    this.level4ShipCount = 10;
                    this.MoveRight = true;
                    this.countSteps = MaxSteps / 2;
                    this.createAllEnemyShips();
                    this.positionEnemies();
                    break;
                case 3:
                    this.level1ShipCount = 10;
                    this.level2ShipCount = 8;
                    this.level3ShipCount = 6;
                    this.level4ShipCount = 4;
                    this.MoveRight = true;
                    this.stepCloser = true;
                    this.countSteps = MaxSteps / 2;
                    this.createAllEnemyShips();
                    this.positionEnemies();
                    break;
            }
        }

        #endregion
    }
}
