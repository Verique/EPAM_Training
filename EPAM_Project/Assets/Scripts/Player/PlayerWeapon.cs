using System;
using System.Collections;
using SaveData;
using Services;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerWeapon : MonoBehaviour,ISaveable<int>
    {
        private enum State
        {
            NotFiring,
            Firing,
            NeedReload
        }

        public event Action<int> BulletCountChanged;
        public event Action<bool> Reloading;

        [SerializeField] [Range(0.001f, 1f)] private float rateOfFire;
        [SerializeField] [Range(1f, 10f)] private float reloadTime;
        [SerializeField] [Min(1)] private int clipSize = 10;

        private int currentClip;
        private Transform player;
        private InputManager inputManager;
        private ObjectPool pool;
        private State state;

        public int CurrentClip
        {
            get => currentClip;
            set
            {
                currentClip = value;
                BulletCountChanged?.Invoke(value);
            }
        }

        public int ClipSize => clipSize;

        private void Start()
        {
            player = transform;
            inputManager = ServiceLocator.Instance.Get<InputManager>();
            inputManager.ReloadKeyUp += Reload;
            inputManager.LmbHold += FireBullet;
            CurrentClip = clipSize;
            state = State.NotFiring;
            pool = ServiceLocator.Instance.Get<ObjectPool>();
            StartCoroutine(nameof(CantFireCoolDown));
        }

        private IEnumerator CantFireCoolDown()
        {
            while (true)
                switch (state)
                {
                    case State.Firing:
                        pool.Spawn("bullet", player.position, player.rotation);
                        CurrentClip--;
                        yield return new WaitForSeconds(rateOfFire);
                        if (state == State.Firing) state = State.NotFiring;
                        break;
                    
                    case State.NeedReload:
                        Reloading?.Invoke(true);
                        yield return new WaitForSeconds(reloadTime);
                        CurrentClip = clipSize;
                        Reloading?.Invoke(false);
                        state = State.NotFiring;
                        break;
                    
                    case State.NotFiring:
                        yield return new WaitForEndOfFrame();
                        break;

                    default:
                        yield return new WaitForEndOfFrame();
                        break;
                }
        }

        private void Reload()
        {
            state = State.NeedReload;
        }

        private void FireBullet()
        {
            if (state == State.NotFiring && CurrentClip > 0) state = State.Firing;
            if (CurrentClip == 0) state = State.NeedReload;
        }

        public int GetSaveData()
        {
            return currentClip;
        }

        public void LoadData(int data)
        {
            CurrentClip = data;
        }
    }
}