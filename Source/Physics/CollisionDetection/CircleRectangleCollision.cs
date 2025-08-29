using Physics.Math;

namespace Physics.CollisionDetection
{
    internal static class CircleRectangleCollision
    {
        internal static CollisionInfo[] GetCollisionPoints(RigidCircle circle, RigidRectangle rectangle)
        {
            // Find the closest point on the rectangle to the circle center
            Vec2D closestPoint = FindClosestPointOnRectangle(circle.Center, rectangle);
            
            // Calculate distance from circle center to closest point
            Vec2D centerToClosest = closestPoint - circle.Center;
            float distance = centerToClosest.Length();
            
            // Check if collision occurs
            if (distance >= circle.Radius)
                return new CollisionInfo[0]; // No collision
            
            // Calculate collision normal (from rectangle surface toward circle center)
            Vec2D normal = distance > 0.001f ? centerToClosest.Normalize() : new Vec2D(0, 1);
            
            // Calculate penetration depth
            float depth = circle.Radius - distance;
            
            // Contact point should be on the rectangle surface (closest point)
            Vec2D contactPoint = closestPoint;
            
            return new CollisionInfo[] 
            { 
                new CollisionInfo(contactPoint, normal, depth, circle, rectangle) 
            };
        }
        
        private static Vec2D FindClosestPointOnRectangle(Vec2D point, RigidRectangle rectangle)
        {
            // Transform point to rectangle's local space
            Vec2D localPoint = point - rectangle.Center;
            
            // Rotate point to align with rectangle's local axes
            float cosAngle = (float)System.Math.Cos(-rectangle.Angle);
            float sinAngle = (float)System.Math.Sin(-rectangle.Angle);
            
            Vec2D rotatedPoint = new Vec2D(
                localPoint.X * cosAngle - localPoint.Y * sinAngle,
                localPoint.X * sinAngle + localPoint.Y * cosAngle
            );
            
            // Clamp to rectangle bounds in local space
            float halfWidth = rectangle.Size.X * 0.5f;
            float halfHeight = rectangle.Size.Y * 0.5f;
            
            Vec2D clampedPoint = new Vec2D(
                System.Math.Max(-halfWidth, System.Math.Min(halfWidth, rotatedPoint.X)),
                System.Math.Max(-halfHeight, System.Math.Min(halfHeight, rotatedPoint.Y))
            );
            
            // Rotate back to world space
            Vec2D worldPoint = new Vec2D(
                clampedPoint.X * cosAngle + clampedPoint.Y * sinAngle,
                -clampedPoint.X * sinAngle + clampedPoint.Y * cosAngle
            );
            
            // Translate back to world position
            return worldPoint + rectangle.Center;
        }
    }
}