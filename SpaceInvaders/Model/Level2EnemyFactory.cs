using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Model
{
    public class Level2EnemyFactory : EnemyFactory
    {
        public override EnemyShip GetEnemyShip()
        {
            return new Level2Enemy();
        }
    }
}
