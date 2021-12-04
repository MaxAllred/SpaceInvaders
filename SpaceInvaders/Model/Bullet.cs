using System;
using System.ComponentModel.Design;
using System.Numerics;
using Windows.Foundation;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the Bullet.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    public class Bullet : GameObject
    {
        #region Data members

        private const int SpeedXDirection = 5;
        private const int SpeedYDirection = 20;

        enum TargetedShipDirection
        {
            left, 
            right, 
            down
        }

        private TargetedShipDirection directionToShoot;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Bullet" /> class.
        /// </summary>
        public Bullet()
        {
            Sprite = new BulletSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        public Bullet(GameObject shipToTarget)
        {
            Sprite = new BulletSprite();
        }

        #endregion

        #region Methods

        /// <summary>Checks for collision between this bullet and a ship.</summary>
        /// <param name="ship">The ship.</param>
        /// <returns>
        ///     True if the bullet intersects the ship, false otherwise
        /// </returns>
        /// <exception cref="ArgumentNullException">ship</exception>
        public bool CheckForCollision(GameObject ship)
        {
            if (ship == null)
            {
                throw new ArgumentNullException(nameof(ship));
            }

            var bulletBoundary = new Rect(X, Y, Width, Height);
            var shipBoundary = new Rect(ship.X, ship.Y, ship.Width, ship.Height);

            var intersect = bulletBoundary;
            intersect.Intersect(shipBoundary);

            if (intersect.IsEmpty)
            {
                return false;
            }

            return true;
        }

        public void TargetShip(GameObject targetShip, GameObject originShip)
        {
            double targetX = targetShip.X + (targetShip.Width / 2);
            double targetY = targetShip.Y;

            double originY = originShip.Y + originShip.Height + this.Height;
            double originX = originShip.X + (originShip.Width / 2) + (this.Width / 2);

            Point targetPoint = new Point(targetX, targetY);
            Point originPoint = new Point(originX, originY);

            if (originPoint.X >= targetShip.X && originPoint.X <= (targetShip.X + targetShip.Width))
            {
                directionToShoot = TargetedShipDirection.down;
            }
            else if (originPoint.X < targetShip.X)
            {
                directionToShoot = TargetedShipDirection.right;
            }
            else
            {
                directionToShoot = TargetedShipDirection.left;
            }
        }

        public void MoveTargeted()
        {
            this.MoveDown();
            if (this.directionToShoot == TargetedShipDirection.left)
            {
                MoveLeft();
            } else if (this.directionToShoot == TargetedShipDirection.right)
            {
                this.MoveRight();
            }

        }

        #endregion
    }
}