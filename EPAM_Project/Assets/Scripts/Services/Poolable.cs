using System;
using UnityEngine;

namespace Services
{
    public class Poolable : MonoBehaviour
    {
        [SerializeField] private string poolTag;
        public string PoolTag => poolTag;
    }
}