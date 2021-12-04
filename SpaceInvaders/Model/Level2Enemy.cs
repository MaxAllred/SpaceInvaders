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
            Sprite1 = new Level2EnemySprite();
            Sprite2 = new Level2EnemySprite2();
            PointValue = 10;
            CanShoot = false;
            Sprite = Sprite1;
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}
