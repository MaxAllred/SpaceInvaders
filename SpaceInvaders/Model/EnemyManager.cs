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

        /// <summary>The enemy bullet</summary>
        public Bullet EnemyBullet;
        private bool moveRight = true;
        /// <summary>Determines if the enemies can fire</summary>
        public bool CeaseFire = false;
        /// <summary>Determines if the player ship can be hit</summary>
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

        private GameObject playerShip;

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
                this.EnemyBullet.MoveTargeted();

                if (this.EnemyBullet.Y > this.backgroundHeight)
                {
                    this.background.Children.Remove(this.EnemyBullet.Sprite);
                }
            }
        }

        private void level1Movement()
        {
            if (this.moveRight)
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
                this.moveRight = !this.moveRight;
            }
        }

        private void level2Movement()
        {
            if (this.moveRight)
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
                this.moveRight = !this.moveRight;
            }
        }

        private void level3Movement()
        {
            if (this.moveRight)
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
                this.moveRight = !this.moveRight;
            }
        }

        /// <summary>Sets the location of the player ship</summary>
        /// <param name="thePlayerShip">The player ship.</param>
        public void setPlayerLocation(GameObject thePlayerShip)
        {
            if (thePlayerShip == null)
            {
                return;
            }
            
            this.playerShip = thePlayerShip;
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
                this.targetedShot(this.BonusShip);
            }
            else
            {
                var enemy = this.chooseRandomEnemy();
                if (enemy != null)
                {
                    this.targetedShot(enemy);
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
        ///     Fires a targeted shot from the specified enemy.
        ///     Precondition: enemy != null
        ///     Postcondition: Choose an enemy and let the enemy fire bullets.
        /// </summary>
        /// <param name="enemy">The enemy.</param>
        public void targetedShot(EnemyShip enemy)
        {
            if (this.CeaseFire || enemy == null) 
            {
                return;
            }

            if (!this.background.Children.Contains(this.EnemyBullet.Sprite))
            {
                this.sound.enemyShot();
                this.background.Children.Add(this.EnemyBullet.Sprite);
                this.EnemyBullet.Y = enemy.Y + enemy.Height + this.EnemyBullet.Height;
                this.EnemyBullet.X = enemy.X + .5 * enemy.Width - .5 * this.EnemyBullet.Width;
                this.EnemyBullet.TargetShip(this.playerShip, enemy);
            }
        }

        /// <summary>Generates the new level based on the input number</summary>
        /// <param name="currentLevel">The level number to create</param>
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
                    this.moveRight = true;
                    this.countSteps = MaxSteps / 2;
                    this.createAllEnemyShips();
                    this.positionEnemies();
                    break;
                case 3:
                    this.level1ShipCount = 10;
                    this.level2ShipCount = 8;
                    this.level3ShipCount = 6;
                    this.level4ShipCount = 4;
                    this.moveRight = true;
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
