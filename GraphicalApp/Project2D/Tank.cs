using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Project2D
{
    class TankObject : GameObject
    {
        public AABB boundingBox;

        public SpriteObject tankBodySprite;
        public SpriteObject turretSprite;
        public GameObject turretObject;

        public Vector2 velocity = new Vector2(); // Velocity
        public Vector2 dVelocity = new Vector2(); // Velocity * Delta Time
        private float speed = 128.0f;
        private float rotSpeed = 2.0f;
        private float turretSpeed = 4.0f;
        private float accel = 50f;
        private float decel = 100f;

        public TankObject() : base()
        {
            boundingBox = new AABB();

            tankBodySprite = new SpriteObject();
            turretSprite = new SpriteObject();
            turretObject = new GameObject();

            tankBodySprite.LoadTexture("../../Images/tank/tankRed_outline.png");
            tankBodySprite.SetRotation(-90 * (float)(Math.PI / 180.0f));
            tankBodySprite.SetPosition(-tankBodySprite.Width / 2.0f, tankBodySprite.Height / 2.0f);

            turretSprite.LoadTexture("../../Images/tank/barrels/barrelRed_outline.png");
            turretSprite.SetRotation(-90 * (float)(Math.PI / 180.0f));
            turretSprite.SetPosition(0.0f, turretSprite.Width / 2.0f);

            turretObject.AddChild(turretSprite);
            AddChild(tankBodySprite);
            AddChild(turretObject);

            SetScale(0.25f, 0.25f);
            SetPosition(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f); // Default position
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            // Calculate bounding box.
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

            Vector2 move = moveV * Forward;
            move = move.Normalise() * speed;
            if (move.x != 0 || move.y != 0)
            {
                // Quick and dirty acceleration and deceleration.
                velocity.x = Approach(velocity.x, move.x, accel * deltaTime);
                velocity.y = Approach(velocity.y, move.y, accel * deltaTime);
            }
            else
            {
                velocity.x = Approach(velocity.x, 0.0f, decel * deltaTime);
                velocity.y = Approach(velocity.y, 0.0f, decel * deltaTime);
            }

            dVelocity = velocity * deltaTime;
            Translate(dVelocity.x, dVelocity.y);
        }
        public override void Draw()
        {
            base.Draw();
            // Debug
            if (GLOBALS.debugEnabled)
            {
                DrawLineEx(GlobalPosition, GlobalPosition + Right * 64, 1.0f, Color.RED);
                DrawLineEx(GlobalPosition, GlobalPosition + Forward * 128, 2.0f, Color.GREEN);
                DrawLineEx(turretObject.GlobalPosition, turretObject.GlobalPosition + turretObject.Forward * 64, 1.0f, Color.GREEN);
                BoundingBox tmp = new BoundingBox();
                tmp.min = boundingBox.min;
                tmp.max = boundingBox.max;
                Raylib.DrawBoundingBox(tmp, Color.BLUE);
            }
        }

        public void Collide(GameObject map)
        {
            // Simple AABB Wall Collision
            bool hit = false;
            for (int i = 0; i < map.GetChildrenCount(); i++)
            {
                var o = map.GetChild(i);
                if (o is WallObject)
                {
                    var w = (WallObject)o;
                    Vector3 xOff = new Vector3(dVelocity.x, 0.0f, 0.0f);
                    Vector3 yOff = new Vector3(0.0f, dVelocity.y, 0.0f);
                    // X
                    if (boundingBox.Overlaps(boundingBox.min + xOff, boundingBox.max + xOff, w.boundingBox))
                    {
                        Translate(-1.0f * Math.Sign(dVelocity.x) / 2.0f, 0.0f);
                        velocity.x = 0.0f;
                        hit = true;
                    }
                    // Y
                    if (boundingBox.Overlaps(boundingBox.min + yOff, boundingBox.max + yOff, w.boundingBox))
                    {
                        Translate(0.0f, -1.0f * Math.Sign(dVelocity.y) / 2.0f);
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
    class TrackParticle : GameObject
    {
        private SpriteObject trackSprite;

        public bool destroy = false;
        private float fadeSpeed = 0.5f;

        public TrackParticle() : base()
        {
            trackSprite = new SpriteObject();

            trackSprite.LoadTexture("../../Images/tank/tracks/tracksSmall.png");
            trackSprite.SetRotation(-90 * (float)(Math.PI / 180.0f));
            trackSprite.SetPosition(-trackSprite.Height / 1.5f, trackSprite.Width / 2.0f);
            trackSprite.alpha = 1.0f;
            AddChild(trackSprite);
        }

        public override void Update(float deltaTime)
        {
            if (destroy)
                return;
            base.Update(deltaTime);
            trackSprite.alpha -= deltaTime * fadeSpeed; // Fade over time.
            if (trackSprite.alpha <= 0)
                destroy = true; // Destroy if faded completely.
        }
        public override void Draw()
        {
            if (destroy)
                return;
            base.Draw();
        }
    }
    class BulletObject : GameObject
    {
        public AABB boundingBox;

        private SpriteObject bulletSprite;

        public bool destroy = false;
        private float timer = 0.0f;
        private float speed = 256.0f;

        public BulletObject() : base()
        {
            boundingBox = new AABB();

            bulletSprite = new SpriteObject();

            bulletSprite.LoadTexture("../../Images/bullets/bulletRed_outline.png");
            bulletSprite.SetRotation(90 * (float)(Math.PI / 180.0f));
            bulletSprite.SetPosition(bulletSprite.Width, -bulletSprite.Height / 4.0f);
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
            Vector2 move = Forward;
            move = move.Normalise() * speed;
            Translate(move.x * deltaTime, move.y * deltaTime);
        }
        public override void Draw()
        {
            if (destroy)
                return;
            base.Draw();
            // Debug
            if (GLOBALS.debugEnabled)
            {
                BoundingBox tmp = new BoundingBox();
                tmp.min = boundingBox.min;
                tmp.max = boundingBox.max;
                Raylib.DrawBoundingBox(tmp, Color.BLUE);
            }
        }

        public bool Collide(GameObject map)
        {
            for (int i = 0; i < map.GetChildrenCount(); i++)
            {
                var o = map.GetChild(i);
                if (o is WallObject)
                {
                    var w = (WallObject)o;
                    if (boundingBox.Overlaps(w.boundingBox))
                    {
                        destroy = true;
                        Console.WriteLine("Destroyed against wall");
                        return true; // Hit
                    }
                }
            }
            return false; // Didn't hit.
        }
    }
    class ExplosionParticle : GameObject
    {
        SpriteObject explosionSprite;

        bool destroy = false;
        float fadeSpeed = 0.8f;

        // Pick random texture.
        string[] textures = {
            "../../Images/explosion/explosion00.png",
            "../../Images/explosion/explosion01.png",
            "../../Images/explosion/explosion02.png",
            "../../Images/explosion/explosion03.png",
            "../../Images/explosion/explosion04.png",
            "../../Images/explosion/explosion05.png",
            "../../Images/explosion/explosion06.png",
            "../../Images/explosion/explosion07.png",
            "../../Images/explosion/explosion08.png",
        };

        public ExplosionParticle() : base()
        {
            explosionSprite = new SpriteObject();

            explosionSprite.LoadTexture(textures[Random.Shared.Next(0, textures.Length - 1)]);
            explosionSprite.SetRotation(-90 * (float)(Math.PI / 180.0f));
            explosionSprite.Scale(0.25f, 0.25f);
            explosionSprite.SetPosition(-explosionSprite.Width / 2.0f, explosionSprite.Height / 2.0f);
            explosionSprite.alpha = 1.0f;

            AddChild(explosionSprite);
        }

        public override void Update(float deltaTime)
        {
            if (destroy)
                return;
            base.Update(deltaTime);
            explosionSprite.alpha -= deltaTime * fadeSpeed; // Fade over time.
            explosionSprite.Rotate(0.001f * (float)(Math.PI / 180.0f));
            if (explosionSprite.alpha <= 0)
                destroy = true; // Destroy if faded completely.
        }
        public override void Draw()
        {
            if (destroy)
                return;
            base.Draw();
        }
    }
}
