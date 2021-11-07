using System;
using Services;
using Stats;
using UnityEngine;

namespace Player.Weapons
{
    [RequireComponent(typeof(WeaponStatLoader))]
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private Sprite icon;
        public Sprite WeaponIcon => icon;

        private float cooldownTime;
        private float lastActionTime;

        private bool isReloading;
        
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

        public void Fire(Vector3 destination)
        {
            if (Time.time - lastActionTime < cooldownTime) return;

            if (isReloading)
            {
                isReloading = false;   
                stats.Clip.Value = stats.Clip.maxValue;
            }
            
            if (stats.Clip.Value > stats.Clip.minValue) FireShot(destination);
            else if (stats.Clip.Value == stats.Clip.minValue) Reload();
        }

        protected virtual void FireShot(Vector3 destination)
        {
            ActionDone(stats.RateOfFire.Value);
            stats.Clip.Value--;
            
            var wTransform = transform;
            pool.SpawnWithSetup<BaseShot>(
                ObjectPoolTag, 
                wTransform.position, 
                wTransform.rotation, 
                baseShot => baseShot.Destination = destination);
        }

        public void Reload() => SetReload(true, stats.ReloadTime.Value);
        public void PrepareToSwitch() => SetReload(false, 0); 
        private void SetReload(bool reloading, float cooldown)
        {
            isReloading = reloading;
            ActionDone(cooldown);
            WeaponReloading?.Invoke(cooldown);
        }

        private void ActionDone(float coolDownTime)
        {
            lastActionTime = Time.time;
            cooldownTime = coolDownTime;
        }
    }
}   