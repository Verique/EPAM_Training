using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

namespace Stats
{
    [Serializable]
    public class Stat<T> 
    {
        public T minValue;
        public T maxValue;
        [SerializeField] private T value;

        public event Action<T> ValueChanged;
        public event Action MinValueReached;
        public event Action MaxValueReached;

        public Stat(T value, T minValue, T maxValue)
        {
            Value = value;
            this.maxValue = maxValue;
            this.minValue = minValue;
        }

        public Stat() { ValueChanged?.Invoke(value); }

        public T Value
        {
            get => value;
            set
            {
                var comparer = Comparer<T>.Default;
                this.value = value;
                ValueChanged?.Invoke(this.value);
                
                if (comparer.Compare(value, maxValue) >= 0)
                {
                    MaxValueReached?.Invoke();
                } 
                else if (comparer.Compare(value, minValue) <= 0)
                {
                    MinValueReached?.Invoke();
                }
            }
        }
    }
}
