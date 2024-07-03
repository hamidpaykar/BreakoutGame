# Breakout Game - Software Udvikling 2024

## Introduction

This project involves the development of a Breakout game using object-oriented design principles, design patterns, and good coding practices. The game is based on the classic arcade game developed by Atari Inc. in 1976.

## Game Requirements

### Task 1: Basic Mechanics
- Implement the basic game mechanics including paddle control, ball, and basic block structures.

### Task 2: Advanced Features
- Extend the game with advanced block types and power-ups.

### Task 3: State Machine
- Implement a state machine to manage different game states such as main menu, running game, paused game, and game over.

## UML Diagrams

### Class Diagram
- Illustrates the main classes involved in the game application: Game, LevelLoader, and Player.
- Highlights the relationships and interactions between these classes, providing an overview of the application structure.

### Sequence Diagram
- Shows the dynamic interactions between objects in the game design.
- Visualizes the sequence of operations and updates, offering a valuable tool for analyzing the game's behavior.

## Design

### Key Classes

#### Block Class
- Represents individual blocks in the game.
- Manages block life, points awarded for destroying it, collision detection, and destruction.

#### Player Class
- Controls paddle movement, ensuring it stays within screen boundaries.
- Handles user input and updates the paddle's position.

#### Ball Class
- Manages the ball's movement and collision detection with other game elements like the paddle and blocks.
- Updates the ball's position and direction based on interactions.

### Advanced Classes

#### PowerUpBlock Class
- Manages power-ups in the game, activating special effects when blocks are hit.
- Handles the logic for various types of power-ups.

#### HazardBlock Class
- Introduces challenging obstacles that players must navigate.
- Triggers traps or other hazards when hit, which players must avoid or overcome.

## Implementation

### Game State Management
The game uses a state machine to handle different game states:

- **MainMenu**: The starting state where players can choose to start a new game or access settings.
- **GameRunning**: The state where the game loop is executed.
- **GamePaused**: The state where the game is temporarily stopped when paused.
- **GameOver**: The state when the player loses all lives, allowing them to restart or return to the main menu.

## Design Patterns

### Singleton Pattern
- Ensures the game state is instantiated only once, providing a global method and maintaining consistent state throughout the game.

### Observer Pattern
- Facilitates communication between different game elements such as the game engine, state machine, and user interface.

## SOLID Principles

- **Single Responsibility**: Each class has a distinct responsibility (e.g., Game, StateMachine, and Score classes).
- **Open/Closed**: The Block class is extendable through subclasses.
- **Liskov Substitution**: Game state classes implement the IGameState interface.
- **Interface Segregation**: The IGameEventProcessor interface exemplifies this principle.
- **Dependency Inversion**: Game and BreakoutBus classes rely on abstractions.

## Project Showcase


### Start Screen / Level Selection
![2024-06-19 at 18 27 45](https://github.com/hamidpaykar/BreakoutGame/assets/95886258/c1809bc4-d2fb-4f42-b55d-b080f20c7ce7)

*Spillet giver en hjemme skærm der giver spillerne mulighed for at vælge mellem en række udfordrende levels.*

### Gameplay Screenshot!
![2024-06-19 at 18 28 18](https://github.com/hamidpaykar/BreakoutGame/assets/95886258/f66fca0f-5c36-487c-afaa-d0e0becc7c1d)

*Her ser vi de grundlæggende Breakout-spiloplevelse der viser paddle boldens og blokkene som spillerne skal ødelægge for at komme videre til de næste niveauer.*

### Power Ups!
![2024-06-19 at 18 35 23](https://github.com/hamidpaykar/BreakoutGame/assets/95886258/6749b8dc-88c1-4e20-8d38-964ed6bda0f2)

*Spillet inkluderer forskellige power-ups der forbedrer gameplay ved at give midlertidige fordele.*


## Conclusion

The essential components of the game have been implemented using object-oriented programming principles, ensuring a modern and engaging Breakout game experience.
