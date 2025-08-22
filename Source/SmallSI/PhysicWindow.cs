using Graphic;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Physics;
using Physics.Math;
using System.Drawing;

namespace SmallSI
{
    internal class PhysicWindow : GameWindow2D
    {
        private PhysicScene physicScene = new PhysicScene();
        private int currentScene = 1; // 1 = rectangles, 2 = falling circle

        public PhysicWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
           : base(gameWindowSettings, nativeWindowSettings)
        {
            LoadScene(currentScene);
        }

        private void LoadScene(int sceneNumber)
        {
            physicScene = new PhysicScene();
            
            if (sceneNumber == 1)
            {
                LoadRectangleScene();
            }
            else if (sceneNumber == 2)
            {
                LoadCircleScene();
            }
        }

        private void LoadRectangleScene()
        {
            // Original rectangle demo scene
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

        private void LoadCircleScene()
        {
            // New circle demo scene - simple setup with falling circle
            // Create boundaries (walls and floor)
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(450f, 540f), new Vec2D(900f, 20f), 0f, float.MaxValue, 0.8f, 0.1f)); // Bottom wall
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(10f, 275f), new Vec2D(20f, 550f), 0f, float.MaxValue, 0.8f, 0.1f)); // Left wall
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(890f, 275f), new Vec2D(20f, 550f), 0f, float.MaxValue, 0.8f, 0.1f)); // Right wall
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(450f, 10f), new Vec2D(900f, 20f), 0f, float.MaxValue, 0.8f, 0.1f)); // Top wall

            // Add a falling circle from the top
            physicScene.Bodies.Add(new RigidCircle(new Vec2D(450f, 50f), 25f, 0.001f, 0.7f, 0.1f));
            
            // Add some static rectangles as obstacles
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(300f, 400f), new Vec2D(100f, 20f), 0.3f, float.MaxValue, 0.5f, 0.1f));
            physicScene.Bodies.Add(new RigidRectangle(new Vec2D(600f, 300f), new Vec2D(100f, 20f), -0.3f, float.MaxValue, 0.5f, 0.1f));
        }

        private void Reset()
        {
            LoadScene(currentScene);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Reset();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            
            if (e.Key == Keys.D1)
            {
                currentScene = 1;
                LoadScene(currentScene);
            }
            else if (e.Key == Keys.D2)
            {
                currentScene = 2;
                LoadScene(currentScene);
            }
            else
            {
                LoadScene(currentScene); // Reset current scene
            }
        }

        //This function is called in a timer from the OpenTK-GameWindow
        protected override void Draw(IDrawingContext context)
        {
            //Move boxes
            this.physicScene.TimeStep(0.005f);

            //Draw bodies
            context.ClearScreen(Color.AliceBlue);
            
            foreach (var body in physicScene.Bodies)
            {
                if (body is RigidRectangle rectangle)
                {
                    context.DrawRotatedRectangle(rectangle.Center.ToGrx(), rectangle.Size.X, rectangle.Size.Y, -rectangle.Angle, Color.FromArgb(230, 230, 0));
                    for (int i = 0; i < 4; i++)
                    {
                        context.DrawLine(rectangle.Vertex[i].ToGrx(), rectangle.Vertex[(i + 1) % 4].ToGrx(), 2, Color.Black);
                    }
                }
                else if (body is RigidCircle circle)
                {
                    context.DrawCircle(circle.Center.ToGrx(), circle.Radius, Color.FromArgb(0, 150, 230));
                }
            }

            context.SwapBuffer();
        }
    }
}
