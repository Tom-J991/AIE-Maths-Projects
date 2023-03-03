﻿using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Project2D
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);
            InitWindow(640, 480, "Tank Game");

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
