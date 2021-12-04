namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages a Level 1 Enemy Factory.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.EnemyFactory" />
    public class Level1EnemyFactory : EnemyFactory
    {
        /// <summary>
        ///     Gets the enemy ship.
        /// </summary>
        /// <returns>
        ///     An enemy ship.
        /// </returns>
        public override EnemyShip GetEnemyShip()
        {
            return new Level1Enemy();
        }
    }
}