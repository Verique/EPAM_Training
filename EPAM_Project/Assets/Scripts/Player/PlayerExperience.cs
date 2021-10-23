using System;
using UnityEngine;

namespace Player
{
    public class PlayerExperience : MonoBehaviour
    {
        private void Start()
        {
            InitExperience();
        }

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
    }
}