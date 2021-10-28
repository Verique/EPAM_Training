using System;
using System.Collections.Generic;
using Enemy;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace Services
{
    public class ServiceLocator : MonoBehaviour
    {
        public static ServiceLocator Instance;

        public ServiceLocator(Dictionary<string, IService> services)
        {
            this.services = services;
        }

        private void Awake()
        {
            Instance = this;
            services = new Dictionary<string, IService>();
        }

        private Dictionary<string, IService> services;

        private void Add<T>(T service) where T: IService 
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

        public T Get<T>() where T : class, IService
        {
            var key = typeof(T).Name;

            if (services.ContainsKey(key))
            {
                return (T) services[key];
            }
            else
            {
                var newService = GetComponentInChildren<T>() ?? CreateMono<T>();
                Add(newService);

                return newService;
            }
        }

        private T CreateMono<T>() where T : class, IService
        {
            var gObject = new GameObject(typeof(T).Name);
            gObject.transform.parent = transform;
            var component = gObject.AddComponent(typeof(T));
            return component as T;
        }
    }
}
