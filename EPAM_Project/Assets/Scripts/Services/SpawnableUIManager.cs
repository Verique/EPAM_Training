using Enemy;
using UI.SpawnableUIElement;
using UnityEngine;

namespace Services
{
    public class SpawnableUIManager : MonoBehaviour, IService
    {
        
        private ObjectPool pool;
        
        private void Start()
        {
            pool = ServiceLocator.Instance.Get<ObjectPool>();
        }

        public EnemyHealthBar GetHpBar(EnemyHasHpBar enemy)
        {
            var uiElement = pool.Spawn<EnemyHealthBar>("hbar", Vector3.zero, Quaternion.identity);
            enemy.EnemyMoved += uiElement.OnTargetMoved;
            enemy.HealthChanged += uiElement.OnValueChanged;
            uiElement.Enemy = enemy;
            return uiElement;
        }

        public void ReleaseHpBar(EnemyHealthBar hbar)
        {
            if (hbar == null) return;
            hbar.gameObject.SetActive(false);
            hbar.Enemy.EnemyMoved -= hbar.OnTargetMoved;
            hbar.Enemy.HealthChanged -= hbar.OnValueChanged;
        }
    }
}