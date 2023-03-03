using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Project2D
{

    class Program
    {
        static Game game;

        static void Main(string[] args)
        {
            game = new Game();

            SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);
            InitWindow(640, 480, "Tank Game");
            SetTargetFPS(60); // Frame Cap

            game.Init();

            while (!WindowShouldClose())
            {
                game.Update();
                game.Draw();
            }

            game.Shutdown();

            CloseWindow();
        }
    }
}
