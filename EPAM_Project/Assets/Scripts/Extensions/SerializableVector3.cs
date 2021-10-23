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

        public static implicit operator Vector3(SerializableVector3 vector) => new Vector3(vector.x, vector.y, vector.z);
    }
}