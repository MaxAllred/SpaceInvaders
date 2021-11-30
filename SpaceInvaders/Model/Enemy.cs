using Windows.UI.Xaml;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the Level 1 Enemy ship.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public class Enemy : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 5;
        private const int SpeedYDirection = 0;
        private const int Level1EnemyPoints = 1;
        private const int Level2EnemyPoints = 2;
        private const int Level3EnemyPoints = 3;
        private const int Level4EnemyPoints = 4;

        public readonly BaseSprite Sprite1;
        public readonly BaseSprite Sprite2;

        public bool CanShoot { get; protected set; }
        public int PointValue { get; protected set; }

        /// <summary>
        ///     Gets or sets the enemy ship version.
        /// </summary>
        /// <value>
        ///     The enemy version.
        /// </value>
        public EnemyShipVersion EnemyVersion { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Enemy" /> class.
        /// </summary>
        public Enemy(EnemyShipVersion enemyVersion)
        {
            if (enemyVersion == EnemyShipVersion.LevelOne)
            {
                this.Sprite1 = new Level1EnemySprite();
                this.Sprite2 = new Level1EnemySprite2();
                this.PointValue = Level1EnemyPoints;
                this.CanShoot = false;
            } else if (enemyVersion == EnemyShipVersion.LevelTwo)
            {
                this.Sprite1 = new Level2EnemySprite();
                this.Sprite2 = new Level2EnemySprite2();
                this.PointValue = Level2EnemyPoints;
                this.CanShoot = false;
            } else if (enemyVersion == EnemyShipVersion.LevelThree)
            {
                this.Sprite1 = new Level3EnemySprite();
                this.Sprite2 = new Level3EnemySprite2();
                this.PointValue = Level3EnemyPoints;
                this.CanShoot = true;
            } else if (enemyVersion == EnemyShipVersion.LevelFour)
            {
                this.Sprite1 = new Level4EnemySprite();
                this.Sprite2 = new Level4EnemySprite2();
                this.PointValue = Level4EnemyPoints;
                this.CanShoot = true;
            }

            Sprite = this.Sprite1;

            this.EnemyVersion = enemyVersion;
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        public void Animate()
        {
            if (Sprite.Equals(this.Sprite1))
            {
                Sprite = this.Sprite2;
                this.Sprite1.Visibility = Visibility.Collapsed;
                this.Sprite2.Visibility = Visibility.Visible;
            }
            else
            {
                Sprite = this.Sprite1;
                this.Sprite2.Visibility = Visibility.Collapsed;
                this.Sprite1.Visibility = Visibility.Visible;
            }
        }

        #endregion
    }
}