using System;
using Stats;
using UnityEngine;

namespace Player.Weapons
{
    [RequireComponent(typeof(ShotStatLoader))]
    public abstract class BaseShot : MonoBehaviour
    {
        protected Transform STransform;
        protected ShotStatLoader StatLoader;
        protected ShotStats Stats;
        
        protected virtual void Awake()
        {
            StatLoader = GetComponent<ShotStatLoader>();
            STransform = GetComponent<Transform>();
        }

        protected virtual void OnEnable()
        {
            Stats = StatLoader.Stats;
        }
    }
}