using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    public class Level1Enemy : EnemyShip
    {
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
