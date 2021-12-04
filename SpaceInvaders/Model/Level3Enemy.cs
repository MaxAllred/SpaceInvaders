using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    public class Level3Enemy : EnemyShip
    {
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
