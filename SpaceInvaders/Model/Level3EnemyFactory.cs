namespace SpaceInvaders.Model
{
    public class Level3EnemyFactory : EnemyFactory
    {
        public override EnemyShip GetEnemyShip()
        {
            return new Level3Enemy();
        }
    }
}