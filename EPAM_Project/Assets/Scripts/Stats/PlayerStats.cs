using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewBasePlayerStats", menuName = "Scriptable/PlayerStats")]
    public class PlayerStats : ScriptableObject, IStats<PlayerStats>, IHasHealthStat
    {
        public Stat<float> Speed => speed;
        [SerializeField] private Stat<float> speed;
        
        public Stat<int> Health => health;
        [SerializeField] private Stat<int> health;

        public Stat<int> Experience => experience;
        [SerializeField] private Stat<int> experience = new Stat<int>(0, 0, 2);
        
        public Stat<int> Level => level;
        [SerializeField] private Stat<int> level = new Stat<int>(0,0,1000);
        

        public PlayerStats Copy()
        {
            var newStats = CreateInstance<PlayerStats>();
            newStats.health = health.Copy();
            newStats.speed = speed.Copy();

            return newStats;
        }
    }
}
