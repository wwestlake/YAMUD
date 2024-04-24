![alt text](https://github.com/wwestlake/YAMUD/blob/main/images/YAMUD_MainScreen.GIF)

# Yet Another Multi-User Dungeon (YAMUD)

YAMUD is a text-based multiplayer role-playing game inspired by classic MUDs (Multi-User Dungeons). It provides a virtual world where players can explore, interact with objects and NPCs (Non-Player Characters), engage in combat, and embark on quests together.

## Features

- **Text-Based Gameplay:** Enjoy immersive gameplay through text-based interactions and descriptions.
- **Multiplayer Experience:** Connect with other players online and explore the virtual world together.
- **Exploration:** Discover vast dungeons, mysterious forests, and other intriguing locations.
- **Quests and Adventures:** Embark on epic quests, solve puzzles, and uncover hidden treasures.
- **Customization:** Create and customize your own character with unique abilities and attributes.
- **Combat System:** Engage in battles with monsters, NPCs, and other players using a robust combat system.
- **Persistence:** Your progress is saved, allowing you to continue your adventure across sessions.

## Getting Started

To get started with YAMUD, follow these steps:
1. Install Postgresql or another appropriate database, or a cloud based inwstance.  The system uses Entity Framework so you will need to put the details in appsettings to connect.
2. Install MongoDB (This is for ThothLog that performs logging for the system)

1. **Clone the Repository:** Clone this repository to your local machine.

   ```bash
   git clone https://github.com/your-username/yamud.git
   ```

1. **Set Up the Server:** Set up the YAMUD server by following the instructions in the `Server` directory. This typically involves running a C# WebAPI server.

1. **Set Up the Client:** Set up the YAMUD client by following the instructions in the `Client` directory. This typically involves running a Blazor WebAssembly client.

1. **Connect to the Game:** Once both the server and client are set up and running, open your favorite web browser and navigate to the client URL to connect to the game.

1. **Create Your Character:** Follow the on-screen instructions to create your character and start playing.

## Note: You will need to run the appropriate scripts or perform a database-update in order to create the schema

## Contributing

Contributions are welcome! If you'd like to contribute to YAMUD, please follow these guidelines:

- Fork the repository and create a new branch for your feature or bug fix.
- Make your changes and ensure that all tests pass.
- Submit a pull request with a clear description of your changes and the problem they solve.

## License

This project is licensed under the [MIT License](LICENSE).
