using System;
using Extensions;
using Stats;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public class EnemyData
    {
        public string poolTag;
        public SerializableVector3 position;
        public EnemyStats stats;

        public EnemyData(SerializableVector3 position, EnemyStats stats, string poolTag)
        {
            this.position = position;
            this.stats = stats;
            this.poolTag = poolTag;
        }

        public EnemyData()
        {
            stats = ScriptableObject.CreateInstance<EnemyStats>();
        }
    }
}