using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public struct Matrix3
    {
        public float m00, m01, m02;
        public float m10, m11, m12;
        public float m20, m21, m22;

        public Matrix3(float i)
        {
            this.m00 = i; this.m01 = 0; this.m02 = 0;
            this.m10 = 0; this.m11 = i; this.m12 = 0;
            this.m20 = 0; this.m21 = 0; this.m22 = i;
        }
        public Matrix3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22)
        {
            this.m00 = m00; this.m01 = m01; this.m02 = m02;
            this.m10 = m10; this.m11 = m11; this.m12 = m12;
            this.m20 = m20; this.m21 = m21; this.m22 = m22;
        }

        public void SetTranslation(float x, float y)
        {
            this.m20 = x;
            this.m21 = y;
            this.m22 = 1;
        }

        public void SetRotateX(float r)
        {
            float c = (float)Math.Cos(r);
            float s = (float)Math.Sin(r);

            // 1      0       0
            // 0    cos(a)  sin(a)
            // 0   -sin(a)  cos(a)
            this.m00 = 1; this.m01 = 0; this.m02 = 0;
            this.m10 = 0; this.m11 = c; this.m12 = s;
            this.m20 = 0; this.m21 = -s; this.m22 = c;
        }
        public void SetRotateY(float r)
        {
            float c = (float)Math.Cos(r);
            float s = (float)Math.Sin(r);

            // cos(a)  0   -sin(a)
            //   0     1     0
            // sin(a)  0    cos(a)
            this.m00 = c; this.m01 = 0; this.m02 = -s;
            this.m10 = 0; this.m11 = 1; this.m12 = 0;
            this.m20 = s; this.m21 = 0; this.m22 = c;
        }
        public void SetRotateZ(float r)
        {
            float c = (float)Math.Cos(r);
            float s = (float)Math.Sin(r);

            //  cos(a)  sin(a)  0
            // -sin(a)  cos(a)  0
            //    0       0     1
            this.m00 = c; this.m01 = s; this.m02 = 0;
            this.m10 = -s; this.m11 = c; this.m12 = 0;
            this.m20 = 0; this.m21 = 0; this.m22 = 1;
        }

        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
        {
            // Each element of the new matrix (mn) is the result of a dot product of Matrix A's row (m) and Matrix B's column (n)
            float m00, m01, m02;
            float m10, m11, m12;
            float m20, m21, m22;
            // First Row
            m00 = (a.m00 * b.m00) + (a.m10 * b.m01) + (a.m20 * b.m02);
            m01 = (a.m01 * b.m00) + (a.m11 * b.m01) + (a.m21 * b.m02);
            m02 = (a.m02 * b.m00) + (a.m12 * b.m01) + (a.m22 * b.m02);
            // Second Row
            m10 = (a.m00 * b.m10) + (a.m10 * b.m11) + (a.m20 * b.m12);
            m11 = (a.m01 * b.m10) + (a.m11 * b.m11) + (a.m21 * b.m12);
            m12 = (a.m02 * b.m10) + (a.m12 * b.m11) + (a.m22 * b.m12);
            // Third Row
            m20 = (a.m00 * b.m20) + (a.m10 * b.m21) + (a.m20 * b.m22);
            m21 = (a.m01 * b.m20) + (a.m11 * b.m21) + (a.m21 * b.m22);
            m22 = (a.m02 * b.m20) + (a.m12 * b.m21) + (a.m22 * b.m22);

            return new Matrix3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
        }
    }
}