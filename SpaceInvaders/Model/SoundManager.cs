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
        public void enemyShot()
        {
            sp = new SoundPlayer();
            sp.Play(@"Assets/Audio/Flash-laser-09.wav", "enemyShot");
        }
        public void playerBulletHit()
        {
            sp = new SoundPlayer();
            sp.Play(@"Assets/Audio/Boink-single-high-01.wav", "playerShotHit");
        }
        public void enemyBulletHit()
        {
            sp = new SoundPlayer();
            sp.Play(@"Assets/Audio/Rubber-snap-01.wav", "enemyShotHit");
        }

        public void gameOver()
        {
            sp = new SoundPlayer();
            sp.Play(@"Assets/Audio/gameover.wav", "gameOver");
        }
    }
}
