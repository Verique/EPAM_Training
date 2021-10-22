using System;
using Services;
using UnityEditor.UIElements;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public class GameData
    {
        [Serializable]
        public struct SerializableVector3
        {
            public float x, y, z;

            public SerializableVector3(Vector3 vector)
            {
                this.x = vector.x;
                this.y = vector.y;
                this.z = vector.z;
            }

            public Vector3 ToVector3 => new Vector3(x, y, z);
        }
        
        public SerializableVector3 position;
        public GameData()
        {
            position = new SerializableVector3(ServiceLocator.Instance.Get<PlayerManager>().Transform.position);
        }
    }
}