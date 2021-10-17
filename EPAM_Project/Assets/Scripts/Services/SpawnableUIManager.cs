using System;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Services
{
    public class SpawnableUIManager : MonoBehaviour, IService
    {
        private ObjectPool pool;
        private Camera cam;
        
        public struct UIInfoPrefs
        {
            public Transform Target;
            public Vector2 Offset;
            public string Tag;
            public readonly float MaxValue;

            public UIInfoPrefs(Transform target, Vector2 offset, string tag, float maxValue)
            {
                Target = target;
                Offset = offset;
                Tag = tag;
                MaxValue = maxValue;
            }
        }

        private void Start()
        {
            cam = ServiceLocator.Instance.Get<CameraManager>().Cam;
            pool = ServiceLocator.Instance.Get<ObjectPool>();
        }

        public T1 Link<T1, T2>(UIInfoPrefs prefs, out Action<T2> action) where T1: SpawnableUIElement
        {
            var go = pool.Spawn(prefs.Tag, Vector3.zero, Quaternion.identity);
            var uiElement = go.GetComponent<T1>();
            uiElement.Prefs = prefs;
            action = uiElement.EventHandler;
            return uiElement;
        }
    }
}