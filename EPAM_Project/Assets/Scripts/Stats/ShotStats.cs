using System;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewBaseShotStats", menuName = "Scriptable/Stats/ShotStats")]
    [Serializable]
    public class ShotStats : ScriptableObject
    {
        [field: SerializeField] public Stat<float> Speed { get; private set; } = new Stat<float>();
        [field: SerializeField] public Stat<float> ShotLength { get; private set; } = new Stat<float>();
        [field: SerializeField] public Stat<float> ShotRadius { get; private set; } = new Stat<float>();
        [field: SerializeField] public Stat<int> Damage { get; private set; } = new Stat<int>();
    }
}