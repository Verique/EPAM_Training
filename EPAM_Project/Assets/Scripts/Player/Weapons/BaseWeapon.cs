using System;
using System.Collections;
using Services;
using Stats;
using UnityEngine;

namespace Player.Weapons
{
    [RequireComponent(typeof(WeaponStatLoader))]
    public abstract class BaseWeapon : MonoBehaviour
    {
        private float cooldownTime;
        private float lastActionTime;
        
        private WeaponStats stats;
        private ObjectPool pool;

        protected abstract string ObjectPoolTag { get; }
        public Stat<int> ClipStat { get; private set; }

        public event Action<float> WeaponReloading;

        protected virtual void Awake()
        {
            pool = ServiceLocator.Instance.Get<ObjectPool>();
            stats = GetComponent<WeaponStatLoader>().Stats;
            ClipStat = stats.Clip;
            cooldownTime = 0;
            lastActionTime = 0;
        }

        public void Fire()
        {
            if (Time.time - lastActionTime < cooldownTime) return;
            
            if (stats.Clip.Value > stats.Clip.minValue) FireShot();
            else if (stats.Clip.Value == stats.Clip.minValue) Reload();
        }

        private IEnumerator ReloadCoroutine(float reloadTime)
        {
            lastActionTime = Time.time;
            cooldownTime = reloadTime;
            WeaponReloading?.Invoke(cooldownTime);
            
            yield return new WaitForSeconds(reloadTime);
            
            stats.Clip.Value = stats.Clip.maxValue;
        }

        protected virtual void FireShot()
        {
            lastActionTime = Time.time;
            cooldownTime = stats.RateOfFire.Value;
            stats.Clip.Value--;
            
            var wTransform = transform;
            pool.Spawn(ObjectPoolTag, wTransform.position, wTransform.rotation);
        }

        public void Reload()
        {
            StartCoroutine(ReloadCoroutine(stats.ReloadTime.Value));
        }
    }
}   