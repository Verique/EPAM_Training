using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            this.value = value;
            this.maxValue = maxValue;
            this.minValue = minValue;
        }

        public Stat() { }

        public void Copy(Stat<T> from)
        {
            value = from.value;
            minValue = from.minValue;
            maxValue = from.maxValue;
        }

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
