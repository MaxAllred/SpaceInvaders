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
                PointValue = 1;
            }else if (enemyVersion == EnemyShipVersion.LevelTwo)
            {
                Sprite = new Level2EnemySprite();
                PointValue = 2;
            } else if (enemyVersion == EnemyShipVersion.LevelThree)
            {
                Sprite = new Level3EnemySprite();
                PointValue = 3;
            }
            else if (enemyVersion == EnemyShipVersion.LevelFour)
            {
                Sprite = new Level4EnemySprite();
                PointValue = 4;

            }
            
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        #endregion
    }
}