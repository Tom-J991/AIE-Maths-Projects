using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Project2D
{
    class WallObject : SpriteObject
    {
        public AABB boundingBox;
        public WallObject(float x, float y) : base()
        {
            LoadTexture("../../Images/floor.png");
            SetPosition(x, y);

            boundingBox = new AABB();
            Vector3[] points = { new Vector3(x, y, 0.0f), new Vector3(x + Width, y + Height, 0.0f) };
            boundingBox.Fit(points);
        }
    }
}
