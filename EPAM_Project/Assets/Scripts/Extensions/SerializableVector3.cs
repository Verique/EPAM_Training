using System;
using UnityEngine;

namespace Extensions
{
    [Serializable]
    public struct SerializableVector3
    {
        public float x, y, z;

        public SerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector3() => new Vector3(x, y, z);
    }
}