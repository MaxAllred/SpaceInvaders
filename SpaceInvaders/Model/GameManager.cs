using System;
using System.Collections.ObjectModel;
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
        public int Score;
        public bool GameOver;
        public event EventHandler ScoreChanged;

        public EnemyManager EnemyManager;
        private int activeBullets;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private Collection<Heart> playerLives;

        private PlayerShip playerShip;

        private Collection<Bullet> playerBullets;

        private Canvas background;
        private bool allEliminated;
        private TextBlock scoreTextBlock;

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

            this.backgroundHeight = backgroundHeight;
            this.backgroundWidth = backgroundWidth;
        }

        #endregion

        #region Methods

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

            if (this.background.Children.Contains(this.EnemyManager.EnemyBullet.Sprite))
            {
                this.EnemyManager.EnemyBullet.MoveDown();
            }

            this.EnemyManager.MoveAllEnemies();
        }

        /// <summary>
        ///     Initializes the game placing player ship and enemy ship in the game.
        ///     Precondition: background != null
        ///     Postcondition: Game is initialized and ready for play.
        /// </summary>
        /// <param name="theBackground">The background canvas.</param>
        public void InitializeGame(Canvas theBackground)
        {
            this.background = theBackground ?? throw new ArgumentNullException(nameof(theBackground));
            this.createAndPlacePlayerShip();
            this.playerLives = new Collection<Heart>();
            this.createAndPlaceHearts();
            this.EnemyManager = new EnemyManager(this.background);
            this.EnemyManager.CreateAndPlaceAllEnemyShips();
            this.playerBullets = new Collection<Bullet>();
            this.createPlayerBullets();
            this.EnemyManager.EnemyBullet = new Bullet();
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

        private void createAndPlacePlayerShip()
        {
            this.playerShip = new PlayerShip();
            this.background.Children.Add(this.playerShip.Sprite);

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
            this.playerShip.MoveLeft();
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
                return;
            }

            for (var i = 0; i < this.activeBullets; i++)
            {
                if (this.playerBullets[i].Y <= 0)
                {
                    this.playerBullets[i].X =
                        this.playerShip.X + this.playerShip.Width / 2 - this.playerBullets[i].Width / 2;
                    this.playerBullets[i].Y = this.playerShip.Y - this.playerShip.Height;
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
        }

        private void checkForPlayerBulletCollision()
        {
            for (var bulletIndex = 0; bulletIndex < this.playerBullets.Count; bulletIndex++)
            {
                Bullet bullet = this.playerBullets[bulletIndex];
                for (var enemyShipIndex = 0; enemyShipIndex < this.EnemyManager.AllEnemies.Count; enemyShipIndex++)
                {
                    EnemyShip currentEnemy = this.EnemyManager.AllEnemies[enemyShipIndex];
                    if (bullet.CheckForCollision(currentEnemy))
                    {
                        this.Score += currentEnemy.PointValue;
                        this.EnemyManager.AllEnemies.Remove(currentEnemy);
                        this.registerHit(currentEnemy.Sprite, bulletIndex);
                    }
                }
            }
        }

        protected virtual void OnScoreChanged(EventArgs e)
        {
            EventHandler handler = ScoreChanged;
            handler?.Invoke(this, e);
        }

        private void registerHit(BaseSprite currentSprite, int bulletNumber)
        {
            this.background.Children.Remove(currentSprite);

            this.playerBullets[bulletNumber].Y = 0 - GameObject.VerticalScreenOffset - this.playerBullets[bulletNumber].Height;

            this.background.Children.Remove(this.scoreTextBlock);
            this.scoreTextBlock = new TextBlock
            {
                Text = "Score: " + this.Score,
                HorizontalTextAlignment = TextAlignment.Center,
               Foreground = new SolidColorBrush(Windows.UI.Colors.White)
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
            this.background.Children.Remove(this.playerLives[this.playerLives.Count - 1].Sprite);
            this.playerLives.RemoveAt(this.playerLives.Count - 1);
            this.EnemyManager.EnemyBullet.Y = this.backgroundHeight;
            this.background.Children.Remove(this.EnemyManager.EnemyBullet.Sprite);

            if (this.playerLives.Count == 0)
            {
                this.background.Children.Remove(this.playerShip.Sprite);
                this.background.Children.Remove(this.EnemyManager.EnemyBullet.Sprite);
                this.GameOver = true;
            }
        }

        private void checkAllEliminated()
        {
            if (this.EnemyManager.AllEnemies.Count == 0)
            {
                this.allEliminated = true;
                this.GameOver = true;
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
            gameOverText.X = this.backgroundWidth / 2 - gameOverText.Width / 2 - GameObject.HorizontalScreenOffset;

            gameOverText.Y = TextBottomOffset - GameObject.VerticalScreenOffset;
        }

        #endregion
    }
}