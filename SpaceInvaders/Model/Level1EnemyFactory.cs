namespace SpaceInvaders.Model
{
    public class Level1EnemyFactory : EnemyFactory
    {
        public override EnemyShip GetEnemyShip()
        {
            return new Level1Enemy();
        }
    }
}