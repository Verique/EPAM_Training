using UnityEngine;

namespace Services
{
    public class PlayerManager : MonoBehaviour, IService
    {
        [SerializeField] private Transform player;
        public Transform Transform => player;
    }
}