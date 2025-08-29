# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

SmallSI is a 2D physics engine demonstrating sequential impulse-based collision resolution. It's inspired by Box2D-Lite but designed for educational clarity. The engine simulates rigid body dynamics with realistic collision detection and response using only rectangles.

## Architecture

The solution consists of three main projects:

### Core Projects
- **SmallSI** (main executable) - Entry point and window management
- **Physics** - Core physics simulation engine with collision detection/resolution  
- **Graphic** - OpenTK-based 2D rendering system with OpenGL shaders

### Key Components

**Physics Engine** (`Source/Physics/`):
- `PhysicScene.cs` - Main simulation loop implementing sequential impulse method
- **IMPORTANT**: Currently only supports `List<RigidRectangle>` - no mixed body types
- `IRigidBody.cs` - Interface for rigid body objects
- `RigidRectangle.cs` - Rectangle rigid body implementation 
- `CollisionDetection/` - Rectangle-to-rectangle collision detection only
- `CollisionResolution/` - Constraint-based impulse resolution system
- `Math/` - Vector and matrix mathematics utilities

**Rendering System** (`Source/Graphic/`):
- `GameWindow2D.cs` - OpenTK window management and rendering loop
- `Drawer2D.cs` - 2D drawing abstraction layer
- `SolidQuadDrawer.cs` - Shader-based quad rendering
- `Shaders/` - GLSL vertex and fragment shaders

**Window Management** (`Source/SmallSI/`):
- `PhysicWindow.cs` - Main game window with simple reset functionality
- Currently loads a single predefined rectangle scene
- Mouse click or any key press resets the scene

## Build Commands

```bash
# Build the solution
dotnet build Source/SmallSI.sln

# Run the physics demo
dotnet run --project Source/SmallSI/SmallSI.csproj

# Build individual projects
dotnet build Source/Physics/Physics.csproj
dotnet build Source/Graphic/Graphic.csproj
```

## Current Limitations

- **Rectangle-only**: PhysicScene.Bodies is typed as `List<RigidRectangle>` 
- **No circle support**: Even though RigidCircle exists, it's not integrated
- **Single scene**: No scene switching - only one hardcoded rectangle demo
- **Simple interaction**: Only supports scene reset via mouse/keyboard

## Development Notes

### Physics Implementation
- Uses sequential impulse method for constraint solving
- Implements position correction to prevent penetration
- Supports friction and restitution coefficients
- Collision detection via separating axis theorem (SAT) for rectangles only

### Key Physics Classes
- `NormalConstraint` - Prevents penetration between bodies
- `FrictionConstraint` - Simulates surface friction
- `CollisionInfo` - Contains collision point data and normals
- `Settings` - Configurable physics parameters (gravity, iterations, etc.)

### Adding New Features
When extending the codebase:
1. To add circles: Change `PhysicScene.Bodies` from `List<RigidRectangle>` to `List<IRigidBody>`
2. Update CollisionHelper to handle mixed body types
3. Update rendering code to handle different body types
4. Consider scene management system for multiple demos

### Dependencies
- .NET 8.0
- OpenTK 4.8.2 (OpenGL wrapper for graphics)

### Physics Timestep Sequence
1. Collision detection (rectangle-rectangle only)
2. Constraint creation (normal + friction)
3. Gravity application
4. Sequential impulse resolution
5. Position integration

For detailed physics theory, see the comprehensive README.md which explains the mathematical derivations behind the sequential impulse method.