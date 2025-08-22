using Physics.Math;

namespace Physics.CollisionDetection
{
    internal static class CircleRectangleCollision
    {
        /// <summary>
        /// Simple collision detection between a circle and rectangle boundaries
        /// This creates basic collision response by adjusting circle position and velocity
        /// </summary>
        public static void HandleCircleRectangleCollisions(RigidCircle circle, List<RigidRectangle> rectangles)
        {
            foreach (var rect in rectangles)
            {
                // Only check collision with static boundaries (infinite mass)
                if (rect.InverseMass > 0) continue;

                // Get rectangle bounds
                Vec2D rectMin = new Vec2D(rect.Center.X - rect.Size.X / 2, rect.Center.Y - rect.Size.Y / 2);
                Vec2D rectMax = new Vec2D(rect.Center.X + rect.Size.X / 2, rect.Center.Y + rect.Size.Y / 2);

                // Debug output - only print when ball is near the ground
                if (circle.Center.Y > 400)
                {
                    System.Console.WriteLine($"Circle at ({circle.Center.X:F1}, {circle.Center.Y:F1}), velocity=({circle.Velocity.X:F2}, {circle.Velocity.Y:F2})");
                    System.Console.WriteLine($"Rect bounds: ({rectMin.X:F1}, {rectMin.Y:F1}) to ({rectMax.X:F1}, {rectMax.Y:F1})");
                    System.Console.WriteLine($"Bottom of circle: {circle.Center.Y + circle.Radius:F1}, Top of rect: {rectMin.Y:F1}");
                    System.Console.WriteLine($"Collision check: bottom({circle.Center.Y + circle.Radius:F1}) >= top({rectMin.Y:F1}) = {circle.Center.Y + circle.Radius >= rectMin.Y}");
                    System.Console.WriteLine($"X in bounds: {circle.Center.X >= rectMin.X && circle.Center.X <= rectMax.X}");
                    System.Console.WriteLine($"Moving down: {circle.Velocity.Y > 0}");
                }
                
                // Handle only ONE face per rectangle per frame (priority: top, bottom, left, right)
                const float epsilon = 0.5f; // prevent micro jitter at contact
                if (circle.Center.Y + circle.Radius >= rectMin.Y - epsilon &&
                    circle.Center.X >= rectMin.X && circle.Center.X <= rectMax.X &&
                    circle.Velocity.Y > 0)
                {
                    System.Console.WriteLine("COLLISION DETECTED! Ball hit top surface of ground");
                    System.Console.WriteLine($"Before correction: Circle Y={circle.Center.Y:F1}, Velocity Y={circle.Velocity.Y:F2}");
                    float penetration = (circle.Center.Y + circle.Radius) - rectMin.Y;
                    System.Console.WriteLine($"Penetration: {penetration:F1}, moving ball up by this amount");
                    // Snap exactly onto the surface to avoid cumulative drift
                    float desiredY = rectMin.Y - circle.Radius;
                    circle.MoveCenter(new Vec2D(0, desiredY - circle.Center.Y));
                    System.Console.WriteLine($"After position correction: Circle Y={circle.Center.Y:F1}");
                    float oldVelocityY = circle.Velocity.Y; // downward (+)
                    circle.Velocity = new Vec2D(circle.Velocity.X, -oldVelocityY * circle.Restitution);
                    System.Console.WriteLine($"Velocity changed: {oldVelocityY:F2} -> {circle.Velocity.Y:F2}");
                    System.Console.WriteLine("---");
                    continue;
                }
                else if (circle.Center.Y - circle.Radius <= rectMax.Y + epsilon &&
                         circle.Center.X >= rectMin.X && circle.Center.X <= rectMax.X &&
                         circle.Velocity.Y < 0)
                {
                    float penetration = rectMax.Y - (circle.Center.Y - circle.Radius);
                    circle.MoveCenter(new Vec2D(0, penetration));
                    circle.Velocity = new Vec2D(circle.Velocity.X, -circle.Velocity.Y * circle.Restitution);
                    continue;
                }
                else if (circle.Center.X - circle.Radius < rectMin.X &&
                         circle.Center.Y >= rectMin.Y && circle.Center.Y <= rectMax.Y &&
                         circle.Velocity.X < 0)
                {
                    circle.MoveCenter(new Vec2D(rectMin.X - (circle.Center.X - circle.Radius), 0));
                    circle.Velocity = new Vec2D(-circle.Velocity.X * circle.Restitution, circle.Velocity.Y);
                    continue;
                }
                else if (circle.Center.X + circle.Radius > rectMax.X &&
                         circle.Center.Y >= rectMin.Y && circle.Center.Y <= rectMax.Y &&
                         circle.Velocity.X > 0)
                {
                    circle.MoveCenter(new Vec2D(rectMax.X - (circle.Center.X + circle.Radius), 0));
                    circle.Velocity = new Vec2D(-circle.Velocity.X * circle.Restitution, circle.Velocity.Y);
                    continue;
                }
            }
        }

        private static void CheckTopSurface(RigidCircle circle, Vec2D rectMin, Vec2D rectMax)
        {
            // Check if circle hits TOP SURFACE of rectangle (ball falling down onto ground)
            if (circle.Center.Y + circle.Radius >= rectMin.Y &&
                circle.Center.X >= rectMin.X && circle.Center.X <= rectMax.X &&
                circle.Velocity.Y > 0)  // Moving downward
            {
                System.Console.WriteLine("COLLISION DETECTED! Ball hit top surface of ground");
                System.Console.WriteLine($"Before correction: Circle Y={circle.Center.Y:F1}, Velocity Y={circle.Velocity.Y:F2}");
                
                // Position correction - place ball just above the top surface
                float penetration = (circle.Center.Y + circle.Radius) - rectMin.Y;
                System.Console.WriteLine($"Penetration: {penetration:F1}, moving ball up by this amount");
                circle.MoveCenter(new Vec2D(0, -penetration));
                
                System.Console.WriteLine($"After position correction: Circle Y={circle.Center.Y:F1}");
                
                // Velocity response with restitution
                float oldVelocityY = circle.Velocity.Y;
                circle.Velocity = new Vec2D(circle.Velocity.X, -circle.Velocity.Y * circle.Restitution);
                System.Console.WriteLine($"Velocity changed: {oldVelocityY:F2} -> {circle.Velocity.Y:F2}");
                System.Console.WriteLine("---");
            }
        }

        private static void CheckBottomSurface(RigidCircle circle, Vec2D rectMin, Vec2D rectMax)
        {
            // Check if circle hits BOTTOM SURFACE of rectangle (ball bouncing up into ceiling)
            if (circle.Center.Y - circle.Radius <= rectMax.Y &&
                circle.Center.X >= rectMin.X && circle.Center.X <= rectMax.X &&
                circle.Velocity.Y < 0)  // Moving upward
            {
                // Position correction - place ball just below the bottom surface
                float penetration = rectMax.Y - (circle.Center.Y - circle.Radius);
                circle.MoveCenter(new Vec2D(0, penetration));
                
                // Velocity response with restitution
                circle.Velocity = new Vec2D(circle.Velocity.X, -circle.Velocity.Y * circle.Restitution);
            }
        }

        private static void CheckLeftEdge(RigidCircle circle, Vec2D rectMin, Vec2D rectMax)
        {
            // Check if circle hits left edge
            if (circle.Center.X - circle.Radius < rectMin.X &&
                circle.Center.Y >= rectMin.Y && circle.Center.Y <= rectMax.Y &&
                circle.Velocity.X < 0)
            {
                // Position correction
                circle.MoveCenter(new Vec2D(rectMin.X - (circle.Center.X - circle.Radius), 0));
                
                // Velocity response with restitution
                circle.Velocity = new Vec2D(-circle.Velocity.X * circle.Restitution, circle.Velocity.Y);
            }
        }

        private static void CheckRightEdge(RigidCircle circle, Vec2D rectMin, Vec2D rectMax)
        {
            // Check if circle hits right edge
            if (circle.Center.X + circle.Radius > rectMax.X &&
                circle.Center.Y >= rectMin.Y && circle.Center.Y <= rectMax.Y &&
                circle.Velocity.X > 0)
            {
                // Position correction
                circle.MoveCenter(new Vec2D(rectMax.X - (circle.Center.X + circle.Radius), 0));
                
                // Velocity response with restitution
                circle.Velocity = new Vec2D(-circle.Velocity.X * circle.Restitution, circle.Velocity.Y);
            }
        }
    }
}
