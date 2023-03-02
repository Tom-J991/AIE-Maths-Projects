using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathClasses;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        const float DEFAULT_TOLERANCE = 0.0001f;

        static void AreComparable(Vector3 expected, Vector3 actual, float tolerance = DEFAULT_TOLERANCE)
        {
            Assert.AreEqual(expected.x, actual.x, tolerance);
            Assert.AreEqual(expected.y, actual.y, tolerance);
            Assert.AreEqual(expected.z, actual.z, tolerance);
        }

        static void AreComparable(Vector4 expected, Vector4 actual, float tolerance = DEFAULT_TOLERANCE)
        {
            Assert.AreEqual(expected.x, actual.x, tolerance);
            Assert.AreEqual(expected.y, actual.y, tolerance);
            Assert.AreEqual(expected.z, actual.z, tolerance);
            Assert.AreEqual(expected.w, actual.w, tolerance);
        }

        static void AreComparable(Matrix3 expected, Matrix3 actual, float tolerance = DEFAULT_TOLERANCE)
        {
            Assert.AreEqual(expected.m00, actual.m00, tolerance);
            Assert.AreEqual(expected.m01, actual.m01, tolerance);
            Assert.AreEqual(expected.m02, actual.m02, tolerance);
            Assert.AreEqual(expected.m10, actual.m10, tolerance);
            Assert.AreEqual(expected.m11, actual.m11, tolerance);
            Assert.AreEqual(expected.m12, actual.m12, tolerance);
            Assert.AreEqual(expected.m20, actual.m20, tolerance);
            Assert.AreEqual(expected.m21, actual.m21, tolerance);
            Assert.AreEqual(expected.m22, actual.m22, tolerance);
        }

        static void AreComparable(Matrix4 expected, Matrix4 actual, float tolerance = DEFAULT_TOLERANCE)
        {
            Assert.AreEqual(expected.m00, actual.m00, tolerance);
            Assert.AreEqual(expected.m01, actual.m01, tolerance);
            Assert.AreEqual(expected.m02, actual.m02, tolerance);
            Assert.AreEqual(expected.m03, actual.m03, tolerance);
            Assert.AreEqual(expected.m10, actual.m10, tolerance);
            Assert.AreEqual(expected.m11, actual.m11, tolerance);
            Assert.AreEqual(expected.m12, actual.m12, tolerance);
            Assert.AreEqual(expected.m13, actual.m13, tolerance);
            Assert.AreEqual(expected.m20, actual.m20, tolerance);
            Assert.AreEqual(expected.m21, actual.m21, tolerance);
            Assert.AreEqual(expected.m22, actual.m22, tolerance);
            Assert.AreEqual(expected.m23, actual.m23, tolerance);
            Assert.AreEqual(expected.m30, actual.m30, tolerance);
            Assert.AreEqual(expected.m31, actual.m31, tolerance);
            Assert.AreEqual(expected.m32, actual.m32, tolerance);
            Assert.AreEqual(expected.m33, actual.m33, tolerance);
        }

        static void AreEqual(Vector3 expected, Vector3 actual)
        {
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
        }
        static void AreEqual(Vector4 expected, Vector4 actual)
        {
            Assert.AreEqual(expected.x, actual.x);
            Assert.AreEqual(expected.y, actual.y);
            Assert.AreEqual(expected.z, actual.z);
            Assert.AreEqual(expected.w, actual.w);
        }
        static void AreEqual(Matrix3 expected, Matrix3 actual)
        {
            Assert.AreEqual(expected.m00, actual.m00);
            Assert.AreEqual(expected.m01, actual.m01);
            Assert.AreEqual(expected.m02, actual.m02);
            Assert.AreEqual(expected.m10, actual.m10);
            Assert.AreEqual(expected.m11, actual.m11);
            Assert.AreEqual(expected.m12, actual.m12);
            Assert.AreEqual(expected.m20, actual.m20);
            Assert.AreEqual(expected.m21, actual.m21);
            Assert.AreEqual(expected.m22, actual.m22);
        }
        static void AreEqual(Matrix4 expected, Matrix4 actual)
        {
            Assert.AreEqual(expected.m00, actual.m00);
            Assert.AreEqual(expected.m01, actual.m01);
            Assert.AreEqual(expected.m02, actual.m02);
            Assert.AreEqual(expected.m03, actual.m03);
            Assert.AreEqual(expected.m10, actual.m10);
            Assert.AreEqual(expected.m11, actual.m11);
            Assert.AreEqual(expected.m12, actual.m12);
            Assert.AreEqual(expected.m13, actual.m13);
            Assert.AreEqual(expected.m20, actual.m20);
            Assert.AreEqual(expected.m21, actual.m21);
            Assert.AreEqual(expected.m22, actual.m22);
            Assert.AreEqual(expected.m23, actual.m23);
            Assert.AreEqual(expected.m30, actual.m30);
            Assert.AreEqual(expected.m31, actual.m31);
            Assert.AreEqual(expected.m32, actual.m32);
            Assert.AreEqual(expected.m33, actual.m33);
        }

        [TestMethod]
        public void Vector3Construction()
        {
            Vector3 expected = new Vector3();
            expected.x = 3.4f;
            expected.y = 2.3f;
            expected.z = 1.2f;

            Vector3 v3a = new Vector3(expected.x, expected.y, expected.z);

            AreEqual(expected, v3a);
        }

        [TestMethod]
        public void Vector4Construction()
        {
            Vector4 expected = new Vector4();
            expected.x = 3.4f;
            expected.y = 2.3f;
            expected.z = 1.2f;
            expected.w = 0.1f;

            Vector4 v4a = new Vector4(expected.x, expected.y, expected.z, expected.w);

            AreEqual(expected, v4a);
        }

        [TestMethod]
        public void Vector3Addition()
        {
            Vector3 v3a = new Vector3(13.5f, -48.23f, 862);
            Vector3 v3b = new Vector3(5, 3.99f, -12);
            Vector3 v3c = v3a + v3b;

            AreComparable(new Vector3(18.5f, -44.24f, 850), v3c);
        }

        [TestMethod]
        public void Vector4Addition()
        {
            Vector4 v4a = new Vector4(13.5f, -48.23f, 862, 0);
            Vector4 v4b = new Vector4(5, 3.99f, -12, 1);
            Vector4 v4c = v4a + v4b;

            AreComparable(new Vector4(18.5f, -44.24f, 850, 1), v4c);
        }

        [TestMethod]
        public void Vector3Subtraction()
        {
            Vector3 v3a = new Vector3(13.5f, -48.23f, 862);
            Vector3 v3b = new Vector3(5, 3.99f, -12);
            Vector3 v3c = v3a - v3b;

            AreComparable(new Vector3(8.5f, -52.22f, 874), v3c);
        }

        [TestMethod]
        public void Vector4Subtraction()
        {
            Vector4 v4a = new Vector4(13.5f, -48.23f, 862, 0);
            Vector4 v4b = new Vector4(5, 3.99f, -12, 1);
            Vector4 v4c = v4a - v4b;

            AreComparable(new Vector4(8.5f, -52.22f, 874, -1), v4c);
        }

        [TestMethod]
        public void Vector3PostScale()
        {
            Vector3 v3a = new Vector3(13.5f, -48.23f, 862);
            Vector3 v3c = v3a * 0.256f;

            AreComparable(new Vector3(3.45600008965f, -12.3468809128f, 220.672012329f), v3c);
        }

        [TestMethod]
        public void Vector4PostScale()
        {
            Vector4 v4a = new Vector4(13.5f, -48.23f, 862, 0);
            Vector4 v4c = v4a * 4.89f;

            AreComparable(new Vector4(66.0149993896f, -235.844696045f, 4215.1796875f, 0), v4c);
        }

        [TestMethod]
        public void Vector3PreScale()
        {
            Vector3 v3a = new Vector3(13.5f, -48.23f, 862);
            Vector3 v3c = 0.256f * v3a;

            AreComparable(new Vector3(3.45600008965f, -12.3468809128f, 220.672012329f), v3c);
        }

        [TestMethod]
        public void Vector4PreScale()
        {
            Vector4 v4a = new Vector4(13.5f, -48.23f, 862, 0);
            Vector4 v4c = 4.89f * v4a;

            AreComparable(new Vector4(66.0149993896f, -235.844696045f, 4215.1796875f, 0), v4c);
        }

        [TestMethod]
        public void Vector3Dot()
        {
            Vector3 v3a = new Vector3(13.5f, -48.23f, 862);
            Vector3 v3b = new Vector3(5, 3.99f, -12);
            float dot3 = v3a.Dot(v3b);

            Assert.AreEqual(-10468.9375f, dot3, DEFAULT_TOLERANCE);
        }

        [TestMethod]
        public void Vector4Dot()
        {
            Vector4 v4a = new Vector4(13.5f, -48.23f, 862, 0);
            Vector4 v4b = new Vector4(5, 3.99f, -12, 1);
            float dot4 = v4a.Dot(v4b);

            Assert.AreEqual(-10468.9375f, dot4, DEFAULT_TOLERANCE);
        }

        [TestMethod]
        public void Vector3Cross()
        {
            Vector3 v3a = new Vector3(13.5f, -48.23f, 862);
            Vector3 v3b = new Vector3(5, 3.99f, -12);
            Vector3 v3c = v3a.Cross(v3b);

            AreComparable(new Vector3(-2860.62011719f, 4472.00000000f, 295.01498413f),
                v3c);
        }

        [TestMethod]
        public void Vector4Cross()
        {
            Vector4 v4a = new Vector4(13.5f, -48.23f, 862, 0);
            Vector4 v4b = new Vector4(5, 3.99f, -12, 1);
            Vector4 v4c = v4a.Cross(v4b);

            AreComparable(v4c, new Vector4(-2860.62011719f, 4472.00000000f, 295.01498413f, 0));
        }

        [TestMethod]
        public void Vector3Magnitude()
        {
            Vector3 v3a = new Vector3(13.5f, -48.23f, 862);
            float mag3 = v3a.Magnitude();

            Assert.AreEqual(863.453735352f, mag3, DEFAULT_TOLERANCE);
        }

        [TestMethod]
        public void Vector4Magnitude()
        {
            Vector4 v4a = new Vector4(13.5f, -48.23f, 862, 0);
            float mag4 = v4a.Magnitude();

            Assert.AreEqual(863.453735352f, mag4, DEFAULT_TOLERANCE);
        }

        [TestMethod]
        public void Vector3Normalise()
        {
            Vector3 v3a = new Vector3(13.5f, -48.23f, 862);
            v3a.Normalize();

            AreComparable(new Vector3(0.0156349f, -0.0558571f, 0.998316f),
                v3a);
        }

        [TestMethod]
        public void Vector4Normalise()
        {
            Vector4 v4a = new Vector4(243, -48.23f, 862, 0);
            v4a.Normalize();

            AreComparable(new Vector4(0.270935f, -0.0537745f, 0.961094f, 0), v4a);
        }

        [TestMethod]
        public void Matrix3Construction()
        {
            Matrix3 expected = new Matrix3();
            expected.m00 = 11.22f;
            expected.m01 = 22.33f;
            expected.m02 = 33.44f;
            expected.m10 = 44.55f;
            expected.m11 = 55.66f;
            expected.m12 = 66.77f;
            expected.m20 = 77.88f;
            expected.m21 = 88.99f;
            expected.m22 = 99.01f;

            Matrix3 m3a = new Matrix3(expected.m00, expected.m01, expected.m02,
                                      expected.m10, expected.m11, expected.m12,
                                      expected.m20, expected.m21, expected.m22);

            AreEqual(expected, m3a);
        }

        [TestMethod]
        public void Matrix4Construction()
        {
            Matrix4 expected = new Matrix4();
            expected.m00 = 11.22f;
            expected.m01 = 22.33f;
            expected.m02 = 33.44f;
            expected.m03 = 44.55f;
            expected.m10 = 55.66f;
            expected.m11 = 66.77f;
            expected.m12 = 77.88f;
            expected.m13 = 88.99f;
            expected.m20 = 99.01f;
            expected.m21 = 1.11f;
            expected.m22 = 11.22f;
            expected.m23 = 22.33f;
            expected.m30 = 33.44f;
            expected.m31 = 44.55f;
            expected.m32 = 55.66f;
            expected.m33 = 66.77f;

            Matrix4 m3a = new Matrix4(expected.m00, expected.m01, expected.m02, expected.m03,
                                      expected.m10, expected.m11, expected.m12, expected.m13,
                                      expected.m20, expected.m21, expected.m22, expected.m23,
                                      expected.m30, expected.m31, expected.m32, expected.m33);

            AreEqual(expected, m3a);
        }

        [TestMethod]
        public void Matrix3SetRotateX()
        {
            Matrix3 m3a = new Matrix3();
            m3a.SetRotateX(3.98f);

            AreComparable(new Matrix3(1, 0, 0, 0, -0.668648f, -0.743579f, 0, 0.743579f, -0.668648f),
                m3a);
        }

        [TestMethod]
        public void Matrix4SetRotateX()
        {
            Matrix4 m4a = new Matrix4();
            m4a.SetRotateX(4.5f);

            AreComparable(new Matrix4(1, 0, 0, 0, 0, -0.210796f, -0.97753f, 0, 0, 0.97753f, -0.210796f, 0, 0, 0, 0, 1),
                m4a);
        }

        [TestMethod]
        public void Matrix3SetRotateY()
        {
            Matrix3 m3b = new Matrix3();
            m3b.SetRotateY(1.76f);

            AreComparable(new Matrix3(-0.188077f, 0, -0.982154f, 0, 1, 0, 0.982154f, 0, -0.188077f),
                m3b);
        }

        [TestMethod]
        public void Matrix4SetRotateY()
        {
            Matrix4 m4b = new Matrix4();
            m4b.SetRotateY(-2.6f);

            AreComparable(new Matrix4(-0.856889f, 0, 0.515501f, 0, 0, 1, 0, 0, -0.515501f, 0, -0.856889f, 0, 0, 0, 0, 1),
                m4b);
        }

        [TestMethod]
        public void Matrix3SetRotateZ()
        {
            Matrix3 m3c = new Matrix3();
            m3c.SetRotateZ(9.62f);

            AreComparable(new Matrix3(-0.981005f, -0.193984f, 0, 0.193984f, -0.981005f, 0, 0, 0, 1),
                m3c);
        }

        [TestMethod]
        public void Matrix4SetRotateZ()
        {
            Matrix4 m4c = new Matrix4();
            m4c.SetRotateZ(0.72f);

            AreComparable(new Matrix4(0.751806f, 0.659385f, 0, 0, -0.659385f, 0.751806f, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1),
                m4c);
        }

        [TestMethod]
        public void Vector3MatrixTransform()
        {
            Matrix3 m3b = new Matrix3();
            m3b.SetRotateY(1.76f);

            Vector3 v3a = new Vector3(13.5f, -48.23f, 862);
            Vector3 v3b = m3b * v3a;

            AreComparable(new Vector3(844.077941895f, -48.2299995422f, -175.38130188f),
                v3b);
        }

        [TestMethod]
        public void Vector3MatrixTransform2()
        {
            Matrix3 m3c = new Matrix3();
            m3c.SetRotateZ(9.62f);

            Vector3 v3a = new Vector3(13.5f, -48.23f, 862);
            Vector3 v3c = m3c * v3a;

            AreComparable(new Vector3(-22.5994224548f, 44.6950683594f, 862),
                v3c);
        }

        [TestMethod]
        public void Vector4MatrixTransform()
        {
            Matrix4 m4b = new Matrix4();
            m4b.SetRotateY(-2.6f);

            Vector4 v4a = new Vector4(13.5f, -48.23f, 862, 0);
            Vector4 v4b = m4b * v4a;

            AreComparable(new Vector4(-455.930236816f, -48.2299995422f, -731.678771973f, 0),
                v4b);
        }

        [TestMethod]
        public void Vector4MatrixTransform2()
        {
            Matrix4 m4c = new Matrix4();
            m4c.SetRotateZ(0.72f);

            Vector4 v4a = new Vector4(13.5f, -48.23f, 862, 0);
            Vector4 v4b = m4c * v4a;

            AreComparable(new Vector4(41.951499939f, -27.3578968048f, 862, 0),
                v4b);
        }

        [TestMethod]
        public void Matrix3Multiply()
        {
            Matrix3 m3a = new Matrix3(1, 2, 3, 4, 5, 4, 3, 2, 1);
            Matrix3 m3b = new Matrix3(5, 4, 3, 2, 1, 2, 3, 4, 5);

            Matrix3 m3c = m3a * m3b;

            AreComparable(new Matrix3(30, 36, 34, 12, 13, 12, 34, 36, 30),
                m3c);
        }

        // the two translations should add up when concatenated
        [TestMethod]
        public void Matrix3MultiplyTranslations()
        {
            Matrix3 m3a = new Matrix3(1);
            m3a.SetTranslation(10, -10);

            Matrix3 m3b = new Matrix3(1);
            m3b.SetTranslation(2, 2);

            Matrix3 m3c = new Matrix3(1);
            m3c.SetTranslation(10 + 2, -10 + 2);

            Matrix3 m3d = m3a * m3b;

            AreComparable(m3d, m3c);
        }

        // rotating by 1 radian, then 2 radians should be equivalent to rotating by 3 radians!
        [TestMethod]
        public void Matrix3MultiplyZRotations()
        {
            Matrix3 m3a = new Matrix3(1);
            m3a.SetRotateZ(1);

            Matrix3 m3b = new Matrix3();
            m3b.SetRotateZ(2);

            Matrix3 m3c = new Matrix3();
            m3c.SetRotateZ(3);

            // order doesn't matter here, for adding two translations
            Matrix3 m3d = m3a * m3b;

            AreComparable(m3d, m3c);
        }

        // rotating by 90 degrees then moving 1m along local x is equivalent to
        // moving 1m along y then rotating by 90 degrees
        [TestMethod]
        public void Matrix3MultiplyRotationTranslation()
        {
            Matrix3 rotateBy90 = new Matrix3(1);
            rotateBy90.SetRotateZ((float)Math.PI / 2.0f);

            Matrix3 translateX = new Matrix3();
            translateX.SetTranslation(1, 0);

            Matrix3 translateY = new Matrix3();
            translateY.SetTranslation(0, 1);

            Matrix3 m3c = new Matrix3();
            m3c.SetRotateZ(3);

            // operations happen in order for Matrix multiplication
            // so m1 is rotate by 90 then move along local x
            Matrix3 m1 = rotateBy90 * translateX;
            // and m2 is move along local Y then rotate by 90
            Matrix3 m2 = translateY * rotateBy90;

            AreComparable(m1, m2);
        }

        [TestMethod]
        public void Matrix4Multiply()
        {
            var m4a = new Matrix4(1, 2, 3, 4, 4, 3, 2, 1, 4, 3, 2, 1, 1, 2, 3, 4);
            var m4b = new Matrix4(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            Matrix4 m4c = m4a * m4b;

            AreComparable(new Matrix4(25, 25, 25, 25,
                                      65, 65, 65, 65,
                                      105, 105, 105, 105,
                                      145, 145, 145, 145),
                          m4c);
        }

        [TestMethod]
        public void Vector3MatrixTranslation()
        {
            // homogeneous point translation
            Matrix3 m3b = new Matrix3(1, 0, 0,
                                      0, 1, 0,
                                      55, 44, 1);

            Vector3 v3a = new Vector3(13.5f, -48.23f, 1);

            Vector3 v3b = m3b * v3a;

            AreComparable(new Vector3(68.5f, -4.23f, 1),
                v3b);
        }

        [TestMethod]
        public void Vector3MatrixTranslation2()
        {
            // homogeneous point translation
            Matrix3 m3c = new Matrix3();
            m3c.SetRotateZ(2.2f);
            m3c.m20 = 55; m3c.m21 = 44; m3c.m22 = 1;

            Vector3 v3a = new Vector3(13.5f, -48.23f, 1);

            Vector3 v3c = m3c * v3a;

            AreComparable(new Vector3(86.0490112305f, 83.2981109619f, 1),
                v3c);
        }

        [TestMethod]
        public void Vector4MatrixTranslation()
        {
            // homogeneous point translation
            Matrix4 m4b = new Matrix4(1, 0, 0, 0,
                                      0, 1, 0, 0,
                                      0, 0, 1, 0,
                                      55, 44, 99, 1);

            Vector4 v4a = new Vector4(13.5f, -48.23f, -54, 1);

            Vector4 v4c = m4b * v4a;
            AreComparable(new Vector4(68.5f, -4.23f, 45, 1),
                v4c);
        }

        [TestMethod]
        public void Vector4MatrixTranslation2()
        {
            // homogeneous point translation
            Matrix4 m4c = new Matrix4();
            m4c.SetRotateZ(2.2f);
            m4c.m30 = 55; m4c.m31 = 44; m4c.m32 = 99; m4c.m33 = 1;

            Vector4 v4a = new Vector4(13.5f, -48.23f, -54, 1);

            Vector4 v4c = m4c * v4a;
            AreComparable(new Vector4(86.0490112305f, 83.2981109619f, 45, 1),
                v4c);
        }

        [TestMethod]
        public void Vector3MatrixTranslation3()
        {
            // homogeneous point translation
            Matrix3 m3b = new Matrix3(1, 0, 0,
                                      0, 1, 0,
                                      55, 44, 1);

            Vector3 v3a = new Vector3(13.5f, -48.23f, 0);

            Vector3 v3b = m3b * v3a;

            AreComparable(new Vector3(13.5f, -48.23f, 0),
                v3b);
        }

        [TestMethod]
        public void Vector3MatrixTranslation4()
        {
            // homogeneous point translation
            Matrix3 m3c = new Matrix3();
            m3c.SetRotateZ(2.2f);
            m3c.m20 = 55; m3c.m21 = 44; m3c.m22 = 1;

            Vector3 v3a = new Vector3(13.5f, -48.23f, 0);

            Vector3 v3c = m3c * v3a;

            AreComparable(new Vector3(31.0490131378f, 39.2981109619f, 0),
                v3c);
        }

        [TestMethod]
        public void Vector4MatrixTranslation3()
        {
            // homogeneous point translation
            Matrix4 m4b = new Matrix4(1, 0, 0, 0,
                                      0, 1, 0, 0,
                                      0, 0, 1, 0,
                                      55, 44, 99, 1);

            Vector4 v4a = new Vector4(13.5f, -48.23f, -54, 0);

            Vector4 v4c = m4b * v4a;
            AreComparable(new Vector4(13.5f, -48.23f, -54, 0),
                v4c);
        }

        [TestMethod]
        public void Vector4MatrixTranslation4()
        {
            // homogeneous point translation
            Matrix4 m4c = new Matrix4();
            m4c.SetRotateZ(2.2f);
            m4c.m30 = 55; m4c.m31 = 44; m4c.m32 = 99; m4c.m33 = 1;

            Vector4 v4a = new Vector4(13.5f, -48.23f, -54, 0);

            Vector4 v4c = m4c * v4a;
            AreComparable(new Vector4(31.0490131378f, 39.2981109619f, -54, 0),
                v4c);
        }

        [TestMethod]
        public void ColourConstructor()
        {
            // homogeneous point translation
            Colour c = new Colour(0x12, 0x34, 0x56, 0x78);

            Assert.AreEqual<UInt32>(0x12345678, c.colour);
        }

        [TestMethod]
        public void ColourGetRed()
        {
            // homogeneous point translation
            Colour c = new Colour(0x12, 0x34, 0x56, 0x78);

            Assert.AreEqual<byte>(0x12, c.red);
        }

        [TestMethod]
        public void ColourGetGreen()
        {
            // homogeneous point translation
            Colour c = new Colour(0x12, 0x34, 0x56, 0x78);

            Assert.AreEqual<byte>(0x34, c.green);
        }


        [TestMethod]
        public void ColourGetBlue()
        {
            // homogeneous point translation
            Colour c = new Colour(0x12, 0x34, 0x56, 0x78);

            Assert.AreEqual<byte>(0x56, c.blue);
        }

        [TestMethod]
        public void ColourGetAlpha()
        {
            // homogeneous point translation
            Colour c = new Colour(0x12, 0x34, 0x56, 0x78);

            Assert.AreEqual<byte>(0x78, c.alpha);
        }

        [TestMethod]
        public void ColourSetRed()
        {
            // homogeneous point translation
            Colour c = new Colour();
            c.red = 0x12;

            Assert.AreEqual<UInt32>(0x12000000, c.colour);
        }

        [TestMethod]
        public void ColourSetGreen()
        {
            // homogeneous point translation
            Colour c = new Colour();
            c.green = 0x34;

            Assert.AreEqual<UInt32>(0x00340000, c.colour);
        }

        [TestMethod]
        public void ColourSetBlue()
        {
            // homogeneous point translation
            Colour c = new Colour();
            c.blue = 0x56;

            Assert.AreEqual<UInt32>(0x00005600, c.colour);
        }

        [TestMethod]
        public void ColourSetAlpha()
        {
            // homogeneous point translation
            Colour c = new Colour();
            c.alpha = 0x78;

            Assert.AreEqual<UInt32>(0x00000078, c.colour);
        }

        // After you have created your Colour class and implemented all the functions listed in the class definition above,
        // add at least 1 new unit test to the unit test program using your answers in this exercise to verify your code.
        [TestMethod]
        public void ColourRedTest()
        {
            byte red = 0b01011110; // 94
            Colour c1 = new Colour();
            c1.red = red;

            Assert.AreEqual<byte>(0b01011110, c1.red);
        }

        [TestMethod]
        public void ColourTest()
        {
            byte red = 0b01011110; // 94
            Colour c1 = new Colour();
            c1.red = red;

            Assert.AreEqual<UInt32>(0b01011110000000000000000000000000, c1.colour);
        }

        [TestMethod]
        public void ColourShiftTest()
        {
            byte red = 0b01011110; // 94
            Colour c1 = new Colour();
            c1.red = red;
            c1.colour = c1.colour >> 8;

            Assert.AreEqual<UInt32>(0b010111100000000000000000, c1.colour);
        }
    }
}
