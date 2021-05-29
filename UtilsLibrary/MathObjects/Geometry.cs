using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilsLibrary.MathObjects
{
    public static class Geometry
    {
        #region METHODS

        public static float GetDistanceToPoint(Vector3 pointA, Vector3 pointB)
        {
            return (float)Math.Abs((pointA - pointB).Length());
        }

        public static Vector3 W2SN(Vector3 _in, Transformation transdata)
        {
            Vector3 _out = new Vector3();
            Vector3 temp = new Vector3();
            temp = _in - transdata.Translation;
            float x = temp.Dot(transdata.Right);
            float y = temp.Dot(transdata.Up);
            float z = temp.Dot(transdata.Forward);

            _out.X = transdata.Viewport.X * (1 + (x / transdata.Proj1.X / z));
            _out.Y = transdata.Viewport.Z * (1 - (y / transdata.Proj2.Z / z));
            _out.Z = z;
            return _out;
        }

        public static Vector3[] Create3DFlatCircle(Vector3 center, float radius, int segments)
        {
            Vector3[] points = new Vector3[segments];
            float angle = 0f;
            for (int i = 0; i < points.Length; i++)
            {
                angle = DegToRad(360f / ((float)segments) * (float)i);

                points[i] = new Vector3(
                    center.X + radius * (float)Math.Cos(angle),
                    center.Y + radius * (float)Math.Sin(angle),
                    center.Z);
            }
            return points;
        }

        public static Vector3[] OffsetVectors(Vector3 offset, params Vector3[] points)
        {
            for (int i = 0; i < points.Length; i++)
                points[i] += offset;
            return points;
        }

        public static Vector3[] CopyVectors(Vector3[] source)
        {
            Vector3[] ret = new Vector3[source.Length];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = new Vector3(source[i].X, source[i].Y, source[i].Z);
            return ret;
        }

        public static Vector2 RotatePoint(Vector2 pointToRotate, Vector2 centerPoint, float angleInDegrees)
        {
            float angleInRadians = (float)(angleInDegrees * (Math.PI / 180f));
            float cosTheta = (float)Math.Cos(angleInRadians);
            float sinTheta = (float)Math.Sin(angleInRadians);
            return new Vector2
            {
                X =
                    (int)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (int)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        private static Vector2 RotatePoint(Vector3 pointToRotate, Vector3 centerPoint, float rotationDeg)
        {
            return RotatePoint(
                new Vector2(pointToRotate.X, pointToRotate.Y),
                new Vector2(centerPoint.X, centerPoint.Y),
                rotationDeg);
        }

        public static bool PointSeesPoint(float degree, float fovDegree)
        {
            degree = (float)Math.Abs(degree);
            return degree >= 90 - fovDegree / 2f && degree <= 90 + fovDegree / 2f;
        }

        public static float DegreeBetweenVectors(Vector2 playerA, Vector2 playerB)
        {
            return (float)(Math.Atan2(playerB.Y - playerA.Y, playerB.X - playerA.X) * 180f / Math.PI);
        }

        public static float DegToRad(float deg) { return (float)(deg * (Math.PI / 180f)); }

        public static float RadToDeg(float deg) { return (float)(deg * (180f / Math.PI)); }

        public static float DotProduct(Vector2 v1, Vector2 v2) { return (v1.X * v2.X) + (v1.Y * v2.Y); }

        public static bool PointInCircle(Vector2 point, Vector2 circleCenter, float radius)
        {
            return Math.Sqrt(((circleCenter.X - point.X) * (circleCenter.X - point.X)) + ((circleCenter.Y - point.Y) * (circleCenter.Y - point.Y))) < radius;
        }

        public static Vector3 ClampAngle(Vector3 qaAng)
        {

            if (qaAng.X > 89.0f && qaAng.X <= 180.0f)
                qaAng.X = 89.0f;

            while (qaAng.X > 180.0f)
                qaAng.X = qaAng.X - 360.0f;

            if (qaAng.X < -89.0f)
                qaAng.X = -89.0f;

            while (qaAng.Y > 180.0f)
                qaAng.Y = qaAng.Y - 360.0f;

            while (qaAng.Y < -180.0f)
                qaAng.Y = qaAng.Y + 360.0f;

            return qaAng;
        }

        public static Vector3 CalcAngle(Vector3 src, Vector3 dst)
        {
            Vector3 ret = new Vector3();
            Vector3 vDelta = src - dst;
            float fHyp = (float)Math.Sqrt((vDelta.X * vDelta.X) + (vDelta.Y * vDelta.Y));

            ret.X = RadToDeg((float)Math.Atan(vDelta.Z / fHyp));
            ret.Y = RadToDeg((float)Math.Atan(vDelta.Y / vDelta.X));

            if (vDelta.X >= 0.0f)
                ret.Y += 180.0f;
            return ret;
        }

        #endregion
    }
}
