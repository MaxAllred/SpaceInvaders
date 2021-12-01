using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    class Level4Enemy : EnemyShip
    {
        public Level4Enemy()
        {
            this.Sprite1 = new Level4EnemySprite();
            this.Sprite2 = new Level4EnemySprite2();
            this.PointValue = 20;
            this.CanShoot = true;
            Sprite = this.Sprite1;
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}
