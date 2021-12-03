using System.Threading;
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

        public BaseSprite Sprite1 { get; protected set; }
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
            
            Sprite = this.Sprite1;
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
        public void ToggleInvincible()
        {
            return;
        }

        #endregion
    }
}