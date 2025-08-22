using Graphic;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Physics;
using Physics.CollisionResolution;
using Physics.Math;
using System.Drawing;

namespace SmallSI
{
    public enum DemoScene
    {
        OriginalDemo,
        BallDrop,
        EmptyScene
    }

    internal class PhysicWindow : GameWindow2D
    {
        private PhysicScene physicScene = new PhysicScene();
        private DemoScene currentScene = DemoScene.OriginalDemo;

        public PhysicWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
           : base(gameWindowSettings, nativeWindowSettings)
        {
            Reset();
        }

        private void Reset()
        {
            physicScene = new PhysicScene();
            
            switch (currentScene)
            {
                case DemoScene.OriginalDemo:
                    SetupOriginalDemo();
                    Title = "2D Rigidbody Physics - Original Demo (Press 1-3 to switch scenes, R to reset)";
                    break;
                case DemoScene.BallDrop:
                    SetupBallDrop();
                    Title = "2D Rigidbody Physics - Ball Drop (Press 1-3 to switch scenes, R to reset)";
                    break;
                case DemoScene.EmptyScene:
                    SetupEmptyScene();
                    Title = "2D Rigidbody Physics - Empty Scene (Press 1-3 to switch scenes, R to reset)";
                    break;
            }
        }

        private void SetupOriginalDemo()
        {
            // Reset to default settings for original demo
            physicScene.Settings.IterationCount = 10;
            physicScene.Settings.AllowedPenetration = 1.0f;
            
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(244.5f, 126f), new Vec2D(449.54117f, 14.5f), 0.33195063f, float.MaxValue, 0.5f, 0.003f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(381.5f, 313f), new Vec2D(422.22897f, 14.538884f), 2.8601727f, float.MaxValue, 0.5f, 0.003f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(13f, 289.5f), new Vec2D(16.5f, 535f), 0f, float.MaxValue, 0.5f, 0.03f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(458.5f, 550f), new Vec2D(875.5f, 13.5f), 0f, float.MaxValue, 0.5f, 0.1f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(884.5f, 276f), new Vec2D(23.5f, 535.5f), 0f, float.MaxValue, 0.5f, 0.03f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(82f, 40.5f), new Vec2D(37f, 38.5f), 0.32114068f, 0.0001f, 0.5f, 0.003f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(574.5f, 135f), new Vec2D(16.5f, 224.5f), 0f, float.MaxValue, 0.5f, 0.03f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(706f, 508.5f), new Vec2D(68.5f, 69f), 0f, 0.0001f, 0.5f, 0.03f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(723f, 439f), new Vec2D(68.5f, 69f), 0f, 0.0001f, 0.5f, 0.03f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(693.5f, 370f), new Vec2D(68.5f, 69f), 0f, 0.0001f, 0.5f, 0.03f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(719.5f, 301.5f), new Vec2D(68.5f, 69f), 0f, 0.0001f, 0.5f, 0.03f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(698.5f, 232f), new Vec2D(68.5f, 69f), 0f, 0.0001f, 0.5f, 0.03f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(441f, 268f), new Vec2D(37f, 38.5f), -0.30717787f, 0.0001f, 0.5f, 0.003f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(217f, 87.5f), new Vec2D(37f, 38.5f), 0.32114068f, 0.0001f, 0.5f, 0.003f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(307.5f, 306.5f), new Vec2D(37f, 38.5f), 1.2705995f, 0.0001f, 0.5f, 0.003f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(332f, 127.5f), new Vec2D(37f, 38.5f), 0.32114068f, 0.0001f, 0.5f, 0.003f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(443.5f, 166.5f), new Vec2D(37f, 38.5f), 0.32114068f, 0.0001f, 0.5f, 0.003f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(458.5f, 3f), new Vec2D(875.5f, 13.5f), 0f, float.MaxValue, 0.5f, 0.1f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(80f, 454f), new Vec2D(148.73466f, 14.5f), 0.7283174f, float.MaxValue, 0.5f, 0.003f));
        }

        private void SetupBallDrop()
        {
            // Reset to default settings - same as original demo
            physicScene.Settings.IterationCount = 10;
            physicScene.Settings.AllowedPenetration = 1.0f;
            // Use default gravity for realism
            physicScene.Settings.Gravity = 9.81f;
            
            // Only create bottom boundary like in original demo
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(458.5f, 550f), new Vec2D(875.5f, 13.5f), 0f, float.MaxValue, 0.5f, 0.1f));
            
            // Create a spherical ball using RigidCircle
            var ball = new RigidCircle(
                new Vec2D(450f, 100f), // Start near the top center
                15f, // Radius of 15 pixels
                0.001f, // Keep default low density
                0.8f,   // Default restitution
                0.1f    // Low friction
            );
            physicScene.Circles.Add(ball);
        }

        private void SetupEmptyScene()
        {
            // Reset to default settings
            physicScene.Settings.IterationCount = 10;
            physicScene.Settings.AllowedPenetration = 1.0f;
            
            // Only create bottom boundary like in ball drop scene
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(450f, 530f), new Vec2D(900f, 20f), 0f, float.MaxValue, 0.5f, 0.1f)); // Bottom

            // Empty scene - just bottom boundary, no objects
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Reset();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            switch (e.Key)
            {
                case Keys.D1:
                    currentScene = DemoScene.OriginalDemo;
                    Reset();
                    break;
                case Keys.D2:
                    currentScene = DemoScene.BallDrop;
                    Reset();
                    break;
                case Keys.D3:
                    currentScene = DemoScene.EmptyScene;
                    Reset();
                    break;
                case Keys.R:
                    Reset(); // Reset current scene
                    break;
            }
        }

        //This function is called in a timer from the OpenTK-GameWindow
        protected override void Draw(IDrawingContext context)
        {
            //Move boxes
            this.physicScene.TimeStep(0.005f);

            //Draw boxes
            context.ClearScreen(Color.AliceBlue);
            
            // Draw rectangles
            foreach (var body in physicScene.Bodies)
            {
                Color bodyColor = Color.FromArgb(230, 230, 0); // Default yellow for boundaries
                
                context.DrawRotatedRectangle(body.Center.ToGrx(), body.Size.X, body.Size.Y, -body.Angle, bodyColor);
                for (int i=0;i<4;i++)
                {
                    context.DrawLine(body.Vertex[i].ToGrx(), body.Vertex[(i + 1) % 4].ToGrx(), 2, Color.Black);
                }                
            }
            
            // Draw circles
            foreach (var circle in physicScene.Circles)
            {
                Color circleColor = Color.FromArgb(255, 100, 100); // Red for the ball
                
                // Draw circle as a filled square with diameter = 2 * radius
                float diameter = circle.Radius * 2;
                context.DrawRotatedRectangle(circle.Center.ToGrx(), diameter, diameter, -circle.Angle, circleColor);
                
                // Draw circle outline using multiple line segments to approximate a circle
                int segments = 16;
                for (int i = 0; i < segments; i++)
                {
                    float angle1 = (float)(2 * System.Math.PI * i / segments);
                    float angle2 = (float)(2 * System.Math.PI * (i + 1) / segments);
                    
                    Vec2D p1 = circle.Center + new Vec2D(
                        circle.Radius * (float)System.Math.Cos(angle1),
                        circle.Radius * (float)System.Math.Sin(angle1)
                    );
                    Vec2D p2 = circle.Center + new Vec2D(
                        circle.Radius * (float)System.Math.Cos(angle2),
                        circle.Radius * (float)System.Math.Sin(angle2)
                    );
                    
                    context.DrawLine(p1.ToGrx(), p2.ToGrx(), 2, Color.Black);
                }
            }

            // Draw scene information
            string sceneText = currentScene switch
            {
                DemoScene.OriginalDemo => "Scene 1: Original Demo (Press 2 for Ball Drop, 3 for Empty Scene)",
                DemoScene.BallDrop => "Scene 2: Ball Drop (Press 1 for Original Demo, 3 for Empty Scene)",
                DemoScene.EmptyScene => "Scene 3: Empty Scene (Press 1 for Original Demo, 2 for Ball Drop)",
                _ => "Unknown Scene"
            };
            
            // Note: Drawing text might not be available in this graphics system, 
            // but we'll leave this for potential future implementation
            // context.DrawText(sceneText, new Vector2(10, 10), Color.Black);

            context.SwapBuffer();
        }


    }
}
