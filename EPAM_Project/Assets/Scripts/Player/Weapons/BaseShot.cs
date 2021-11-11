using Stats;
using UnityEngine;

namespace Player.Weapons
{
    [RequireComponent(typeof(ShotStatLoader))]
    public abstract class BaseShot : MonoBehaviour
    {
        protected Transform STransform;
        protected ShotStats Stats;
        
        public Vector3 Destination { get; set; }

        protected virtual void Awake()
        {
            STransform = GetComponent<Transform>();
        }

        protected virtual void OnEnable()
        {
            Stats = GetComponent<ShotStatLoader>().Stats;
        }
    }
}