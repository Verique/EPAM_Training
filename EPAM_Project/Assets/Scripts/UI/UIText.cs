using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public abstract class UIText<T> : UIElement<T> where T : IUIManager
    {
        protected Text Text;
        public abstract override void Init(T manager);
    }
}