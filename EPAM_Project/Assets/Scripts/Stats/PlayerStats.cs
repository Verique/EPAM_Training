using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewBasePlayerStats", menuName = "Scriptable/PlayerStats")]
    [Serializable]
    public class PlayerStats : ScriptableObject, IStats<PlayerStats>, IHasHealthStat
    {
        public Stat<float> Speed => speed;
        [SerializeField] private Stat<float> speed = new Stat<float>();
        
        public Stat<int> Health => health;
        [SerializeField] private Stat<int> health = new Stat<int>();

        public Stat<int> Experience => experience;
        [SerializeField] private Stat<int> experience = new Stat<int>();
        
        public Stat<int> Level => level;
        [SerializeField] private Stat<int> level = new Stat<int>();

        public Stat<int> Clip => clip;
        [SerializeField] private Stat<int> clip = new Stat<int>();

        public void Copy(PlayerStats from)
        {
            health.Copy(from.health);
            speed.Copy(from.speed);
            experience.Copy(from.experience);
            level.Copy(from.level);
            clip.Copy(from.clip);
        }
    }
}
