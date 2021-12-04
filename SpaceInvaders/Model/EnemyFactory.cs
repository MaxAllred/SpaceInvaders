namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages an Enemy Factory for the enemy ships.
    /// </summary>
    public abstract class EnemyFactory
    {
        #region Methods        
        /// <summary>
        ///     Gets the enemy ship.
        /// </summary>
        /// <returns> An enemy ship.</returns>
        public abstract EnemyShip GetEnemyShip();

        #endregion
    }
}