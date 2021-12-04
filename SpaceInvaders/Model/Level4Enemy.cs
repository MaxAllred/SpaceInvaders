using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    public class Level4Enemy : EnemyShip
    {
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
