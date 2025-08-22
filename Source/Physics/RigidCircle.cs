using Physics.Math;

namespace Physics
{
    public class RigidCircle
    {
        public float Radius { get; private set; }

        public Vec2D Center { get; set; } //Position of the Center of gravity
        public float Angle { get; private set; } //Oriantation around the Z-Axis [0..2PI] (for drawing purposes)
        public Vec2D Velocity { get; set; } //Velocity from the Center-Point
        public float AngularVelocity { get; set; }

        public float InverseMass { get; private set; } //1 / Mass
        public float InverseInertia { get; private set; }

        public float Restitution { get; set; } = 0.2f;
        public float Friction { get; set; } = 0.1f;

        public RigidCircle(Vec2D center, float radius, float density, float restitution, float friction)
        {
            this.Center = center;
            this.Radius = radius;
            this.Angle = 0;
            this.Velocity = new Vec2D(0, 0);
            this.AngularVelocity = 0;

            // Calculate mass from area and density: mass = π * r² * density
            float area = (float)(System.Math.PI * radius * radius);
            float mass = area * density;
            
            this.InverseMass = density == float.MaxValue ? 0 : 1 / mass;
            
            // Moment of inertia for a solid circle: I = (1/2) * m * r²
            float inertia = 0.5f * mass * radius * radius;
            this.InverseInertia = InverseMass == 0 ? 0 : 1.0f / inertia;

            this.Restitution = restitution;
            this.Friction = friction;
        }

        public void MoveCenter(Vec2D v)
        {
            Center += v;
        }

        public void Rotate(float angle)
        {
            Angle += angle;
            // Keep angle in [0, 2π] range
            while (Angle > 2 * System.Math.PI) Angle -= 2 * (float)System.Math.PI;
            while (Angle < 0) Angle += 2 * (float)System.Math.PI;
        }

        /// <summary>
        /// Get the bounding box of the circle for broad-phase collision detection
        /// </summary>
        public (Vec2D min, Vec2D max) GetBoundingBox()
        {
            return (
                new Vec2D(Center.X - Radius, Center.Y - Radius),
                new Vec2D(Center.X + Radius, Center.Y + Radius)
            );
        }

        /// <summary>
        /// Check if a point is inside the circle
        /// </summary>
        public bool ContainsPoint(Vec2D point)
        {
            Vec2D diff = point - Center;
            return diff.Length() <= Radius;
        }
    }
}
