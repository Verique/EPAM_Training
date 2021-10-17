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
        private Transform wTransform;
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
            StopFiring,
            CanFire,
            NeedReload
        }

        private State state;

        private void Start()
        {
            wTransform = transform;
            CurrentClip = clipSize;
            state = State.CanFire;
            pool = ServiceLocator.Instance.Get<ObjectPool>();
        }

        public void Fire()
        {
            switch (state)
            {
                case State.CanFire:
                    pool.Spawn("bullet", transform.position, wTransform.rotation);
                    StartCoroutine(nameof(WeaponLogic));
                    break;
                case State.NeedReload:
                    StartCoroutine(nameof(Reload));
                    break;
            }
        }

        private IEnumerator WeaponLogic()
        {
            state = State.StopFiring;

            CurrentClip--;

            if (CurrentClip > 0)
            {
                yield return new WaitForSecondsRealtime(rateOfFire);
                state = State.CanFire;
            }
            else
            {
                state = State.NeedReload;
            }
        }

        private IEnumerator Reload()
        {
            state = State.StopFiring;

            Reloading?.Invoke(true);

            yield return new WaitForSecondsRealtime(reloadTime);
            CurrentClip = clipSize;

            Reloading?.Invoke(false);

            state = State.CanFire;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.R)) StartCoroutine(nameof(Reload));
        }
    }
}