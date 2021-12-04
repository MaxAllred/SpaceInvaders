using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages a level 4 enemy ship.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.EnemyShip" />
    public class Level4Enemy : EnemyShip
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Level4Enemy"/> class.
        /// </summary>
        public Level4Enemy()
        {
            Sprite1 = new Level4EnemySprite();
            Sprite2 = new Level4EnemySprite2();
            PointValue = 20;
            CanShoot = true;
            Sprite = Sprite1;
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}
