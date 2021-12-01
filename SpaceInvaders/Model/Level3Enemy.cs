using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    class Level3Enemy : EnemyShip
    {
        public Level3Enemy()
        {
            this.Sprite1 = new Level3EnemySprite();
            this.Sprite2 = new Level3EnemySprite2();
            this.PointValue = 15;
            this.CanShoot = true;
            Sprite = this.Sprite1;
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}
