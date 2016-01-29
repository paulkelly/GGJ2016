using UnityEngine;

namespace Billygoat.Extensions
{
    public static class Vector2Extensions
    {
        public static bool CompareTo(this Vector2 vector2, Vector2 toCompare)
        {
            return CompareTo(vector2, toCompare, float.Epsilon);
        }

        public static bool CompareTo(this Vector2 vector2, Vector2 toCompare, float tolerance)
        {
            var xAreEqual = Mathf.Abs(vector2.x - toCompare.x) < tolerance;
            var yAreEqual = Mathf.Abs(vector2.y - toCompare.y) < tolerance;
            return xAreEqual && yAreEqual;
        }

        public static float GetAngleFrom(this Vector2 vector2, Vector2 fromVector)
        {
            return fromVector.GetAngleTo(vector2);
        }

        public static float GetAngleTo(this Vector2 vector2, Vector2 toVector)
        {
            var angle = Vector2.Angle(vector2, toVector);
            var cross = Vector3.Cross(vector2, toVector).z;
            angle *= -Mathf.Sign(cross);
            return angle;
        }

        public static Vector2 RotateAroundPoint(this Vector2 point, Vector2 origin, float angle)
        {
            var sinTheta = Mathf.Sin(Mathf.Deg2Rad * angle);
            var cosTheta = Mathf.Cos(Mathf.Deg2Rad * angle);

            point.x -= origin.x;
            point.y -= origin.y;

            var newPoint = Vector2.zero;
            newPoint.x = point.x * cosTheta + point.y * sinTheta + origin.x;
            newPoint.y = -point.x * sinTheta + point.y * cosTheta + origin.y;
            return newPoint;
        }

        public static Vector2 SwapXY(this Vector2 theVector2)
        {
            return new Vector2(theVector2.y, theVector2.x);
        }


        public static string ToExpressiveString(this Vector2 point)
        {
            return string.Format("({0}, {1})", point.x, point.y);
        }
    }
}