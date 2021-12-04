using Windows.UI.Xaml;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages an Enemy ship.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public abstract class EnemyShip : GameObject
    {
        #region Data members

        /// <summary>
        ///     Set up a speed x direction.
        /// </summary>
        protected const int SpeedXDirection = 8;

        /// <summary>
        ///     Set up a speed y direction.
        /// </summary>
        protected const int SpeedYDirection = 5;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether this instance can shoot.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance can shoot; otherwise, <c>false</c>.
        /// </value>
        public bool CanShoot { get; protected set; }

        /// <summary>
        ///     Gets or sets the point value.
        /// </summary>
        /// <value>
        ///     The point value.
        /// </value>
        public int PointValue { get; protected set; }

        /// <summary>
        ///     Gets or sets the sprite1.
        /// </summary>
        /// <value>
        ///     The sprite1.
        /// </value>
        public BaseSprite Sprite1 { get; protected set; }

        /// <summary>
        ///     Gets or sets the sprite2.
        /// </summary>
        /// <value>
        ///     The sprite2.
        /// </value>
        public BaseSprite Sprite2 { get; protected set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Animates the sprites.
        /// </summary>
        public void Animate()
        {
            if (Sprite.Equals(this.Sprite1))
            {
                Sprite = this.Sprite2;
                this.Sprite1.Visibility = Visibility.Collapsed;
                this.Sprite2.Visibility = Visibility.Visible;
            }
            else
            {
                Sprite = this.Sprite1;
                this.Sprite2.Visibility = Visibility.Collapsed;
                this.Sprite1.Visibility = Visibility.Visible;
            }
        }

        #endregion
    }
}