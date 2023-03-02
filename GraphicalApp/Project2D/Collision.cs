using System;

namespace Project2D
{
    public class Ray
    {
        Vector3 origin;
        Vector3 direction;
        float length;

        public Ray()
        { }
        public Ray(Vector3 origin, Vector3 direction, float length = float.MaxValue)
        {
            this.origin = origin;
            this.direction = direction;
            this.length = length;
        }

        float Clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        public Vector3 ClosestPoint(Vector3 point)
        {
            Vector3 p = point - origin;
            float t = Clamp(p.Dot(direction), 0, length);
            return origin + direction * t;
        }

        public bool Intersects(AABB aabb, Vector3 I = null, Vector3 R = null)
        {
            float xmin, xmax, ymin, ymax;

            // Get Min/Max
            if (direction.x < 0)
            {
                xmin = (aabb.max.x - origin.x) / direction.x;
                xmax = (aabb.min.x - origin.x) / direction.x;
            }
            else
            {
                xmin = (aabb.min.x - origin.x) / direction.x;
                xmax = (aabb.max.x - origin.x) / direction.x;
            }
            if (direction.y < 0)
            {
                ymin = (aabb.max.y - origin.y) / direction.y;
                ymax = (aabb.min.y - origin.y) / direction.y;
            }
            else
            {
                ymin = (aabb.min.y - origin.y) / direction.y;
                ymax = (aabb.max.y - origin.y) / direction.y;
            }

            // Is within box?
            if (xmin > ymax || ymin > xmax)
                return false;

            float t = Math.Max(xmin, ymin);
            if (t >= 0 && t <= length)
            {
                // Get intersection point
                if (I != null)
                    I = origin + direction * t;
                // Get reflection point
                if (R != null)
                {
                    Vector3 N; // Normal Vector
                    if (t == xmin)
                    {
                        if (direction.x < 0) // Right Side
                            N = new Vector3(1, 0, 1);
                        else // Left Side
                            N = new Vector3(-1, 0, 1);
                    }
                    else
                    {
                        if (direction.y < 0) // Top
                            N = new Vector3(0, 1, 1);
                        else // Bottom
                            N = new Vector3(0, -1, 1);
                    }

                    Vector3 P = direction * (length - t); // Penetration Vector
                    float p = P.Dot(N);                   // Amount

                    // Get Reflected Vector
                    R = N * -2 * p + P;
                }
                return true; // Within Range
            }

            return false; // Not within range.
        }
    }

    public class AABB
    {
        public Vector3 min = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
        public Vector3 max = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

        public AABB()
        { }
        public AABB(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public Vector3 Center()
        {
            return (min + max) * 0.5f;
        }
        public Vector3 Extents()
        {
            return new Vector3(
                Math.Abs(max.x - min.x) * 0.5f,
                Math.Abs(max.y - min.y) * 0.5f,
                Math.Abs(max.z - min.z) * 0.5f
                );
        }
        public Vector3[] Corners()
        {
            Vector3[] corners = new Vector3[4];
            corners[0] = min;
            corners[1] = new Vector3(min.x, max.y, min.z);
            corners[2] = max;
            corners[3] = new Vector3(max.x, min.y, min.z);
            return corners;
        }

        public void Fit(Vector3[] points)
        {
            min = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            max = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

            foreach (Vector3 p in points)
            {
                min = Vector3.Min(min, p);
                max = Vector3.Max(max, p);
            }
        }

        public Vector3 ClosestPoint(Vector3 p)
        {
            return Vector3.Clamp(p, min, max);
        }

        // AABB Detection
        public bool Overlaps(Vector3 p)
        {
            return !(p.x < min.x || p.y < min.y || p.x > max.x || p.y > max.y);
        }
        public bool Overlaps(Vector3 min, Vector3 max, AABB other)
        {
            bool collX = max.x >= other.min.x && other.max.x >= min.x;
            bool collY = max.y >= other.min.y && other.max.y >= min.y;
            return collX && collY;
        }
        public bool Overlaps(AABB other)
        {
            return Overlaps(this.min, this.max, other);
        }
    }
}
