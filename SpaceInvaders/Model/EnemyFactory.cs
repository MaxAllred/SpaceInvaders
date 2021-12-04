namespace SpaceInvaders.Model
{
    public abstract class EnemyFactory
    {
        #region Methods

        public abstract EnemyShip GetEnemyShip();

        #endregion
    }
}