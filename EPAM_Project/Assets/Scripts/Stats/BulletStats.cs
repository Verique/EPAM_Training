using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewBaseBulletStats", menuName = "Scriptable/BulletStats")]
    [Serializable]
    public class BulletStats : ScriptableObject, IHasHealthStat
    {
        [field: SerializeField] public Stat<float> Speed { get; private set; } = new Stat<float>();
        [field: SerializeField] public Stat<int> Health { get; private set; } = new Stat<int>();
    }
}