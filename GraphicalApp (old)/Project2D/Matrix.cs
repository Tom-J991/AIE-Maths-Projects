using System;

namespace Project2D
{
    public class Matrix3
    {
        public float m00, m01, m02;
        public float m10, m11, m12;
        public float m20, m21, m22;

        public Matrix3()
        {
            m00 = 0.0f; m01 = 0.0f; m02 = 0.0f;
            m10 = 0.0f; m11 = 0.0f; m12 = 0.0f;
            m20 = 0.0f; m21 = 0.0f; m22 = 0.0f;
        }
        public Matrix3(float i)
        {
            m00 = i; m01 = 0.0f; m02 = 0.0f;
            m10 = 0.0f; m11 = i; m12 = 0.0f;
            m20 = 0.0f; m21 = 0.0f; m22 = i;
        }
        public Matrix3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22)
        {
            this.m00 = m00; this.m01 = m01; this.m02 = m02;
            this.m10 = m10; this.m11 = m11; this.m12 = m12;
            this.m20 = m20; this.m21 = m21; this.m22 = m22;
        }

        public void Set(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22)
        {
            this.m00 = m00; this.m01 = m01; this.m02 = m02;
            this.m10 = m10; this.m11 = m11; this.m12 = m12;
            this.m20 = m20; this.m21 = m21; this.m22 = m22;
        }
        public void Set(Matrix3 m)
        {
            Set(m.m00, m.m01, m.m02, m.m10, m.m11, m.m12, m.m20, m.m21, m.m22);
        }

        // Scale
        public void SetScaled(float x, float y, float z)
        {
            Set(
                x, 0.0f, 0.0f,
                0.0f, y, 0.0f,
                0.0f, 0.0f, z
            );
        }
        public void SetScaled(Vector3 v) { SetScaled(v.x, v.y, v.z); }
        public void SetScaled(Vector2 v) { SetScaled(v.x, v.y, 1.0f); }
        public void Scale(float x, float y, float z)
        {
            Matrix3 m = new Matrix3();
            m.SetScaled(x, y, z);
            Set(this * m);
        }
        public void Scale(Vector3 v) { Scale(v.x, v.y, v.z); }
        public void Scale(Vector2 v) { Scale(v.x, v.y, 1.0f); }

        // Rotation
        public void SetRotateX(double a)
        {
            // Angle in Radians
            float sin = (float)Math.Sin(a);
            float cos = (float)Math.Cos(a);
            Set(
                1.0f, 0.0f, 0.0f,
                0.0f, cos, sin,
                0.0f, -sin, cos
                );
        }
        public void RotateX(double a)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateX(a);
            Set(this * m);
        }
        public void SetRotateY(double a)
        {
            // Angle in Radians
            float sin = (float)Math.Sin(a);
            float cos = (float)Math.Cos(a);
            Set(
                cos, 0.0f, -sin,
                0.0f, 1.0f, 0.0f,
                sin, 0.0f, cos
                );
        }
        public void RotateY(double a)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateY(a);
            Set(this * m);
        }
        public void SetRotateZ(double a)
        {
            // Angle in Radians
            float sin = (float)Math.Sin(a);
            float cos = (float)Math.Cos(a);
            Set(
                cos, sin, 0.0f,
                -sin, cos, 0.0f,
                0.0f, 0.0f, 1.0f
                );
        }
        public void RotateZ(double a)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateZ(a);
            Set(this * m);
        }

        public void SetEuler(float pitch, float yaw, float roll)
        {
            Matrix3 x = new Matrix3();
            Matrix3 y = new Matrix3();
            Matrix3 z = new Matrix3();
            x.SetRotateX(pitch);
            y.SetRotateY(roll);
            z.SetRotateZ(yaw);
            Set(z * y * x);
        }

        // Translation
        public void SetTranslation(float x, float y)
        {
            m20 = x; m21 = y; 
        }
        public void SetTranslation(Vector3 v) { SetTranslation(v.x, v.y); }
        public void SetTranslation(Vector2 v) { SetTranslation(v.x, v.y); }
        public void Translate(float x, float y)
        {
            m20 += x; m21 += y;
        }
        public void Translate(Vector3 v) { Translate(v.x, v.y); }
        public void Translate(Vector2 v) { Translate(v.x, v.y); }

        // Operator Overloading
        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
        {
            return new Matrix3(
                a.m00 * b.m00 + a.m10 * b.m01 + a.m20 * b.m02,
                a.m01 * b.m00 + a.m11 * b.m01 + a.m21 * b.m02,
                a.m02 * b.m00 + a.m12 * b.m01 + a.m22 * b.m02,
                //
                a.m00 * b.m10 + a.m10 * b.m11 + a.m20 * b.m12,
                a.m01 * b.m10 + a.m11 * b.m11 + a.m21 * b.m12,
                a.m02 * b.m10 + a.m12 * b.m11 + a.m22 * b.m12,
                //
                a.m00 * b.m20 + a.m10 * b.m21 + a.m20 * b.m22,
                a.m01 * b.m20 + a.m11 * b.m21 + a.m21 * b.m22,
                a.m02 * b.m20 + a.m12 * b.m21 + a.m22 * b.m22
                );
        }
        public static Vector3 operator *(Matrix3 a, Vector3 b)
        {
            return new Vector3(
                b.x + a.m00 * b.y * a.m10 + b.z * a.m20,
                b.x + a.m01 * b.y * a.m11 + b.z * a.m21,
                b.x + a.m02 * b.y * a.m12 + b.z * a.m22
                );
        }
    }
    public class Matrix4
    {
        public float m00, m01, m02, m03;
        public float m10, m11, m12, m13;
        public float m20, m21, m22, m23;
        public float m30, m31, m32, m33;

        public Matrix4()
        {
            m00 = 0.0f; m01 = 0.0f; m02 = 0.0f; m03 = 0.0f;
            m10 = 0.0f; m11 = 0.0f; m12 = 0.0f; m13 = 0.0f;
            m20 = 0.0f; m21 = 0.0f; m22 = 0.0f; m23 = 0.0f;
            m30 = 0.0f; m31 = 0.0f; m32 = 0.0f; m33 = 0.0f;
        }
        public Matrix4(float i)
        {
            m00 = i; m01 = 0.0f; m02 = 0.0f; m03 = 0.0f;
            m10 = 0.0f; m11 = i; m12 = 0.0f; m13 = 0.0f;
            m20 = 0.0f; m21 = 0.0f; m22 = i; m23 = 0.0f;
            m30 = 0.0f; m31 = 0.0f; m32 = 0.0f; m33 = i;
        }
        public Matrix4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23, float m30, float m31, float m32, float m33)
        {
            this.m00 = m00; this.m01 = m01; this.m02 = m02; this.m03 = m03;
            this.m10 = m10; this.m11 = m11; this.m12 = m12; this.m13 = m13;
            this.m20 = m20; this.m21 = m21; this.m22 = m22; this.m23 = m23;
            this.m30 = m30; this.m31 = m31; this.m32 = m32; this.m33 = m33;
        }

        public void Set(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23, float m30, float m31, float m32, float m33)
        {
            this.m00 = m00; this.m01 = m01; this.m02 = m02; this.m03 = m03;
            this.m10 = m10; this.m11 = m11; this.m12 = m12; this.m13 = m13;
            this.m20 = m20; this.m21 = m21; this.m22 = m22; this.m23 = m23;
            this.m30 = m30; this.m31 = m31; this.m32 = m32; this.m33 = m33;
        }
        public void Set(Matrix4 m)
        {
            Set(m.m00, m.m01, m.m02, m.m03, m.m10, m.m11, m.m12, m.m13, m.m20, m.m21, m.m22, m.m23, m.m30, m.m31, m.m32, m.m33);
        }

        // Scale
        public void SetScaled(float x, float y, float z)
        {
            Set(
                x, 0.0f, 0.0f, 0.0f,
                0.0f, y, 0.0f, 0.0f,
                0.0f, 0.0f, z, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f
            );
        }
        public void SetScaled(Vector4 v) { SetScaled(v.x, v.y, v.z); }
        public void SetScaled(Vector3 v) { SetScaled(v.x, v.y, v.z); }
        public void Scale(float x, float y, float z)
        {
            Matrix4 m = new Matrix4();
            m.SetScaled(x, y, z);
            Set(this * m);
        }
        public void Scale(Vector4 v) { Scale(v.x, v.y, v.z); }
        public void Scale(Vector3 v) { Scale(v.x, v.y, v.z); }

        // Rotation
        public void SetRotateX(double a)
        {
            // Angle in Radians
            float sin = (float)Math.Sin(a);
            float cos = (float)Math.Cos(a);
            Set(
                1.0f, 0.0f, 0.0f, 0.0f,
                0.0f, cos, sin, 0.0f,
                0.0f, -sin, cos, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f
            );
        }
        public void RotateX(double a)
        {
            Matrix4 m = new Matrix4();
            m.SetRotateX(a);
            Set(this * m);
        }
        public void SetRotateY(double a)
        {
            // Angle in Radians
            float sin = (float)Math.Sin(a);
            float cos = (float)Math.Cos(a);
            Set(
                cos, 0.0f, -sin, 0.0f,
                0.0f, 1.0f, 0.0f, 0.0f,
                sin, 0.0f, cos, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f
            );
        }
        public void RotateY(double a)
        {
            Matrix4 m = new Matrix4();
            m.SetRotateY(a);
            Set(this * m);
        }
        public void SetRotateZ(double a)
        {
            // Angle in Radians
            float sin = (float)Math.Sin(a);
            float cos = (float)Math.Cos(a);
            Set(
                cos, sin, 0.0f, 0.0f,
                -sin, cos, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f
            );
        }
        public void RotateZ(double a)
        {
            Matrix4 m = new Matrix4();
            m.SetRotateZ(a);
            Set(this * m);
        }

        public void SetEuler(float pitch, float yaw, float roll)
        {
            Matrix4 x = new Matrix4();
            Matrix4 y = new Matrix4();
            Matrix4 z = new Matrix4();
            x.SetRotateX(pitch);
            y.SetRotateY(roll);
            z.SetRotateZ(yaw);
            Set(z * y * x);
        }

        // Translation
        public void SetTranslation(float x, float y, float z)
        {
            m30 = x; m31 = y; m32 = z; m33 = 1.0f;
        }
        public void SetTranslation(Vector4 v) { SetTranslation(v.x, v.y, v.z); }
        public void SetTranslation(Vector3 v) { SetTranslation(v.x, v.y, v.z); }
        public void Translate(float x, float y, float z)
        {
            m30 += x; m31 += y; m32 += z;
        }
        public void Translate(Vector4 v) { Translate(v.x, v.y, v.z); }
        public void Translate(Vector3 v) { Translate(v.x, v.y, v.z); }

        // Operator Overloading
        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            return new Matrix4(
                a.m00 * b.m00 + a.m10 * b.m01 + a.m20 * b.m02 + a.m30 * b.m03,
                a.m01 * b.m00 + a.m11 * b.m01 + a.m21 * b.m02 + a.m31 * b.m03,
                a.m02 * b.m00 + a.m12 * b.m01 + a.m22 * b.m02 + a.m32 * b.m03,
                a.m03 * b.m00 + a.m13 * b.m01 + a.m23 * b.m02 + a.m33 * b.m03,
                //
                a.m00 * b.m10 + a.m10 * b.m11 + a.m20 * b.m12 + a.m30 * b.m13,
                a.m01 * b.m10 + a.m11 * b.m11 + a.m21 * b.m12 + a.m31 * b.m13,
                a.m02 * b.m10 + a.m12 * b.m11 + a.m22 * b.m12 + a.m32 * b.m13,
                a.m03 * b.m10 + a.m13 * b.m11 + a.m23 * b.m12 + a.m33 * b.m13,
                //
                a.m00 * b.m20 + a.m10 * b.m21 + a.m20 * b.m22 + a.m30 * b.m23,
                a.m01 * b.m20 + a.m11 * b.m21 + a.m21 * b.m22 + a.m31 * b.m23,
                a.m02 * b.m20 + a.m12 * b.m21 + a.m22 * b.m22 + a.m32 * b.m23,
                a.m03 * b.m20 + a.m13 * b.m21 + a.m23 * b.m22 + a.m33 * b.m23,
                //
                a.m00 * b.m30 + a.m10 * b.m31 + a.m20 * b.m32 + a.m30 * b.m33,
                a.m01 * b.m30 + a.m11 * b.m31 + a.m21 * b.m32 + a.m31 * b.m33,
                a.m02 * b.m30 + a.m12 * b.m31 + a.m22 * b.m32 + a.m32 * b.m33,
                a.m03 * b.m30 + a.m13 * b.m31 + a.m23 * b.m32 + a.m33 * b.m33
                );
        }
        public static Vector4 operator *(Matrix4 a, Vector4 b)
        {
            return new Vector4(
                b.x + a.m00 * b.y * a.m10 + b.z * a.m20 + b.w * a.m30,
                b.x + a.m01 * b.y * a.m11 + b.z * a.m21 + b.w * a.m31,
                b.x + a.m02 * b.y * a.m12 + b.z * a.m22 + b.w * a.m32,
                b.x + a.m03 * b.y * a.m13 + b.z * a.m23 + b.w * a.m33
                );
        }
    }
}
