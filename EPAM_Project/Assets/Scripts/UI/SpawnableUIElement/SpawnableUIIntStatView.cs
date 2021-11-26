using Stats;
using UnityEngine;

namespace UI.SpawnableUIElement
{
    public abstract class SpawnableUIIntStatView: SpawnableUIElement
    {
        public abstract void OnValueChanged(Stat<int> stat);
        public abstract override void OnTargetMoved(Vector3 newPos);
    }
}