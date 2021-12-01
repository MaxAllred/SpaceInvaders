using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    class Level2Enemy : EnemyShip
    {
        public Level2Enemy()
        {
            this.Sprite1 = new Level2EnemySprite();
            this.Sprite2 = new Level2EnemySprite2();
            this.PointValue = 10;
            this.CanShoot = false;
            Sprite = this.Sprite1;
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}
