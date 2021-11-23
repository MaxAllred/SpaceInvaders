﻿using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the Bullet.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public class Bullet : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 0;
        private const int SpeedYDirection = 20;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Bullet" /> class.
        /// </summary>
        public Bullet()
        {
            Sprite = new BulletSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        #endregion
    }
}