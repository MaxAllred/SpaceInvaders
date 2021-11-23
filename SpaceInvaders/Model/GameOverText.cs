using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the player ship.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public class GameOverText : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 0;
        private const int SpeedYDirection = 0;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShip" /> class.
        /// </summary>
        public GameOverText(int winTrue)
        {
            if (winTrue == 1)
            {
                Sprite = new CongratsSprite();
            }
            else
            {
                Sprite = new GameOverSprite();
            }
            
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        #endregion
    }
}