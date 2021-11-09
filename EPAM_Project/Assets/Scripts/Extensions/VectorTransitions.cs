using UnityEngine;

namespace Extensions
{
    public static class VectorTransitions
    {
        public static Vector2 ToVector2(this Vector3 vector) => new Vector2(vector.x, vector.z);
        public static Vector3 ToVector3(this Vector2 vector) => new Vector3(vector.x, 0, vector.y);
        public static Quaternion ToRotation(this Vector3 vector) => Quaternion.Euler(0,Mathf.Atan2(vector.x, vector.z) * Mathf.Rad2Deg,0);
    }
}