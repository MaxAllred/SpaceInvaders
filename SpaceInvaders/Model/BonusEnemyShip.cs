using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the bonus enemy ship.
    /// </summary>
    public class BonusEnemyShip : EnemyShip
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BonusEnemyShip" /> class.
        /// </summary>
        public BonusEnemyShip()
        {
            Sprite = new BonusEnemyShipSprite();
            PointValue = 50;
            CanShoot = true;

            SetSpeed(12, 1);
        }

        #endregion
    }
}