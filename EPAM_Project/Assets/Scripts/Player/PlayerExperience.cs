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
            stats.Experience.ValueChanged +=(exp) => Debug.Log($"Got Exp! Current : {exp} ");
            stats.Level.ValueChanged +=(lvl) => Debug.Log($"Level Up! Current : {lvl} ");
        }

        public void GetExperience(int exp)
        {
            stats.Experience.Value += exp;
        }

        private void LevelUp()
        {
            stats.Level.Value++;
            stats.Experience.Value -= stats.Experience.maxValue;
        }
    }
}