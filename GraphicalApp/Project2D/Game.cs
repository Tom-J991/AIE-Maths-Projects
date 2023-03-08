using System;
using System.Collections.Generic;
using System.Diagnostics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Project2D
{
    class Game
    {
        Stopwatch stopwatch = new Stopwatch();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private float deltaTime = 0.005f;

        private Camera2D camera;
        private RenderTexture2D screenBuffer;
        private Shader screenShader;

        // Game Objects
        public GameObject sceneRoot;
        public GameObject map;
        public TankObject player;
        public List<BulletObject> bullets;

        int mapWidth = 20, mapHeight = 15;
        // W = Wall
        // P = Player position (Middle of map by default)
        string mapLayout = 
            "--------------------" +
            "--------------------" +
            "------WWWWWWWW------" +
            "----WWWWWWWWWWWW----" +
            "----WWWWWWWWWWWW----" +
            "----WWWW--------W---" +
            "--WWWWWW--------P---" +
            "--WWWWWW--------W---" +
            "--WWWWWWWWWWWWWW----" +
            "--WWWWWWWWWWWWWW----" +
            "--WWWWWWWWWWWWWW----" +
            "----WWWWWWWWWWWW----" +
            "----WWWW----WWWW----" +
            "----WWWW----WWWW----" +
            "----WWWW----WWWW----";

        public Game()
        { }

        public bool Init()
        {
            // Init Timing
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }

            // Preload Textures to avoid stalling during gameplay.
            // Particles
            GameObject.PreloadTexture("../../Images/explosion/explosion00.png");
            GameObject.PreloadTexture("../../Images/explosion/explosion01.png");
            GameObject.PreloadTexture("../../Images/explosion/explosion02.png");
            GameObject.PreloadTexture("../../Images/explosion/explosion03.png");
            GameObject.PreloadTexture("../../Images/explosion/explosion04.png");
            GameObject.PreloadTexture("../../Images/explosion/explosion05.png");
            GameObject.PreloadTexture("../../Images/explosion/explosion06.png");
            GameObject.PreloadTexture("../../Images/explosion/explosion07.png");
            GameObject.PreloadTexture("../../Images/explosion/explosion08.png");
            // Player
            GameObject.PreloadTexture("../../Images/tank/tracks/tracksSmall.png"); // Particle
            GameObject.PreloadTexture("../../Images/tank/tankRed_outline.png");
            GameObject.PreloadTexture("../../Images/tank/barrels/barrelRed_outline.png");
            GameObject.PreloadTexture("../../Images/bullets/bulletRed_outline.png");
            // Environment
            GameObject.PreloadTexture("../../Images/floor.png");

            // Create Game Objects and Init
            sceneRoot = new GameObject();

            map = new GameObject();
            player = new TankObject();
            bullets = new List<BulletObject>();

            // Init Map
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (mapLayout[y * mapWidth + x] == 'W')
                        map.AddChild(new WallObject(x * 32, y * 32)); // Scale the position by 32
                    if (mapLayout[y * mapWidth + x] == 'P')
                    {
                        player.SetPosition((x + 0.5f) * 32, (y + 0.5f) * 32); // Offset by 0.5f to account for player offset then scale the position by 32
                    }
                }
            }

            // Add to scene
            sceneRoot.AddChild(map);
            sceneRoot.AddChild(player);

            // Create Camera
            camera = new Camera2D();
            camera.rotation = 0.0f;
            camera.zoom = 2.0f;
            camera.offset = new Vector2(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);
            camera.target = player.GlobalPosition;

            // Create Screen Buffer
            screenShader = LoadShader(null, "../../Shaders/screen.fs"); // Post Processing
            screenBuffer = LoadRenderTexture(GetScreenWidth(), GetScreenHeight());

            return true;
        }
        public void Shutdown()
        {
            GameObject.Destroy(); // Unload all textures.
        }

        float shootTimer = 0.0f;
        float fireRate = 0.1f;
        float particleSpacing = 1.0f;
        Vector2 lastParticlePos = new Vector2();
        public void Update()
        {
            // Timing
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;

            // Insert game logic here
            // Collisions
            player.Collide(map);
            foreach (var b in bullets)
            {
                // Bullet collide.
                if (b.Collide(map))
                {
                    // Create Particle
                    ExplosionParticle e = new ExplosionParticle();
                    e.SetScale(0.25f, 0.25f);
                    e.SetPosition(b.GlobalPosition.x, b.GlobalPosition.y);
                    e.Rotate(Random.Shared.Next(-90, 90) + b.GlobalRotation);
                    sceneRoot.AddChild(e); // Add particle to scene.
                }
            }

            // Fire Bullets
            if (shootTimer > 0.0f)
                shootTimer -= deltaTime;
            bool fire = IsKeyPressed(KeyboardKey.KEY_SPACE); // Get Input
            if (fire && shootTimer <= 0.0f)
            {
                var playerTurret = player.GetChild(1);
                BulletObject bullet = new BulletObject();
                bullet.SetScale(0.25f, 0.25f);
                bullet.SetPosition(playerTurret.GlobalPosition.x + (playerTurret.Forward.x * 64), playerTurret.GlobalPosition.y + (playerTurret.Forward.y * 64)); // Fire out of end of turret.
                bullet.Rotate(playerTurret.GlobalRotation); // Set rotation relative to turret.

                bullets.Add(bullet); // Keep track of bullet.
                sceneRoot.AddChild(bullet); // Add to scene.

                shootTimer = fireRate; // Reset timer
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                var b = bullets[i];
                // Destroy bullets off screen
                var max = GetScreenToWorld2D(new Vector2(GetScreenWidth(), GetScreenHeight()), camera); // Screen to World Coordinates
                var min = GetScreenToWorld2D(new Vector2(0.0f, 0.0f), camera);
                if (b.GlobalPosition.x > max.X || b.GlobalPosition.x < min.X ||
                    b.GlobalPosition.y > max.Y || b.GlobalPosition.y < min.Y)
                {
                    b.destroy = true;
                    Console.WriteLine("Destroyed outside of screen bounds.");
                }
                // Remove from scene after destroyed.
                if (b.destroy)
                {
                    sceneRoot.RemoveChild(b);
                    bullets.RemoveAt(i--);
                }
            }
            // Create Track Particle
            Vector2 dir = (player.GlobalPosition - lastParticlePos).Normalise();
            if ((player.dVelocity.x != 0.0f || player.dVelocity.y != 0.0f)) // Only create track when moving.
            {
                while (dir.Dot(player.GlobalPosition - lastParticlePos) > particleSpacing * (104 /* <- Texture Height */ * 0.25f)) // Spawn rate based on distance
                {
                    TrackParticle t = new TrackParticle();
                    t.SetScale(0.25f, 0.25f);
                    t.SetPosition(player.GlobalPosition.x, player.GlobalPosition.y); // Position relative to player.
                    t.Rotate(player.GlobalRotation); // Rotation relative to player.

                    sceneRoot.AddChild(t); // Add track to scene.
                    sceneRoot.MoveChildToStart(t); // Move to start of list so it draws underneath.
                    lastParticlePos = t.GlobalPosition;
                }
            }

            // Update all Scene Objects
            sceneRoot.Update(deltaTime);

            // Update Camera
            camera.target = player.GlobalPosition;
        }

        public void Draw()
        {
            // Draw to Screen Buffer
            BeginTextureMode(screenBuffer);
            BeginMode2D(camera); // Enable Camera Transformation
            ClearBackground(Color.RAYWHITE);
            sceneRoot.Draw(); // Draw all objects in scene.
            EndMode2D();
            EndTextureMode();

            // Draw to Window
            BeginDrawing();
            // Draw Screen Buffer
            ClearBackground(Color.RAYWHITE);
            BeginShaderMode(screenShader); // Enable Post Processing Shader
            DrawTextureRec(screenBuffer.texture,
                new Rectangle(0.0f, 0.0f, screenBuffer.texture.width, -screenBuffer.texture.height), // Flip Y (OpenGL coordinates)
                new Vector2(0.0f, 0.0f),
                Color.WHITE);
            EndShaderMode();

            // Draw UI
            if (GLOBALS.debugEnabled)
            {
                DrawText(fps.ToString(), 10, 10, 14, Color.RED);
            }
            EndDrawing();
        }
    }
}
