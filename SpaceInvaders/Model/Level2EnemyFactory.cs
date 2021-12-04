namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages a Level 2 Enemy Factory.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.EnemyFactory" />
    public class Level2EnemyFactory : EnemyFactory
    {
        /// <summary>
        ///     Gets the enemy ship.
        /// </summary>
        /// <returns>
        ///     An enemy ship.
        /// </returns>
        public override EnemyShip GetEnemyShip()
        {
            return new Level2Enemy();
        }
    }
}
