using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages a Sound manager for each sound to be played for playership, enemies, bullets, and collisions.
    /// </summary>
    public class SoundManager
    {
        private SoundPlayer sp;

        /// <summary>
        ///     Plays the player shot sound.
        /// </summary>
        public void playerShot()
        {
            this.sp = new SoundPlayer();
            this.sp.Play(@"Assets/Audio/Flash-laser-03.wav", "playerShot");
        }

        /// <summary>
        ///  Plays the enemy shot sound.
        /// </summary>
        public void enemyShot()
        {
            this.sp = new SoundPlayer();
            this.sp.Play(@"Assets/Audio/Flash-laser-09.wav", "enemyShot");
        }

        /// <summary>
        ///     Plays the bonus enemy shot.
        /// </summary>
        public void bonusEnemyShot()
        {
            this.sp = new SoundPlayer();
            this.sp.Play(@"Assets/Audio/mixkit-game-whip-shot-1512.wav", "bonusEnemyShot");
        }

        /// <summary>
        ///     Plays the player bullet hit sound.
        /// </summary>
        public void playerBulletHit()
        {
            this.sp = new SoundPlayer();
            this.sp.Play(@"Assets/Audio/Boink-single-high-01.wav", "playerShotHit");
        }

        /// <summary>
        /// Plays the enemy bullet hit sound.
        /// </summary>
        public void enemyBulletHit()
        {
            this.sp = new SoundPlayer();
            this.sp.Play(@"Assets/Audio/Rubber-snap-01.wav", "enemyShotHit");
        }

        /// <summary>
        ///     Plays the bonus enemy appears sound.
        /// </summary>
        public void bonusEnemyAppears()
        {
            this.sp = new SoundPlayer();
            this.sp.Play(@"Assets/Audio/Game-Bonus-Sound-Effect.wav", "bonusEnemyAppears");
        }

        /// <summary>
        ///     Plays the game over sound.
        /// </summary>
        public void gameOver()
        {
            this.sp = new SoundPlayer();
            this.sp.Play(@"Assets/Audio/gameover.wav", "gameOver");
        }

        /// <summary>
        ///  Plays the you win sound.
        /// </summary>
        public void youWin()
        {
            this.sp = new SoundPlayer();
            this.sp.Play(@"Assets/Audio/mixkit-video-game-win-2016.wav", "youWin");
        }

        public void invincible()
        {
            this.sp = new SoundPlayer();
            this.sp.Play(@"Assets/Audio/invincible-sound.wav", "invincible");
        }
    }
}
