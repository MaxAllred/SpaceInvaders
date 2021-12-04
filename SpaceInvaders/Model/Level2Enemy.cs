using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages a level 2 enemy ship.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.EnemyShip" />
    public class Level2Enemy : EnemyShip
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Level2Enemy"/> class.
        /// </summary>
        public Level2Enemy()
        {
            Sprite1 = new Level2EnemySprite();
            Sprite2 = new Level2EnemySprite2();
            PointValue = 10;
            CanShoot = false;
            Sprite = Sprite1;
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}
