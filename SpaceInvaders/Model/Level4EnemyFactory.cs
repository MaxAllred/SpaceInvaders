namespace SpaceInvaders.Model
{
    public class Level4EnemyFactory : EnemyFactory
    {
        public override EnemyShip GetEnemyShip()
        {
            return new Level4Enemy();
        }
    }
}