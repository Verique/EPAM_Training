using UnityEngine;

namespace UI.SpawnableUIElement
{
    public abstract class SpawnableUIElement : MonoBehaviour
    {
        public abstract void OnTargetMoved(Vector3 newPos);
    }
}