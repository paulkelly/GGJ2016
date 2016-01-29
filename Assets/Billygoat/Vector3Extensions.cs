using UnityEngine;

namespace Billygoat.Extensions
{
    public static class Vector3Extensions
    {
        public static bool CompareTo(this Vector3 vector3, Vector3 toCompare)
        {
            return CompareTo(vector3, toCompare, float.Epsilon);
        }

        public static bool CompareTo(this Vector3 vector3, Vector3 toCompare, float tolerance)
        {
            var xAreEqual = Mathf.Abs(vector3.x - toCompare.x) < tolerance;
            var yAreEqual = Mathf.Abs(vector3.y - toCompare.y) < tolerance;
            var zAreEqual = Mathf.Abs(vector3.z - toCompare.z) < tolerance;
            return xAreEqual && yAreEqual && zAreEqual;
        }

        public static Vector2 GetPlaner(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }


        public static Vector3 SetZ(this Vector3 vector3, float z)
        {
            var rawVector3 = vector3;
            rawVector3.z = z;
            return rawVector3;
        }
        public static Vector3 SetY(this Vector3 vector3, float y)
        {
            var rawVector3 = vector3;
            rawVector3.y = y;
            return rawVector3;
        }

        public static Quaternion ToQuaternion(this Vector3 eularAngle)
        {
            return toQuaternion(eularAngle.y, eularAngle.x, eularAngle.z);
        }

        private static Quaternion toQuaternion(float yaw, float pitch, float roll)
        {
            yaw *= Mathf.Deg2Rad;
            pitch *= Mathf.Deg2Rad;
            roll *= Mathf.Deg2Rad;
            var rollOver2 = roll * 0.5f;
            var sinRollOver2 = Mathf.Sin(rollOver2);
            var cosRollOver2 = Mathf.Cos(rollOver2);
            var pitchOver2 = pitch * 0.5f;
            var sinPitchOver2 = Mathf.Sin(pitchOver2);
            var cosPitchOver2 = Mathf.Cos(pitchOver2);
            var yawOver2 = yaw * 0.5f;
            var sinYawOver2 = Mathf.Sin(yawOver2);
            var cosYawOver2 = Mathf.Cos(yawOver2);
            Quaternion result;
            result.w = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            result.x = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.y = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            result.z = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;

            return result;
        }
    }
}