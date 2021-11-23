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

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Enemy" /> class.
        /// </summary>
        public Enemy(int enemyLevel)
        {
            if (enemyLevel == 1)
            {
                Sprite = new Level1EnemySprite();
            }else if (enemyLevel == 2)
            {
                Sprite = new Level2EnemySprite();
            } else if (enemyLevel == 3)
            {
                Sprite = new Level3EnemySprite();
            }
            else if (enemyLevel == 4)
            {
                Sprite = new Level4EnemySprite();
                
            }
            
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        #endregion
    }
}