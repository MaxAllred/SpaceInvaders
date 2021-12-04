using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages a level 3 enemy ship.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.EnemyShip" />
    public class Level3Enemy : EnemyShip
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Level3Enemy"/> class.
        /// </summary>
        public Level3Enemy()
        {
            Sprite1 = new Level3EnemySprite();
            Sprite2 = new Level3EnemySprite2();
            PointValue = 15;
            CanShoot = true;
            Sprite = Sprite1;
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}
