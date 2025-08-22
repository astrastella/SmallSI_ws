using Physics.Math;

namespace Physics
{
    public interface IRigidBody
    {
        Vec2D Center { get; }
        Vec2D Velocity { get; set; }
        float AngularVelocity { get; set; }
        float InverseMass { get; }
        float InverseInertia { get; }
        float Restituion { get; set; }
        float Friction { get; set; }
        float Radius { get; } // For bounding circle test
        
        void MoveCenter(Vec2D v);
        void Rotate(float angle);
    }
}
