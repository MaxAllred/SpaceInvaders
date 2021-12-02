using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the bonus enemy ship.
    /// </summary>
    public class BonusEnemyShip : EnemyShip
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BonusEnemyShip"/> class.
        /// </summary>
        public BonusEnemyShip()
        {
            Sprite = new BonusEnemyShipSprite();
            this.PointValue = 50;
            this.CanShoot = true;

            SetSpeed(12, 1);
        }
    }
}
