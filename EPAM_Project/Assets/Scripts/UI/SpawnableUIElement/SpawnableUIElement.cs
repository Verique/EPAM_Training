using Services;
using UnityEngine;

namespace UI.SpawnableUIElement
{
    public abstract class SpawnableUIElement : MonoBehaviour
    {
        public SpawnableUIManager.UIInfoPrefs Prefs { get; set; }
        public abstract void EventHandler<T>(T param);
    }
}