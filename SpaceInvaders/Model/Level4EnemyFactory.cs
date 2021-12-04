namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages a Level 4 Enemy Factory.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.EnemyFactory" />
    public class Level4EnemyFactory : EnemyFactory
    {
        /// <summary>
        ///     Gets the enemy ship.
        /// </summary>
        /// <returns>
        ///     An enemy ship.
        /// </returns>
        public override EnemyShip GetEnemyShip()
        {
            return new Level4Enemy();
        }
    }
}