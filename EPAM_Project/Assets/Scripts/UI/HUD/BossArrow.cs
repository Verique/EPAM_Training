using Extensions;
using UnityEngine;

namespace UI.HUD
{
    public class BossArrow : UIElement<HUDManager>
    {
        private Vector2 direction;
        public override void Init(HUDManager manager)
        {
            manager.BossDirectionChanged += OnBossDirectionChanged;
            manager.BossSpawned += (bName) => gameObject.SetActive(true);
            manager.PlayerMovedOnScreen += OnPlayerMovedOnScreen;
            manager.GameEnded += () => gameObject.SetActive(false);
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