using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MathClasses
{
    public struct Matrix4
    {
        public float m00, m01, m02, m03;
        public float m10, m11, m12, m13;
        public float m20, m21, m22, m23;
        public float m30, m31, m32, m33;

        public Matrix4(float i)
        {
            this.m00 = i; this.m01 = 0; this.m02 = 0; this.m03 = 0;
            this.m10 = 0; this.m11 = i; this.m12 = 0; this.m13 = 0;
            this.m20 = 0; this.m21 = 0; this.m22 = i; this.m23 = 0;
            this.m30 = 0; this.m31 = 0; this.m32 = 0; this.m33 = i;
        }
        public Matrix4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23, float m30, float m31, float m32, float m33)
        {
            this.m00 = m00; this.m01 = m01; this.m02 = m02; this.m03 = m03;
            this.m10 = m10; this.m11 = m11; this.m12 = m12; this.m13 = m13;
            this.m20 = m20; this.m21 = m21; this.m22 = m22; this.m23 = m23;
            this.m30 = m30; this.m31 = m31; this.m32 = m32; this.m33 = m33;
        }

        public void SetTranslation(float x, float y, float z)
        {
            this.m30 = x;
            this.m31 = y;
            this.m32 = z;
            this.m33 = 1;
        }

        public void SetRotateX(float r)
        {
            float c = (float)Math.Cos(r);
            float s = (float)Math.Sin(r);

            // 1      0       0
            // 0    cos(a)  sin(a)
            // 0   -sin(a)  cos(a)
            this.m00 = 1; this.m01 = 0; this.m02 = 0; this.m03 = 0;
            this.m10 = 0; this.m11 = c; this.m12 = s; this.m13 = 0;
            this.m20 = 0; this.m21 = -s; this.m22 = c; this.m23 = 0;
            this.m30 = 0; this.m31 = 0; this.m32 = 0; this.m33 = 1;
        }
        public void SetRotateY(float r)
        {
            float c = (float)Math.Cos(r);
            float s = (float)Math.Sin(r);

            // cos(a)  0   -sin(a)
            //   0     1     0
            // sin(a)  0    cos(a)
            this.m00 = c; this.m01 = 0; this.m02 = -s; this.m03 = 0;
            this.m10 = 0; this.m11 = 1; this.m12 = 0; this.m13 = 0;
            this.m20 = s; this.m21 = 0; this.m22 = c; this.m23 = 0;
            this.m30 = 0; this.m31 = 0; this.m32 = 0; this.m33 = 1;
        }
        public void SetRotateZ(float r)
        {
            float c = (float)Math.Cos(r);
            float s = (float)Math.Sin(r);

            //  cos(a)  sin(a)  0
            // -sin(a)  cos(a)  0
            //    0       0     1
            this.m00 = c; this.m01 = s; this.m02 = 0; this.m03 = 0;
            this.m10 = -s; this.m11 = c; this.m12 = 0; this.m13 = 0;
            this.m20 = 0; this.m21 = 0; this.m22 = 1; this.m23 = 0;
            this.m30 = 0; this.m31 = 0; this.m32 = 0; this.m33 = 1;
        }

        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            // Each element of the new matrix (mn) is the result of a dot product of Matrix A's row (m) and Matrix B's column (n)
            float m00, m01, m02, m03;
            float m10, m11, m12, m13;
            float m20, m21, m22, m23;
            float m30, m31, m32, m33;
            // First Row
            m00 = (a.m00 * b.m00) + (a.m10 * b.m01) + (a.m20 * b.m02) + (a.m30 * b.m03);
            m01 = (a.m01 * b.m00) + (a.m11 * b.m01) + (a.m21 * b.m02) + (a.m31 * b.m03);
            m02 = (a.m02 * b.m00) + (a.m12 * b.m01) + (a.m22 * b.m02) + (a.m32 * b.m03);
            m03 = (a.m03 * b.m00) + (a.m13 * b.m01) + (a.m23 * b.m02) + (a.m33 * b.m03);
            // Second Row
            m10 = (a.m00 * b.m10) + (a.m10 * b.m11) + (a.m20 * b.m12) + (a.m30 * b.m13);
            m11 = (a.m01 * b.m10) + (a.m11 * b.m11) + (a.m21 * b.m12) + (a.m31 * b.m13);
            m12 = (a.m02 * b.m10) + (a.m12 * b.m11) + (a.m22 * b.m12) + (a.m32 * b.m13);
            m13 = (a.m03 * b.m10) + (a.m13 * b.m11) + (a.m23 * b.m12) + (a.m33 * b.m13);
            // Third Row
            m20 = (a.m00 * b.m20) + (a.m10 * b.m21) + (a.m20 * b.m22) + (a.m30 * b.m23);
            m21 = (a.m01 * b.m20) + (a.m11 * b.m21) + (a.m21 * b.m22) + (a.m31 * b.m23);
            m22 = (a.m02 * b.m20) + (a.m12 * b.m21) + (a.m22 * b.m22) + (a.m32 * b.m23);
            m23 = (a.m03 * b.m20) + (a.m13 * b.m21) + (a.m23 * b.m22) + (a.m33 * b.m23);
            // Fourth Row
            m30 = (a.m00 * b.m30) + (a.m10 * b.m31) + (a.m20 * b.m32) + (a.m30 * b.m33);
            m31 = (a.m01 * b.m30) + (a.m11 * b.m31) + (a.m21 * b.m32) + (a.m31 * b.m33);
            m32 = (a.m02 * b.m30) + (a.m12 * b.m31) + (a.m22 * b.m32) + (a.m32 * b.m33);
            m33 = (a.m03 * b.m30) + (a.m13 * b.m31) + (a.m23 * b.m32) + (a.m33 * b.m33);

            return new Matrix4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
        }
    }
}
