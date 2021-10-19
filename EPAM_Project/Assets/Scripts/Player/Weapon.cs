using System.Collections;
using Services;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Weapon : MonoBehaviour
    {
        public UnityAction<int> BulletCountChanged;
        public UnityAction<bool> Reloading;

        [SerializeField] [Range(0.001f, 1f)] private float rateOfFire;
        [SerializeField] [Range(1f, 10f)] private float reloadTime;
        [SerializeField] [Min(1)] private int clipSize = 10;
        private int currentClip;
        private InputManager inputManager;
        private Transform wTransform;
        private float fireCooldown;
        private ObjectPool pool;

        private int CurrentClip
        {
            get => currentClip;
            set
            {
                currentClip = value;
                BulletCountChanged?.Invoke(value);
            }
        }

        public int ClipSize => clipSize;

        private enum State
        {
            NotFiring,
            Firing,
            NeedReload
        }

        private State state;

        private void Start()
        {
            inputManager = ServiceLocator.Instance.Get<InputManager>();
            inputManager.ReloadKeyUp += Reload;
            inputManager.LmbHold += FireBullet;
            wTransform = transform;
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
                        pool.Spawn("bullet", wTransform.position, wTransform.rotation);
                        CurrentClip--;
                        yield return new WaitForSecondsRealtime(rateOfFire);
                        
                        if (state == State.Firing)
                            state = State.NotFiring;
                        
                        break;
                    case State.NeedReload:
                        Reloading?.Invoke(true);
                        yield return new WaitForSecondsRealtime(reloadTime);
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
            if (state == State.NotFiring && CurrentClip > 0)
                state = State.Firing;

            if (CurrentClip == 0)
                state = State.NeedReload;
        }
    }
}