using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Model
{
    public abstract class EnemyFactory
    {
        public abstract EnemyShip GetEnemyShip();
    }
}
