using Physics.Math;

namespace Physics.CollisionDetection
{
    internal static class CircleCircleCollision
    {
        internal static CollisionInfo[] GetCollisionPoints(RigidCircle circle1, RigidCircle circle2)
        {
            // Calculate distance between centers
            Vec2D centerToCenter = circle2.Center - circle1.Center;
            float distance = centerToCenter.Length();
            
            // Check if collision occurs
            float radiusSum = circle1.Radius + circle2.Radius;
            if (distance >= radiusSum)
                return new CollisionInfo[0]; // No collision
            
            // Calculate collision normal (from circle1 center to circle2 center)
            Vec2D normal = distance > 0.001f ? centerToCenter.Normalize() : new Vec2D(1, 0);
            
            // Calculate penetration depth
            float depth = radiusSum - distance;
            
            // Contact point is on the line between centers, on circle1's surface
            Vec2D contactPoint = circle1.Center + normal * circle1.Radius;
            
            return new CollisionInfo[] 
            { 
                new CollisionInfo(contactPoint, normal, depth, circle1, circle2) 
            };
        }
    }
}