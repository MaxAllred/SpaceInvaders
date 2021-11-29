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

        public bool CanShoot { get; protected set; }
        public int PointValue { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Enemy" /> class.
        /// </summary>
        public Enemy(EnemyShipVersion enemyVersion)
        {
            if (enemyVersion == EnemyShipVersion.LevelOne)
            {
                Sprite = new Level1EnemySprite();
                PointValue = Level1EnemyPoints;
                CanShoot = false;
            }else if (enemyVersion == EnemyShipVersion.LevelTwo)
            {
                Sprite = new Level2EnemySprite();
                PointValue = Level2EnemyPoints;
                CanShoot = false;
            } else if (enemyVersion == EnemyShipVersion.LevelThree)
            {
                Sprite = new Level3EnemySprite();
                PointValue = Level3EnemyPoints;
                CanShoot = true;
            }
            else if (enemyVersion == EnemyShipVersion.LevelFour)
            {
                Sprite = new Level4EnemySprite();
                PointValue = Level4EnemyPoints;
                CanShoot = true;
            }
            
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        #endregion
    }
}