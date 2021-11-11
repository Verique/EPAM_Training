﻿using System;
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
                this.value = value;
                ValueChanged?.Invoke(this.value);
                CheckBorders(this.value);
            }
        }

        private void CheckBorders(T valueForCheck)
        {
            if (valueForCheck.CompareTo(maxValue) >= 0)
            {
                MaxValueReached?.Invoke();
            } 
            if (valueForCheck.CompareTo(minValue) <= 0)
            {
                MinValueReached?.Invoke();
            }
        }
    }
}
