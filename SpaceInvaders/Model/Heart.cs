using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the Heart.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public class Heart : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 0;
        private const int SpeedYDirection = 0;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Heart" /> class.
        /// </summary>
        public Heart()
        {
            Sprite = new HeartSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        #endregion
    }
}