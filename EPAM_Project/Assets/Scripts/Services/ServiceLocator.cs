using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class ServiceLocator
    {
        private ServiceLocator() { }
        public static ServiceLocator Instance { get; } = new ServiceLocator();

        private Dictionary<string, IService> services = new Dictionary<string, IService>();

        public void Add<T>(T service) where T : IService
        {
            var key = typeof(T).Name;
            
            if (services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
                return;
            }

            services.Add(key, service);
        }
        
        public T Get<T>() where T : IService
        {
            var key = typeof(T).Name;
            
            if (services.ContainsKey(key)) 
                return (T) services[key];
            
            Debug.LogError($"{key} not registered with {GetType().Name}");
            throw new InvalidOperationException();
        } 
    }
}
