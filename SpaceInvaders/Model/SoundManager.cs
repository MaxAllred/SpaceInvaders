using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Model
{
    class SoundManager
    {
        private SoundPlayer sp;

        public void playerShot()
        {
            sp = new SoundPlayer();
            sp.Play(@"Assets/Audio/Flash-laser-03.wav", "playerShot");
        }
    }
}
