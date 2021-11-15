using Extensions;
using Services;
using UnityEngine;

namespace UI
{
    public class BossArrow : UIElement
    {
        private Vector2 direction;
        public override void Init(UIManager manager)
        {
            manager.BossDirectionChanged += OnBossDirectionChanged;
            manager.BossSpawned += (bName) => gameObject.SetActive(true);
            manager.PlayerMovedOnScreen += OnPlayerMovedOnScreen;
            manager.GameEnded += stats => gameObject.SetActive(false);
        }

        private void OnPlayerMovedOnScreen(Vector2 obj)
        {
            transform.position = obj + direction * 40;
        }

        private void OnBossDirectionChanged(Vector2 obj)
        {
            direction = obj.normalized;
            transform.rotation = direction.ToRotation();
        }
    }
}