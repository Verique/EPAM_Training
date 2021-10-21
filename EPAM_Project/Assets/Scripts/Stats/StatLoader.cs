using UnityEngine;

namespace Stats
{
    public class StatLoader : MonoBehaviour
    {
        [SerializeField] private StatsScriptable stats;

        private void Start()
        {
            Debug.Log(stats.GetFloat("test"));
        }
    }
}