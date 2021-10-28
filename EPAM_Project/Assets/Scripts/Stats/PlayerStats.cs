using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewBasePlayerStats", menuName = "Scriptable/PlayerStats")]
    [Serializable]
    public class PlayerStats : ScriptableObject, IHasHealthStat
    {
        [field: SerializeField] public Stat<float> Speed { get; private set; } = new Stat<float>();
        [field: SerializeField] public Stat<int> Health { get; private set; } = new Stat<int>();
        [field: SerializeField] public Stat<int> Experience { get; private set; } = new Stat<int>();
        [field: SerializeField] public Stat<int> Level { get; private set; } = new Stat<int>();
        [field: SerializeField] public Stat<int> Clip { get; private set; } = new Stat<int>();
    }
}
