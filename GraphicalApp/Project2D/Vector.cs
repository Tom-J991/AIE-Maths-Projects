using System;

namespace Project2D
{
    public class Vector2
    {
        public float x, y;

        public Vector2()
        {
            x = 0.0f;
            y = 0.0f;
        }
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float Dot(Vector2 v)
        {
            return x*v.x + y*v.y;
        }
        public float MagnitudeSqr()
        {
            return x*x + y*y;
        }
        public float Magnitude()
        {
            return (float)Math.Sqrt(MagnitudeSqr());
        }

        public Vector2 Normalise()
        {
            if (Magnitude() == 0)
                return this / 1.0f;
            return this / Magnitude();
        }

        public static Vector2 Min(Vector2 a, Vector2 b)
        {
            return new Vector2(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
        }
        public static Vector2 Max(Vector2 a, Vector2 b)
        {
            return new Vector2(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
        }
        public static Vector2 Clamp(Vector2 t, Vector2 a, Vector2 b)
        {
            return Max(a, Min(b, t));
        }

        // Operator Overloading
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.x * b, a.y * b);
        }
        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2(a.x / b, a.y / b);
        }
        public static Vector2 operator *(float a, Vector2 b)
        {
            return new Vector2(a * b.x, a * b.y);
        }
        public static Vector2 operator /(float a, Vector2 b)
        {
            return new Vector2(a / b.x, a / b.y);
        }

        // Conversion from Raylib Types
        public static implicit operator System.Numerics.Vector2(Vector2 v) => new System.Numerics.Vector2(v.x, v.y);
        public static explicit operator Vector2(System.Numerics.Vector2 v) => new Vector2(v.X, v.Y);
    }
    public class Vector3
    {
        public float x, y, z;

        public Vector3()
        {
            x = 0.0f; y = 0.0f; z = 0.0f;
        }
        public Vector3(float x, float y, float z)
        {
            this.x = x; this.y = y; this.z = z;
        }

        public float Dot(Vector3 v)
        {
            return x * v.x + y * v.y + z * v.z;
        }
        public Vector3 Cross(Vector3 v)
        {
            return new Vector3(
                y * v.z - z * v.y,
                z * v.x - x * v.z,
                x * v.y - y * v.x
                );
        }
        public float MagnitudeSqr()
        {
            return x * x + y * y + z * z;
        }
        public float Magnitude()
        {
            return (float)Math.Sqrt(MagnitudeSqr());
        }

        public Vector3 Normalise()
        {
            return this / Magnitude();
        }

        public static Vector3 Min(Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z));
        }
        public static Vector3 Max(Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z));
        }
        public static Vector3 Clamp(Vector3 t, Vector3 a, Vector3 b)
        {
            return Max(a, Min(b, t));
        }

        // Operator Overloading
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3(a.x * b, a.y * b, a.z * b);
        }
        public static Vector3 operator /(Vector3 a, float b)
        {
            return new Vector3(a.x / b, a.y / b, a.z / b);
        }
        public static Vector3 operator *(float a, Vector3 b)
        {
            return new Vector3(a * b.x, a * b.y, a * b.z);
        }
        public static Vector3 operator /(float a, Vector3 b)
        {
            return new Vector3(a / b.x, a / b.y, a / b.z);
        }

        // Conversion from Raylib Types
        public static implicit operator System.Numerics.Vector3(Vector3 v) => new System.Numerics.Vector3(v.x, v.y, v.z);
        public static explicit operator Vector3(System.Numerics.Vector3 v) => new Vector3(v.X, v.Y, v.Z);
    }
    public class Vector4
    {
        public float x, y, z, w;

        public Vector4()
        {
            x = 0.0f; y = 0.0f; z = 0.0f; w = 0.0f;
        }
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x; this.y = y; this.z = z; this.w = w;
        }

        public float Dot(Vector4 v)
        {
            return x * v.x + y * v.y + z * v.z + w * v.w;
        }
        public Vector4 Cross(Vector4 v)
        {
            return new Vector4(
                y * v.z - z * v.y,
                z * v.x - x * v.z,
                x * v.y - y * v.x,
                0.0f);
        }
        public float MagnitudeSqr()
        {
            return x * x + y * y + z * z + w * w;
        }
        public float Magnitude()
        {
            return (float)Math.Sqrt(MagnitudeSqr());
        }

        public Vector4 Normalise()
        {
            return this / Magnitude();
        }

        public static Vector4 Min(Vector4 a, Vector4 b)
        {
            return new Vector4(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z), Math.Min(a.w, b.w));
        }
        public static Vector4 Max(Vector4 a, Vector4 b)
        {
            return new Vector4(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z), Math.Max(a.w, b.w));
        }
        public static Vector4 Clamp(Vector4 t, Vector4 a, Vector4 b)
        {
            return Max(a, Min(b, t));
        }

        // Operator Overloading
        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }
        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Vector4 operator *(Vector4 a, float b)
        {
            return new Vector4(a.x * b, a.y * b, a.z * b, a.w * b);
        }
        public static Vector4 operator /(Vector4 a, float b)
        {
            return new Vector4(a.x / b, a.y / b, a.z / b, a.w / b);
        }
        public static Vector4 operator *(float a, Vector4 b)
        {
            return new Vector4(a * b.x, a * b.y, a * b.z, a * b.w);
        }
        public static Vector4 operator /(float a, Vector4 b)
        {
            return new Vector4(a / b.x, a / b.y, a / b.z, a / b.w);
        }

        // Conversion from Raylib Types
        public static implicit operator System.Numerics.Vector4(Vector4 v) => new System.Numerics.Vector4(v.x, v.y, v.z, v.w);
        public static explicit operator Vector4(System.Numerics.Vector4 v) => new Vector4(v.X, v.Y, v.Z, v.W);
    }
}
