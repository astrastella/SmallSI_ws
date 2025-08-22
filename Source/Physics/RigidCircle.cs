using Physics.Math;

namespace Physics
{
    public class RigidCircle : IRigidBody
    {
        public float Radius { get; private set; }

        public Vec2D Center { get; private set; } //Position of the Center of gravity
        public Vec2D Velocity { get; set; } //Velocity from the Center-Point
        public float AngularVelocity { get; set; }

        public float InverseMass { get; private set; } //1 / Mass
        public float InverseInertia { get; private set; }

        public float Restituion { get; set; } = 0.2f;
        public float Friction { get; set; } = 0.1f;

        public RigidCircle(Vec2D center, float radius, float density, float restituion, float friction)
        {
            this.Center = center;
            this.Radius = radius;
            this.Velocity = new Vec2D(0, 0);
            this.AngularVelocity = 0;

            // For a circle: mass = π * r² * density
            float area = (float)(System.Math.PI * radius * radius);
            float mass = area * density;
            this.InverseMass = density == float.MaxValue ? 0 : 1 / mass;
            
            // For a solid circle: I = (1/2) * m * r²
            this.InverseInertia = InverseMass == 0 ? 0 : 1.0f / (0.5f * mass * radius * radius);

            this.Restituion = restituion;
            this.Friction = friction;
        }

        public void MoveCenter(Vec2D v)
        {
            Center += v;
        }

        public void Rotate(float angle)
        {
            // For a circle, rotation doesn't change its visual appearance
            // but we still track angular velocity for physics calculations
        }
    }
}
