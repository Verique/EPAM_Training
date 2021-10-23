using System;
using Extensions;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public class PlayerData
    {
        public SerializableVector3 Position;
        public SerializableVector3 Rotation;


        public PlayerData(Vector3 position, Vector3 rotation)
        {
            Position = position.ToSerializable();
            Rotation = rotation.ToSerializable();
        }
    }
}