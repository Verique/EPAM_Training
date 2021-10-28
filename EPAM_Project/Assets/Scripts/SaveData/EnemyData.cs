using System;
using Extensions;
using Stats;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public class EnemyData
    {
        public SerializableVector3 position;
        public EnemyStats stats;

        public EnemyData(SerializableVector3 position, EnemyStats stats)
        {
            this.position = position;
            this.stats = stats;
        }

        public EnemyData()
        {
            stats = ScriptableObject.CreateInstance<EnemyStats>();
        }
    }
}