using System;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewBaseWeaponStats", menuName = "Scriptable/Stats/WeaponStats")]
    [Serializable]
    public class WeaponStats : ScriptableObject
    {
        [field: SerializeField] public Stat<int> Clip { get; private set; } = new Stat<int>();
        
        [field: SerializeField] public Stat<float> RateOfFire { get; private set; } = new Stat<float>();
        [field: SerializeField] public Stat<float> ReloadTime { get; private set; } = new Stat<float>();
    }
}