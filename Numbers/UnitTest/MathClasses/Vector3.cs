using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public struct Vector3
    {
        public float x, y, z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float Dot(Vector3 V)
        {
            // A ∙ B = AxBx + AyBy + AzBz
            float x, y, z;
            x = this.x * V.x;
            y = this.y * V.y;
            z = this.z * V.z;
            return x + y + z;
        }
        public Vector3 Cross(Vector3 V)
        {
            // A × B = [ AyBz - AzBy, AzBx - AxBz, AxBy - AyBx ]
            float x, y, z;
            x = (this.y * V.z) - (this.z * V.y);
            y = (this.z * V.x) - (this.x * V.z);
            z = (this.x * V.y) - (this.y * V.x);
            return new Vector3(x, y, z);
        }

        public float Magnitude()
        {
            // Pythagoras Theorem
            double length = Math.Sqrt(x * x + y * y + z * z);
            return (float)length;
        }
        public void Normalize()
        {
            // V / Magnitude
            float m = Magnitude();
            this.x /= m;
            this.y /= m;
            this.z /= m;
        }

        // Operator Overloads
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            float x, y, z;
            x = a.x + b.x;
            y = a.y + b.y;
            z = a.z + b.z;
            return new Vector3(x, y, z);
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            float x, y, z;
            x = a.x - b.x;
            y = a.y - b.y;
            z = a.z - b.z;
            return new Vector3(x, y, z);
        }
        public static Vector3 operator *(Vector3 a, float b)
        {
            float x, y, z;
            x = a.x * b;
            y = a.y * b;
            z = a.z * b;
            return new Vector3(x, y, z);
        }
        public static Vector3 operator *(float a, Vector3 b) { return b * a; }
        public static Vector3 operator /(Vector3 a, float b)
        {
            float x, y, z;
            x = a.x / b;
            y = a.y / b;
            z = a.z / b;
            return new Vector3(x, y, z);
        }
        public static Vector3 operator /(float a, Vector3 b) { return b / a; }
        public static Vector3 operator *(Matrix3 a, Vector3 b)
        {
            // Multiply similarly to a normal matrix 3
            float x, y, z;
            x = (a.m00 * b.x) + (a.m10 * b.y) + (a.m20 * b.z);
            y = (a.m01 * b.x) + (a.m11 * b.y) + (a.m21 * b.z);
            z = (a.m02 * b.x) + (a.m12 * b.y) + (a.m22 * b.z);
            return new Vector3(x, y, z);
        }
    }
}
