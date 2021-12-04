using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages a level 1 enemy ship.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.EnemyShip" />
    public class Level1Enemy : EnemyShip
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Level1Enemy"/> class.
        /// </summary>
        public Level1Enemy()
        {
            Sprite1 = new Level1EnemySprite();
            Sprite2 = new Level1EnemySprite2();
            PointValue = 5;
            CanShoot = false;
            Sprite = Sprite1;
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}
