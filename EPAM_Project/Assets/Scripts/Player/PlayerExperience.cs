using Stats;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerStatLoader))]
    public class PlayerExperience : MonoBehaviour
    {
        private PlayerStats stats;
        private Stat<int> expStat;
        private void Start()
        {
            stats = GetComponent<PlayerStatLoader>().Stats;
            expStat = stats.Experience;
        }

        public void GetExperience(int exp)
        {
            while (expStat.Value + exp >= expStat.maxValue)
            {
                var gotExp = expStat.maxValue - expStat.Value;
                exp -= gotExp;
                LevelUp();
            }
            
            expStat.Value += exp;
        }

        private void LevelUp()
        {
            expStat.Value = 0;
            stats.Level.Value++;
            stats.Speed.Value += 5;
        }
    }
}