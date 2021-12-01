using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    class Level1Enemy : EnemyShip
    {
        public Level1Enemy()
        {
            this.Sprite1 = new Level1EnemySprite();
            this.Sprite2 = new Level1EnemySprite2();
            this.PointValue = 5;
            this.CanShoot = false;
            Sprite = this.Sprite1;
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}
