using System;
using UI.SpawnableUIElement;
using UnityEngine;

namespace Services
{
    public class SpawnableUIManager : MonoBehaviour, IService
    {
        private ObjectPool pool;
        
        public readonly struct UIInfoPrefs
        {
            public readonly Transform Target;
            public readonly Vector2 Offset;
            public readonly float MaxValue;

            public UIInfoPrefs(Transform target, Vector2 offset, float maxValue)
            {
                Target = target;
                Offset = offset;
                MaxValue = maxValue;
            }
        }

        private void Start()
        {
            pool = ServiceLocator.Instance.Get<ObjectPool>();
        }

        public GameObject Link<T>(UIInfoPrefs prefs, string poolTag, out Action<T> action) 
        {
            var go = pool.Spawn(poolTag, Vector3.zero, Quaternion.identity);
            var uiElement = go.GetComponent<SpawnableUIElement>();

            if (uiElement == null) throw new ArgumentException($"GameObject with tag {poolTag} doesn't have a uiElement");

            uiElement.Prefs = prefs;
            action = uiElement.EventHandler;
            return go;
        }
    }
}