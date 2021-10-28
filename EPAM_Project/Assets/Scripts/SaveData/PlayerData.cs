using System;
using Extensions;
using Stats;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public class PlayerData
    {
        public SerializableVector3 position;
        public SerializableVector3 rotation;
        public PlayerStats stats;

        public PlayerData(SerializableVector3 position, SerializableVector3 rotation, PlayerStats stats)
        {
            this.position = position;
            this.rotation = rotation;
            this.stats = stats;
        }

        public PlayerData()
        {
            stats = ScriptableObject.CreateInstance<PlayerStats>();
        }
    }
}