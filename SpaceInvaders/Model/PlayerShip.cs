using Windows.UI.Xaml;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the player ship.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public class PlayerShip : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 3;
        private const int SpeedYDirection = 0;

        /// <summary>
        ///     Create first sprite.
        /// </summary>
        public BaseSprite Sprite1 { get; protected set; }

        /// <summary>
        ///     Create second sprite.
        /// </summary>
        public BaseSprite Sprite2 { get; protected set; }
        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShip" /> class.
        /// </summary>
        public PlayerShip()
        {
            this.Sprite1 = new PlayerShipSprite();
            this.Sprite2 = new PlayerShipInvincibleSprite();
            this.Sprite2.Visibility = Visibility.Collapsed;
            
            Sprite = this.Sprite1;
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        /// <summary>
        ///     Toggles the invincible.
        /// </summary>
        public void ToggleInvincible()
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