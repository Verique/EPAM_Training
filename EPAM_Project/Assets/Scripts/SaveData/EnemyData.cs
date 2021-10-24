using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public class EnemyData
    {
        public SerializableVector3 position;
        public int currentHealth;
    }
}