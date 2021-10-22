using UnityEngine;

namespace Extensions
{
    public static class SerializableTypesExt
    {
        public static SerializableVector3 ToSerializable(this Vector3 vector)
        {
            return new SerializableVector3(vector);
        }
    }
}