using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.VoiceCommands;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the entire game.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private const double PlayerShipBottomOffset = 30;

        private const int HitBoxBuffer = 10;
        private const double TextBottomOffset = 150;
        private static readonly int startingLives = 3;
        private static readonly int bulletCount = 3;
        private static readonly int numberOfShields = 3;
        public int Score;
        public bool GameOver;
        public bool PowerUp;
        public int level;
        public EnemyManager EnemyManager;
        private int activeBullets;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private Collection<Heart> playerLives;
        private Collection<Shield> shields;

        private PlayerShip playerShip;

        private Collection<Bullet> playerBullets;

        private Canvas background;
        private bool allEliminated;
        private TextBlock scoreTextBlock;
        private SoundManager sound;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        ///     Precondition: backgroundHeight > 0 AND backgroundWidth > 0
        /// </summary>
        /// <param name="backgroundHeight">The backgroundHeight of the game play window.</param>
        /// <param name="backgroundWidth">The backgroundWidth of the game play window.</param>
        public GameManager(double backgroundHeight, double backgroundWidth)
        {
            if (backgroundHeight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundHeight));
            }

            if (backgroundWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundWidth));
            }

            this.PowerUp = false;
            this.backgroundHeight = backgroundHeight;
            this.backgroundWidth = backgroundWidth;
        }

        #endregion

        #region Methods

        public event EventHandler ScoreChanged;

        /// <summary>Moves enemy ships and all active bullets</summary>
        public void MoveElements()
        {
            foreach (var bullet in this.playerBullets)
            {
                if (this.background.Children.Contains(bullet.Sprite))
                {
                    bullet.MoveUp();
                }
            }
        }

        /// <summary>
        ///     Initializes the game placing player ship and enemy ship in the game.
        ///     Precondition: background != null
        ///     Postcondition: Game is initialized and ready for play.
        /// </summary>
        /// <param name="theBackground">The background canvas.</param>
        public void InitializeGame(Canvas theBackground)
        {
            this.level = 1;
            this.sound = new SoundManager();
            this.background = theBackground ?? throw new ArgumentNullException(nameof(theBackground));
            this.createAndPlacePlayerShip();
            this.playerLives = new Collection<Heart>();
            this.createAndPlaceHearts();
            this.shields = new Collection<Shield>();
            this.createAndPlaceShields();
            this.EnemyManager = new EnemyManager(this.background);
            this.EnemyManager.CreateAndPlaceAllEnemyShips();
            this.playerBullets = new Collection<Bullet>();
            this.createPlayerBullets();
        }

        private void createAndPlaceShields()
        {
            for (var i = 0; i < numberOfShields; i++)
            {
                var newShield = new Shield();
                this.shields.Add(newShield);
                this.background.Children.Add(newShield.Intact);
                this.background.Children.Add(newShield.HitOnce);
                this.background.Children.Add(newShield.HitTwice);
            }

            this.placeShields();
        }

        private void placeShields()
        {
            if (this.shields.Count == 0)
            {
                return;
            }

            double shieldWidth = this.shields[0].Width;
            double distanceFromBottom = this.backgroundHeight - (PlayerShipBottomOffset * 4);
            double gap = (this.backgroundWidth - (shieldWidth * this.shields.Count)) / (this.shields.Count + 1);
            

            for (int shieldIndex = 0; shieldIndex < this.shields.Count; shieldIndex++)
            {
                this.shields[shieldIndex].Y = distanceFromBottom;
                this.shields[shieldIndex].X = ((shieldIndex + 1) * gap) + (shieldIndex * shieldWidth);
            }
        }

        private void createPlayerBullets()
        {
            for (var i = 0; i < bulletCount; i++)
            {
                this.playerBullets.Add(new Bullet());
            }
        }

        private void createAndPlaceHearts()
        {
            for (var i = 0; i < startingLives; i++)
            {
                var heart = new Heart();
                this.background.Children.Add(heart.Sprite);
                this.playerLives.Add(heart);
            }

            this.placeHeartsNearBottomLeft();
        }

        private void placeHeartsNearBottomLeft()
        {
            var offset = this.playerLives[0].Width;
            foreach (var current in this.playerLives)
            {
                current.X = offset;
                offset += current.Width * 1.25;
                current.Y = this.backgroundHeight - current.Height;
            }
        }

        internal void EndPowerUp()
        {
            this.PowerUp = false;
            this.playerShip.ToggleInvincible();
            this.EnemyManager.BonusActive = false;
            this.playerShip.MoveRight();
        }

        private void createAndPlacePlayerShip()
        {
            this.playerShip = new PlayerShip();
            this.background.Children.Add(this.playerShip.Sprite);
            this.background.Children.Add(this.playerShip.Sprite2);

            this.placePlayerShipNearBottomOfBackgroundCentered();
        }

        private void placePlayerShipNearBottomOfBackgroundCentered()
        {
            this.playerShip.X = this.backgroundWidth / 2 - this.playerShip.Width / 2.0;
            this.playerShip.Y = this.backgroundHeight - this.playerShip.Height - PlayerShipBottomOffset;
        }

        /// <summary>
        ///     Moves the player ship to the left.
        ///     Precondition: none
        ///     Postcondition: The player ship has moved left.
        /// </summary>
        public void MovePlayerShipLeft()
        {
            if (this.playerShip.X - this.playerShip.SpeedX > 0)
            {
                this.playerShip.MoveLeft();
            }
        }

        /// <summary>
        ///     Moves the player ship to the right.
        ///     Precondition: none
        ///     Postcondition: The player ship has moved right.
        /// </summary>
        public void MovePlayerShipRight()
        {
            this.playerShip.MoveRight();
        }

        public void FirePlayerBullet()
        {
            if (this.activeBullets < bulletCount)
            {
                this.background.Children.Add(this.playerBullets[this.activeBullets].Sprite);
                this.playerBullets[this.activeBullets].X = this.playerShip.X + this.playerShip.Width / 2 -
                                                           this.playerBullets[this.activeBullets].Width / 2;
                this.playerBullets[this.activeBullets].Y = this.playerShip.Y - this.playerShip.Height;
                this.activeBullets++;

                this.sound.playerShot();

                return;
            }

            for (var i = 0; i < this.activeBullets; i++)
            {
                if (this.playerBullets[i].Y <= 0)
                {
                    this.playerBullets[i].X =
                        this.playerShip.X + this.playerShip.Width / 2 - this.playerBullets[i].Width / 2;
                    this.playerBullets[i].Y = this.playerShip.Y - this.playerShip.Height;
                    this.sound.playerShot();
                    return;
                }
            }
        }

        /// <summary>Checks for collisions.</summary>
        public void CheckForCollisions()
        {
            this.checkForPlayerBulletCollision();
            this.checkAllEliminated();
            this.checkForEnemyCollision();
            this.checkShieldHitFromPlayerOrEnemy();
        }

        private void checkForPlayerBulletCollision()
        {
            for (var bulletIndex = 0; bulletIndex < this.playerBullets.Count; bulletIndex++)
            {
                var bullet = this.playerBullets[bulletIndex];
                for (var enemyShipIndex = 0; enemyShipIndex < this.EnemyManager.AllEnemies.Count; enemyShipIndex++)
                {
                    var currentEnemy = this.EnemyManager.AllEnemies[enemyShipIndex];
                    if (bullet.CheckForCollision(currentEnemy))
                    {
                        this.Score += currentEnemy.PointValue;
                        this.EnemyManager.AllEnemies.Remove(currentEnemy);
                        this.registerHit(currentEnemy.Sprite, bullet);
                    }
                }

                if (this.background.Children.Contains(this.EnemyManager.BonusShip.Sprite))
                {
                    if (bullet.CheckForCollision(this.EnemyManager.BonusShip))
                    {
                        this.Score += this.EnemyManager.BonusShip.PointValue;
                        this.registerHit(this.EnemyManager.BonusShip.Sprite, bullet);
                        this.PowerUp = true;
                        this.sound.invincible();
                        this.playerShip.ToggleInvincible();
                        this.EnemyManager.BonusActive = true;
                    }
                }
            }
        }

        private void checkShieldHitFromPlayerOrEnemy()
        {
            foreach (var bullet in this.playerBullets)
            {
                if (this.checkShieldHit(bullet))
                {
                    bullet.Y = 0 - bullet.Height;
                    this.sound.playerBulletHit();
                }
            }

            if (checkShieldHit(this.EnemyManager.EnemyBullet))
            {
                this.EnemyManager.EnemyBullet.Y = this.backgroundHeight;
                this.background.Children.Remove(this.EnemyManager.EnemyBullet.Sprite);
                this.sound.enemyBulletHit();
            }
        }

        private bool checkShieldHit(Bullet theBullet)
        {
            bool returnBool = false;

            foreach (var shield in this.shields)
            {
                if (theBullet.CheckForCollision(shield) && shield.Sprite.Visibility == Visibility.Visible)
                {
                    shield.HandleHit();
                    returnBool = true;
                }

                if (shield.isDestroyed && this.background.Children.Contains(shield.Sprite))
                {
                    this.background.Children.Remove(shield.Sprite);
                }
            }

            return returnBool;
        }

        private void registerHit(BaseSprite currentSprite, Bullet theBullet)
        {
            this.sound.playerBulletHit();
            this.background.Children.Remove(currentSprite);

            theBullet.Y = 0 - theBullet.Height;

            this.background.Children.Remove(this.scoreTextBlock);
            this.scoreTextBlock = new TextBlock
            {
                Text = "Score: " + this.Score,
                HorizontalTextAlignment = TextAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White)
            };
            this.background.Children.Add(this.scoreTextBlock);
        }

        private void checkForEnemyCollision()
        {
            if (this.EnemyManager.EnemyBullet.CheckForCollision(this.playerShip))
            {
                this.registerHitFromEnemy();
            }
        }

        private void registerHitFromEnemy()
        {
            this.EnemyManager.EnemyBullet.Y = this.backgroundHeight;
            this.background.Children.Remove(this.EnemyManager.EnemyBullet.Sprite);
            this.sound.enemyBulletHit();
            if (!this.PowerUp)
            {
                this.background.Children.Remove(this.playerLives[this.playerLives.Count - 1].Sprite);
                this.playerLives.RemoveAt(this.playerLives.Count - 1);
            }

            if (this.playerLives.Count == 0)
            {
                this.sound.gameOver();
                this.background.Children.Remove(this.playerShip.Sprite);
                this.background.Children.Remove(this.EnemyManager.EnemyBullet.Sprite);
                this.GameOver = true;
                this.EnemyManager.CeaseFire = true;
            }
        }

        private void checkAllEliminated()
        {
            if (this.EnemyManager.AllEnemies.Count == 0 && this.level >= 3)
            {
                this.sound.youWin();
                this.allEliminated = true;
                this.GameOver = true;
            }
            else if (this.EnemyManager.AllEnemies.Count == 0)
            {
                foreach (var currentBullet in this.playerBullets)
                {
                    currentBullet.Y = 0 - currentBullet.Height;
                }

                this.level++;
                this.EnemyManager.GenerateNewLevel(this.level);
            }
        }

        public void HandleGameOver()
        {
            var gameOverText = new GameOverText(0);

            if (this.allEliminated)
            {
                gameOverText = new GameOverText(1);
            }

            this.background.Children.Add(gameOverText.Sprite);
            gameOverText.X = this.backgroundWidth / 2 - gameOverText.Width / 2;

            gameOverText.Y = TextBottomOffset;
        }

        #endregion
    }
}