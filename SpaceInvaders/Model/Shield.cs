using System.Threading;
using Windows.UI.Xaml;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the player ship.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public class Shield : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 0;
        private const int SpeedYDirection = 0;
        private const int MaxShieldHealth = 3;
        private int health;

        public bool IsDestroyed
        {
            get
            {
                if (this.health <= 0)
                {
                    return true;
                }
                return false;
            }
        }

        public BaseSprite Intact { get; protected set; }
        public BaseSprite HitOnce { get; protected set; }
        public BaseSprite HitTwice { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShip" /> class.
        /// </summary>
        public Shield()
        {
            this.Intact = new ShieldIntact();
            this.HitOnce = new ShieldHitOnce();
            this.HitTwice = new ShieldHitTwice();
            this.HitOnce.Visibility = Visibility.Collapsed;
            this.HitTwice.Visibility = Visibility.Collapsed;
            this.health = MaxShieldHealth;
            Sprite = this.Intact;
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
        /// <summary>Determines whether this instance is hit.</summary>
        /// <returns>
        ///   <c>true if the shield has been hit for the final time, false otherwise</c>
        /// </returns>
        public void HandleHit()
        {


            if (Sprite.Equals(this.Intact))
            {
                Sprite = this.HitOnce;
                this.Intact.Visibility = Visibility.Collapsed;
                this.HitOnce.Visibility = Visibility.Visible;
            }
            else if(Sprite.Equals(this.HitOnce))
            {
                Sprite = this.HitTwice;
                this.HitOnce.Visibility = Visibility.Collapsed;
                this.HitTwice.Visibility = Visibility.Visible;
            }
            else
            {
                Sprite.Visibility = Visibility.Collapsed;
            }
            X = X;
            this.health--;
        }

        #endregion
    }
}