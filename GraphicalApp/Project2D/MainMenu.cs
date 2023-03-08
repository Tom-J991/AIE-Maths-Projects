using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Project2D
{
    public class MainMenu
    {
        public int currentOption = 0;
        int maxOptions = 2;
        string options;

        public MainMenu()
        { }

        public bool Init()
        {
            options = ""; // Initialize
            return true;
        }
        public void Shutdown()
        {

        }

        public void Update()
        {
            // Input
            int keyPrim = (ParseBool(IsKeyReleased(KeyboardKey.KEY_UP)) - ParseBool(IsKeyReleased(KeyboardKey.KEY_DOWN)));
            int keySec = (ParseBool(IsKeyReleased(KeyboardKey.KEY_W)) - ParseBool(IsKeyReleased(KeyboardKey.KEY_S)));
            currentOption += keyPrim | keySec;
            if (currentOption < 0) // Loop back to max.
                currentOption = (maxOptions - 1);
            if (currentOption > (maxOptions - 1)) // Loop back to 0.
                currentOption = 0;
            // Update String
            options = ((currentOption == 0) ? ">" : " ") + " Play Game" + "\n";
            options += ((currentOption == 1) ? ">" : " ") + " Quit Game" + "\n";
        }
        public void Draw()
        {
            // Draw to Window
            BeginDrawing();
            ClearBackground(Color.RAYWHITE);
            // Draw Menu Text
            DrawText("Tank Test", (GetScreenWidth() / 2) - (MeasureText("Tank Test", 64) / 2), GetScreenHeight() / 3, 64, Color.ORANGE);
            DrawText(options, (GetScreenWidth() / 2) - (MeasureText(options, 24) / 2), GetScreenHeight() / 2, 24, Color.ORANGE);
            EndDrawing();
        }
        internal int ParseBool(bool _b)
        {
            return _b ? 1 : 0; // Parses boolean as an Integer.
        }
    }
}
