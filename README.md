# Chess Game - C# WPF Project

## Overview
This is a two-player chess game developed in C# using WPF. The game adheres to standard chess rules:

- **Implemented Chess Rules:**
  - **Pawn Promotion:** When a pawn reaches the opponent's back rank, it can be promoted to a queen, rook, bishop, or knight.
  - **En Passant:** A special pawn capture move that can only occur immediately after a pawn moves two squares forward from its starting position and lands beside an opponent's pawn.
  - **Insufficient Material:** The game declares a draw if neither player has enough pieces to deliver checkmate (e.g., king vs. king, or king and bishop vs. king).
  - **Fifty-Move Rule:** A draw is declared if 50 consecutive moves are made by both players without a pawn move or a capture.
  - **Threefold Repetition:** If the same position occurs three times with the same player to move and all possible moves available, the game is declared a draw.

## Features
- **Two-player mode only:** No AI or bot support; players take turns manually.
- **Pause and Resume:** You can pause the game and continue later.
- **Restart Game:** Start a new game at any time.

## Getting Started
1. Clone the repository and open the solution in Visual Studio.
2. Build the solution to install necessary dependencies.
3. Run the application to start playing chess!

## Acknowledgments
This project was developed using guidance from the chess game development tutorial by **[OttoBotCode](https://www.youtube.com/c/OttoBotCode)**. The guide provided a comprehensive foundation for implementing chess logic and UI design in C#.

## Screenshots

Here are some screenshots of the game:

<div style="display: flex; justify-content: space-around;">
  <img src="/ChessUI/Assets/Capture1.PNG" alt="Screenshot 1" width="200">
  <img src="/ChessUI/Assets/Capture2.PNG" alt="Screenshot 2" width="200">
  <img src="/ChessUI/Assets/Capture3.PNG" alt="Screenshot 3" width="200">
</div>


## Future Enhancements
- Add AI for single-player mode.
- Include additional chess rules and customizable options.

## License
This project is for educational purposes. All rights reserved by the author.
