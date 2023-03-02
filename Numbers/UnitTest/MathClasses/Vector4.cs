using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public struct Vector4
    {
        public float x, y, z, w;

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public float Dot(Vector4 V)
        {
            // A ∙ B = AxBx + AyBy + AzBz + AwBw
            float x, y, z, w;
            x = this.x * V.x;
            y = this.y * V.y;
            z = this.z * V.z;
            w = this.w * V.w;
            return x + y + z + w;
        }
        public Vector4 Cross(Vector4 V)
        {
            // A × B = [ AyBz - AzBy, AzBx - AxBz, AxBy - AyBx ]
            // w is unused for Vector4 cross product function since it functions the same as a Vector3.
            float x, y, z, w;
            x = (this.y * V.z) - (this.z * V.y);
            y = (this.z * V.x) - (this.x * V.z);
            z = (this.x * V.y) - (this.y * V.x);
            w = 0;
            return new Vector4(x, y, z, w);
        }

        public float Magnitude()
        {
            // Pythagoras Theorem
            double length = Math.Sqrt(x * x + y * y + z * z + w * w);
            return (float)length;
        }
        public void Normalize()
        {
            // V / Magnitude
            float m = Magnitude();
            this.x /= m;
            this.y /= m;
            this.z /= m;
            this.w /= m;
        }


        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            float x, y, z, w;
            x = a.x + b.x;
            y = a.y + b.y;
            z = a.z + b.z;
            w = a.w + b.w;
            return new Vector4(x, y, z, w);
        }
        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            float x, y, z, w;
            x = a.x - b.x;
            y = a.y - b.y;
            z = a.z - b.z;
            w = a.w - b.w;
            return new Vector4(x, y, z, w);
        }
        public static Vector4 operator *(Vector4 a, float b)
        {
            float x, y, z, w;
            x = a.x * b;
            y = a.y * b;
            z = a.z * b;
            w = a.w * b;
            return new Vector4(x, y, z, w);
        }
        public static Vector4 operator *(float a, Vector4 b) { return b * a; }
        public static Vector4 operator /(Vector4 a, float b)
        {
            float x, y, z, w;
            x = a.x / b;
            y = a.y / b;
            z = a.z / b;
            w = a.w / b;
            return new Vector4(x, y, z, w);
        }
        public static Vector4 operator /(float a, Vector4 b) { return b / a; }
        public static Vector4 operator *(Matrix4 a, Vector4 b)
        {
            // Multiply similarly to a normal matrix 4
            float x, y, z, w;
            x = (a.m00 * b.x) + (a.m10 * b.y) + (a.m20 * b.z) + (a.m30 * b.w);
            y = (a.m01 * b.x) + (a.m11 * b.y) + (a.m21 * b.z) + (a.m31 * b.w);
            z = (a.m02 * b.x) + (a.m12 * b.y) + (a.m22 * b.z) + (a.m32 * b.w);
            w = (a.m03 * b.x) + (a.m13 * b.y) + (a.m23 * b.z) + (a.m33 * b.w);
            return new Vector4(x, y, z, w);
        }
    }
}
