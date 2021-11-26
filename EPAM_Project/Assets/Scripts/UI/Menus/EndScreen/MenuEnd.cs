using UnityEngine;

namespace UI.Menus.EndScreen
{
    public class MenuEnd : UIMenu<UIManager>
    {
        [SerializeField] private UIElement<UIManager> endGameStatsView;
        
        public override void Init(UIManager manager)
        {
            manager.GameEnded += stats => Show();
            endGameStatsView.Init(manager);
        }
    }
}