using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Services
{
    public class ServiceLocator : MonoBehaviour
    {
        public static ServiceLocator Instance;
        
        private void Awake()
        {
            Instance = this;
            services = new Dictionary<string, IService>();
            
            Add(GetComponentInChildren<InputManager>());
            Add(GetComponentInChildren<CameraManager>());
            Add(GetComponentInChildren<PlayerManager>());
            Add(GetComponentInChildren<ObjectPool>());
            Add(GetComponentInChildren<SpawnableUIManager>());
            Add(GetComponentInChildren<EnemyManager>());
            Add(GetComponentInChildren<GameManager>());
        }

        private Dictionary<string, IService> services;

        private void Add<T>(T service) where T: Component, IService 
        {
            var key = typeof(T).Name;

            if (service == null)
            {
                Debug.LogError($"There is no {key} in game root");
                return;
            }
            
            if (services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
                return;
            }
            
            services.Add(key, service); 
        }

        public T Get<T>() where T : Component, IService
        {
            var key = typeof(T).Name;

            if (services.ContainsKey(key)) 
                return (T) services[key];
            
            Debug.LogError($"{key} not registered with {GetType().Name}");
            throw new InvalidOperationException();
        }

        private T CreateComponent<T>() where T : Component, IService
        {
            var gObject = new GameObject(typeof(T).Name);
            gObject.transform.parent = transform;
            var component = gObject.AddComponent<T>();
            return component;
        }
    }
}
