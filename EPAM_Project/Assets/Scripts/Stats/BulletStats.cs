﻿using System;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewBaseBulletStats", menuName = "Scriptable/Stats/BulletStats")]
    [Serializable]
    public class BulletStats : ScriptableObject, IHasHealthStat
    {
        [field: SerializeField] public Stat<float> Speed { get; private set; } = new Stat<float>();
        [field: SerializeField] public Stat<int> Health { get; private set; } = new Stat<int>();
    }
}