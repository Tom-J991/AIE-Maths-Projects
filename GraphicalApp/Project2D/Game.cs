using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
using System.Security.Cryptography;
using Raylib;
using static Raylib.Raylib;

namespace Project2D
{
    public static class DEBUG
    {
        public static bool debugEnabled = true;
    }

    class TankObject : GameObject
    {
        public AABB boundingBox;

        public SpriteObject tankBodySprite;
        public SpriteObject turretSprite;
        public GameObject turretObject;

        private Vector2 velocity = new Vector2();
        private Vector2 dVelocity = new Vector2();
        private float speed = 256.0f;
        private float rotSpeed = 1.0f;
        private float turretSpeed = 2.0f;
        private float accel = 0.01f;
        private float decel = 0.05f;

        public TankObject() : base()
        {
            boundingBox = new AABB();

            tankBodySprite = new SpriteObject();
            turretSprite = new SpriteObject();
            turretObject = new GameObject();

            tankBodySprite.LoadTexture("../Images/tank/tankRed_outline.png");
            tankBodySprite.SetRotation(-90 * (float)(Math.PI / 180.0f));
            tankBodySprite.SetPosition(-tankBodySprite.Width / 2.0f, tankBodySprite.Height / 2.0f);

            turretSprite.LoadTexture("../Images/tank/barrels/barrelRed_outline.png");
            turretSprite.SetRotation(-90 * (float)(Math.PI / 180.0f));
            turretSprite.SetPosition(0.0f, turretSprite.Width / 2.0f);

            turretObject.AddChild(turretSprite);
            AddChild(tankBodySprite);
            AddChild(turretObject);

            SetScale(0.25f, 0.25f);
            SetPosition(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            Vector3 offset = new Vector3((tankBodySprite.Width / 2.0f), (tankBodySprite.Height / 2.0f), 0.0f);
            Vector3[] points = new Vector3[2];
            points[0] = new Vector3(LocalPosition.x, LocalPosition.y, 0.0f) - offset;
            points[1] = new Vector3((LocalPosition.x + tankBodySprite.Width), (LocalPosition.y + tankBodySprite.Height), 0.0f) - offset;
            boundingBox.Fit(points);

            // Get Input
            int moveH = ParseBool(IsKeyDown(KeyboardKey.KEY_D)) - ParseBool(IsKeyDown(KeyboardKey.KEY_A)); // left = -1, right = 1, neither/both = 0
            int moveV = ParseBool(IsKeyDown(KeyboardKey.KEY_W)) - ParseBool(IsKeyDown(KeyboardKey.KEY_S)); // down = -1, up = 1, neither/both = 0
            int turretMoveH = ParseBool(IsKeyDown(KeyboardKey.KEY_E)) - ParseBool(IsKeyDown(KeyboardKey.KEY_Q));

            // Move
            Rotate(moveH * rotSpeed * deltaTime);
            turretObject.Rotate(turretMoveH * turretSpeed * deltaTime);

            Vector2 move = moveV * Forward * speed;
            if (move.x != 0 || move.y != 0)
            {
                // Quick and dirty acceleration and deceleration.
                velocity.x = Approach(velocity.x, move.x, accel);
                velocity.y = Approach(velocity.y, move.y, accel);
            }
            else
            {
                velocity.x = Approach(velocity.x, 0.0f, decel);
                velocity.y = Approach(velocity.y, 0.0f, decel);
            }

            dVelocity = velocity * deltaTime;
            Translate(dVelocity.x, dVelocity.y);
        }
        public override void Draw()
        {
            base.Draw();
            // Debug
            if (DEBUG.debugEnabled)
            {
                DrawLineEx(GlobalPosition, GlobalPosition + Right * 64, 1.0f, Color.RED);
                DrawLineEx(GlobalPosition, GlobalPosition + Forward * 128, 2.0f, Color.GREEN);
                DrawLineEx(turretObject.GlobalPosition, turretObject.GlobalPosition + turretObject.Forward * 64, 1.0f, Color.GREEN);
                BoundingBox tmp = new BoundingBox();
                tmp.min = boundingBox.min;
                tmp.max = boundingBox.max;
                Raylib.Raylib.DrawBoundingBox(tmp, Color.BLUE);
            }
        }

        public void Collide(GameObject map)
        {
            // Wall Collision
            bool hit = false;
            for (int i = 0; i < map.GetChildrenCount(); i++)
            {
                var o = map.GetChild(i);
                if (o is WallObject)
                {
                    var w = (WallObject)o;
                    if (boundingBox.Overlaps(boundingBox.min, boundingBox.max + new Vector3(dVelocity.x, dVelocity.y, 0.0f), w.boundingBox))
                    {
                        
                        Translate(-Math.Sign(dVelocity.x), -Math.Sign(dVelocity.y));
                        velocity.x = 0.0f;
                        velocity.y = 0.0f;
                        hit = true;
                    }
                }
            }
        }

        internal float Approach(float a, float b, float shift)
        {
            if (a < b)
                return Math.Min(a + shift, b);
            else
                return Math.Max(a - shift, b);
        }

        internal int ParseBool(bool _b)
        {
            return _b ? 1 : 0; // Parses boolean as an Integer.
        }
    }
    class BulletObject : GameObject
    {
        public AABB boundingBox;

        private SpriteObject bulletSprite;

        public bool destroy = false;
        private float timer = 0.0f;
        private float speed = 1024.0f;

        public BulletObject() : base()
        {
            boundingBox = new AABB();

            bulletSprite = new SpriteObject();

            bulletSprite.LoadTexture("../Images/bullets/bulletRed_outline.png");
            bulletSprite.SetRotation(90 * (float)(Math.PI / 180.0f));
            bulletSprite.SetPosition(bulletSprite.Width, -bulletSprite.Height/4.0f);
            AddChild(bulletSprite);
        }

        public override void Update(float deltaTime)
        {
            if (destroy)
                return;
            base.Update(deltaTime);

            Vector3 offset = new Vector3((bulletSprite.Width / 2.0f), (bulletSprite.Height / 2.0f), 0.0f);
            Vector3[] points = new Vector3[2];
            points[0] = new Vector3(LocalPosition.x, LocalPosition.y, 0.0f) - offset;
            points[1] = new Vector3((LocalPosition.x + bulletSprite.Width), (LocalPosition.y + bulletSprite.Height), 0.0f) - offset;
            boundingBox.Fit(points);

            // Count Down
            timer += deltaTime;
            if (timer >= 1.5f)
                destroy = true; // Destroy after 1.5 seconds.

            // Move
            Vector2 move = Forward * deltaTime * speed;
            Translate(move.x, move.y);
        }

        public override void Draw()
        {
            if (destroy)
                return;
            base.Draw();
            // Debug
            if (DEBUG.debugEnabled)
            {
                BoundingBox tmp = new BoundingBox();
                tmp.min = boundingBox.min;
                tmp.max = boundingBox.max;
                Raylib.Raylib.DrawBoundingBox(tmp, Color.BLUE);
            }
        }

        public void Collide(GameObject map)
        {
            for (int i = 0; i < map.GetChildrenCount(); i++)
            {
                var o = map.GetChild(i);
                if (o is WallObject)
                {
                    var w = (WallObject)o;
                    if (boundingBox.Overlaps(w.boundingBox))
                        destroy = true;
                }
            }
        }
    }
    class WallObject : SpriteObject
    {
        public AABB boundingBox;
        public WallObject(float _x, float _y) : base()
        {
            LoadTexture("../Images/floor.png");
            SetPosition(_x, _y);

            boundingBox = new AABB();
            Vector3[] points = { new Vector3(_x, _y, 0.0f), new Vector3(_x + Width, _y + Height, 0.0f) };
            boundingBox.Fit(points);
        }
    }

    class Game
    {
        Stopwatch stopwatch = new Stopwatch();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private float deltaTime = 0.005f;

        RenderTexture2D screenBuffer;
        Shader screenShader;

        // Game Objects
        public GameObject sceneRoot;
        public GameObject map;
        public TankObject player;
        public List<BulletObject> bullets;

        int mapWidth = 20, mapHeight = 15;
        string mapLayout = 
            "--------------------" +
            "--------------------" +
            "------WWWWWWWW------" +
            "----WWWWWWWWWWWW----" +
            "----WWWWWWWWWWWW----" +
            "----WWWW--------W---" +
            "--WWWWWW------------" +
            "--WWWWWW--------W---" +
            "--WWWWWWWWWWWWWW----" +
            "--WWWWWWWWWWWWWW----" +
            "--WWWWWWWWWWWWWW----" +
            "----WWWWWWWWWWWW----" +
            "----WWWW----WWWW----" +
            "----WWWW----WWWW----" +
            "WWWWWWWWWWWWWWWWWWWW";

        public Game()
        {
        }

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }

            screenBuffer = LoadRenderTexture(GetScreenWidth(), GetScreenHeight());
            screenShader = LoadShader(null, "../Shaders/screen.fs");

            // Init Game
            sceneRoot = new GameObject();

            map = new GameObject();
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (mapLayout[y * mapWidth + x] == 'W')
                        map.AddChild(new WallObject(x*32, y*32));
                }
            }

            player = new TankObject();
            bullets = new List<BulletObject>();

            sceneRoot.AddChild(map);
            sceneRoot.AddChild(player);
        }

        public void Shutdown()
        {
            GameObject.Destroy();
        }

        float shootTimer = 0.0f;
        float fireRate = 0.1f;
        public void Update()
        {
            // Game Timings
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
                b.Collide(map);
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
                if (b.GlobalPosition.x > GetScreenWidth() || b.GlobalPosition.x < 0 ||
                    b.GlobalPosition.y > GetScreenHeight() || b.GlobalPosition.y < 0)
                    b.destroy = true;
                // Remove from scene after destroyed.
                if (b.destroy)
                {
                    sceneRoot.RemoveChild(b);
                    bullets.RemoveAt(i--);
                }
            }

            sceneRoot.Update(deltaTime);
        }

        public void Draw()
        {
            // Draw to Screen Buffer
            BeginTextureMode(screenBuffer);
            ClearBackground(Color.RAYWHITE);

            // Draw Game Objects
            sceneRoot.Draw();

            EndTextureMode();

            BeginDrawing();
            ClearBackground(Color.RAYWHITE);

            // Draw Screen Buffer
            BeginShaderMode(screenShader); // Enable Shader
            DrawTextureRec(screenBuffer.texture,
                new Rectangle(0.0f, 0.0f, screenBuffer.texture.width, -screenBuffer.texture.height), // Flip Y (OpenGL coordinates)
                new Raylib.Vector2(0.0f, 0.0f),
                Color.WHITE);
            EndShaderMode();

            // Draw UI
            if (DEBUG.debugEnabled)
            {
                DrawText(fps.ToString(), 10, 10, 14, Color.RED);
            }

            EndDrawing();
        }
    }
}
