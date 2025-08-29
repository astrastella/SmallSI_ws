# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

SmallSI is a 2D physics engine demonstrating sequential impulse-based collision resolution. It's inspired by Box2D-Lite but designed for educational clarity. The engine simulates rigid body dynamics with realistic collision detection and response for both rectangles and circles.

## Architecture

The solution consists of three main projects:

### Core Projects
- **SmallSI** (main executable) - Entry point and window management
- **Physics** - Core physics simulation engine with collision detection/resolution  
- **Graphic** - OpenTK-based 2D rendering system with OpenGL shaders

### Key Components

**Physics Engine** (`Source/Physics/`):
- `PhysicScene.cs` - Main simulation loop implementing sequential impulse method
- `IRigidBody.cs` - Interface for rigid body objects enabling mixed body types
- `RigidRectangle.cs` - Rectangle rigid body implementation 
- `RigidCircle.cs` - Circle rigid body implementation
- `CollisionDetection/` - Full collision detection for all body type combinations
  - `CircleCircleCollision.cs` - Circle-to-circle collision detection
  - `CircleRectangleCollision.cs` - Circle-to-rectangle collision detection
  - `RectangleRectangleCollision.cs` - Rectangle-to-rectangle collision detection
- `CollisionResolution/` - Constraint-based impulse resolution system
- `Math/` - Vector and matrix mathematics utilities

**Rendering System** (`Source/Graphic/`):
- `GameWindow2D.cs` - OpenTK window management and rendering loop
- `Drawer2D.cs` - 2D drawing abstraction layer with circle support
- `SolidQuadDrawer.cs` - Shader-based quad rendering with multi-ring circle rendering
- `IDrawingContext.cs` - Drawing interface supporting rectangles, lines, and circles
- `Shaders/` - GLSL vertex and fragment shaders

**Window Management** (`Source/SmallSI/`):
- `PhysicWindow.cs` - Main game window with scene switching and reset functionality
- Scene 1: Complex rectangle physics demonstration
- Scene 2: Bouncy circle physics with obstacles
- Controls: Press "1"/"2" to switch scenes, mouse click or other keys to reset

## Build Commands

```bash
# Build the solution
dotnet build Source/SmallSI.sln

# Run the physics demo
dotnet run --project Source/SmallSI/SmallSI.csproj
# Or run from output directory:
cd Source/SmallSI/bin/Debug/net8.0 && dotnet SmallSI.dll

# Build individual projects
dotnet build Source/Physics/Physics.csproj
dotnet build Source/Graphic/Graphic.csproj
```

## Current Features

- **Mixed body types**: PhysicScene.Bodies supports `List<IRigidBody>` with circles and rectangles
- **Full circle support**: Complete circle physics with collision detection and rendering
- **Dual scene system**: Scene switching between rectangle demo and circle demo
- **Interactive controls**: Scene switching (keys 1/2) and reset functionality

## Development Notes

### Physics Implementation
- Uses sequential impulse method for constraint solving
- Implements position correction to prevent penetration
- Supports friction and restitution coefficients
- Collision detection for all body type combinations:
  - Rectangle-Rectangle: Separating Axis Theorem (SAT)
  - Circle-Circle: Distance-based collision detection
  - Circle-Rectangle: Closest point algorithm with proper normal calculation

### Key Physics Classes
- `NormalConstraint` - Prevents penetration between bodies
- `FrictionConstraint` - Simulates surface friction
- `CollisionInfo` - Contains collision point data and normals
- `Settings` - Configurable physics parameters (gravity, iterations, etc.)

### Circle Rendering Implementation
- Uses concentric rings approach for smooth appearance
- 8 rings with increasing segment counts (32-88 segments)
- Each ring uses small overlapping rectangles positioned in circles
- Hollow center design (no center fill rectangle)
- Total segments: ~400+ rectangles per circle for smooth appearance

### Dependencies
- .NET 8.0
- OpenTK 4.8.2 (OpenGL wrapper for graphics)

### Physics Timestep Sequence
1. Collision detection (all body type combinations)
2. Constraint creation (normal + friction)
3. Gravity application
4. Sequential impulse resolution (15 iterations for stability)
5. Position integration

### Scene Descriptions
**Scene 1 (Rectangle Demo)**: Complex multi-body rectangle physics with various static and dynamic obstacles, showcasing collision resolution between many rectangular bodies.

**Scene 2 (Circle Demo)**: High-bounce circle (0.95 restitution) dropping from top with angled obstacle platforms. Features smooth circle rendering and energetic bouncing physics.

For detailed physics theory, see the comprehensive README.md which explains the mathematical derivations behind the sequential impulse method.