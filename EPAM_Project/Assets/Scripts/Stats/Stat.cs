using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class Stat<T> where T : IComparable<T>
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
                if (value.CompareTo(maxValue) >= 0)
                {
                    this.value = maxValue;
                    ValueChanged?.Invoke(value);
                    MaxValueReached?.Invoke();
                }
                else if (value.CompareTo(minValue) <= 0)
                {
                    this.value = minValue;
                    ValueChanged?.Invoke(value);
                    MinValueReached?.Invoke();
                }
                else
                {
                    this.value = value;
                    ValueChanged?.Invoke(this.value);
                }
            }
        }
    }
}
