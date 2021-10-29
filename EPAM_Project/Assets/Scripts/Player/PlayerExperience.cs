using Stats;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerStatLoader))]
    public class PlayerExperience : MonoBehaviour
    {
        private PlayerStats stats;
        
        private void Start()
        {
            stats = GetComponent<PlayerStatLoader>().Stats;
            stats.Experience.MaxValueReached += LevelUp;
        }

        public void GetExperience(int exp)
        {
            stats.Experience.Value += exp;
        }

        private void LevelUp()
        {
            stats.Level.Value++;
            stats.Speed.Value += 5;
            stats.Experience.Value -= stats.Experience.maxValue;
        }
    }
}