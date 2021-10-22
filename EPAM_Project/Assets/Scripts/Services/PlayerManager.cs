using System;
using System.Collections;
using Stats;
using UnityEngine;
using UnityEngine.Events;

namespace Services
{
    public class PlayerManager : MonoBehaviour, IService
    {
        [SerializeField] private Transform player;
        public Transform Transform => player;
        
        private void Start()
        {
            InitHealth();
            InitWeapon();
            InitExperience();
        }

        #region Health
        
        private Renderer playerRenderer;
        public Health Health { get; private set; }
        private const int FramesToLerp = 80;
        private const float ColorLerpSpeed = 1 / (float) FramesToLerp;

        private void InitHealth()
        {
            Health = player.GetComponent<Health>();
            Health.DamageTaken += currentHealth => StartCoroutine(nameof(PlayerDamageTakenIndication));
            Health.IsDead += () => player.gameObject.SetActive(false);
            playerRenderer = player.GetComponentInChildren<Renderer>();
        }
        
        private IEnumerator PlayerDamageTakenIndication()
        {
            var frameCount = 0;

            while (frameCount < FramesToLerp)
            {
                playerRenderer.material.color = Color.Lerp(Color.red, Color.white, frameCount * ColorLerpSpeed);
                frameCount++;
                yield return new WaitForEndOfFrame();
            } 
        }
        
        #endregion
        
        #region Weapon
        
        private enum State
        {
            NotFiring,
            Firing,
            NeedReload
        }
        
        public UnityAction<int> BulletCountChanged;
        public UnityAction<bool> Reloading;

        [SerializeField] [Range(0.001f, 1f)] private float rateOfFire;
        [SerializeField] [Range(1f, 10f)] private float reloadTime;
        [SerializeField] [Min(1)] private int clipSize = 10;
        
        private int currentClip;
        private InputManager inputManager;
        private ObjectPool pool;
        private State state;

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

        private void InitWeapon()
        {
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
        
        #endregion

        #region Experience

        private int level;
        private int experience;

        public event Action<int> LevelUp;
        public event Action<int> ExperienceGet;
        
        private const int ToNextLevel = 2;

        private void InitExperience()
        {
            LevelUp += (lvl) => Debug.Log($"Level Up! Current : {lvl} ");
            ExperienceGet += (exp) => Debug.Log($"Got Exp! Current : {exp} ");
        }

        public void GetExperience(int exp)
        {
            experience += exp;

            if (experience < ToNextLevel)
            {
                ExperienceGet?.Invoke(experience);
                return;
            }
            
            level += experience / ToNextLevel;
            LevelUp?.Invoke(level);
            
            experience %= ToNextLevel;
            ExperienceGet?.Invoke(experience);
        }

        #endregion
    }
}