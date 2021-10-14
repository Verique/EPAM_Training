using System;
using Player;
using UnityEngine;

namespace Systems
{
    public class PlayerManager : MonoBehaviour, IService
    {
        [SerializeField] private Transform player;
        public Transform Transform => player;
        
        public void Register() 
        {
            Services.Instance.Add(this);
        }
    }
}