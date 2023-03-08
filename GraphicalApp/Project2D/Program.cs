using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Project2D
{
    public static class GLOBALS
    {
        public static bool debugEnabled = false;
        public static GameScreen currentScreen = GameScreen.Menu;
    }
    public enum GameScreen
    {
        Menu,
        GamePlay
    }

    class Program
    {
        static Game game;
        static MainMenu mainMenu;

        static void Main(string[] args)
        {
            game = new Game();
            mainMenu = new MainMenu();

            SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);
            InitWindow(640, 480, "Tank Test");
            SetTargetFPS(60); // Frame Cap

            // Goto Main Menu
            GLOBALS.currentScreen = GameScreen.Menu;
            StartScreen();
        }

        public static void StartScreen()
        {
            switch (GLOBALS.currentScreen)
            {
                case GameScreen.Menu:
                    mainMenu.Init();
                    bool quit = false;
                    while (!WindowShouldClose())
                    {
                        mainMenu.Update();
                        mainMenu.Draw();
                        // Handle Menu Inputs
                        if (IsKeyReleased(KeyboardKey.KEY_ENTER) || IsKeyReleased(KeyboardKey.KEY_SPACE))
                        {
                            switch (mainMenu.currentOption)
                            {
                                case 0: // Play
                                    mainMenu.Shutdown();
                                    GLOBALS.currentScreen = GameScreen.GamePlay;
                                    StartScreen();
                                    break;
                                case 1: // Quit
                                    mainMenu.Shutdown();
                                    quit = true;
                                    break;
                            }
                            break;
                        }
                    }
                    if (quit) // Avoids some errors when in gameplay.
                        CloseWindow();
                    break;
                case GameScreen.GamePlay:
                    game.Init();
                    while (!WindowShouldClose())
                    {
                        game.Update();
                        game.Draw();
                    }
                    game.Shutdown();
                    CloseWindow();
                    break;
            }
        }
    }
}
