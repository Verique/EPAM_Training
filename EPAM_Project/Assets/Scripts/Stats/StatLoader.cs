using UnityEngine;

namespace Stats
{
    public class StatLoader : MonoBehaviour
    {
        [SerializeField] private StatsScriptable stats;
        public StatsScriptable Stats => stats;
    }
}