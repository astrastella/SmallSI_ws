namespace Physics.CollisionDetection
{
    internal static class CollisionHelper
    {
        public static CollisionInfo[] GetAllCollisions(List<IRigidBody> bodies)
        {
            List<CollisionInfo> collisions = new List<CollisionInfo>();

            for (int i = 0; i < bodies.Count; i++)
                for (int j = i + 1; j < bodies.Count; j++)
                {
                    var b1 = bodies[i];
                    var b2 = bodies[j];
                    if (b1.InverseMass == 0 && b2.InverseMass == 0) continue; //No collisionpoint between non moveable bodies

                    if (BoundingCircleCollides(b1, b2))   //Broudphase-Test
                    {
                        var contacts = GetCollisionPoints(b1, b2); //Nearphase-Test
                        if (contacts.Any())
                            collisions.AddRange(contacts);
                    }
                }

            return collisions.ToArray();
        }

        private static CollisionInfo[] GetCollisionPoints(IRigidBody body1, IRigidBody body2)
        {
            // Handle different collision type combinations
            if (body1 is RigidRectangle rect1 && body2 is RigidRectangle rect2)
            {
                return RectangleRectangleCollision.GetCollisionPoints(rect1, rect2);
            }
            else if (body1 is RigidCircle circle1 && body2 is RigidCircle circle2)
            {
                return CircleCircleCollision.GetCollisionPoints(circle1, circle2);
            }
            else if (body1 is RigidRectangle rect && body2 is RigidCircle circle)
            {
                return CircleRectangleCollision.GetCollisionPoints(circle, rect);
            }
            else if (body1 is RigidCircle circ && body2 is RigidRectangle rectangle)
            {
                return CircleRectangleCollision.GetCollisionPoints(circ, rectangle);
            }
            
            return new CollisionInfo[0]; // No collision handling for unknown types
        }

        internal static bool BoundingCircleCollides(IRigidBody c1, IRigidBody c2)
        {
            float d = (c1.Center - c2.Center).Length();
            return d < (c1.Radius + c2.Radius);
        }
    }
}
