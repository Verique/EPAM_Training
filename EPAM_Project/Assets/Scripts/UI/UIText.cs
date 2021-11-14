using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public abstract class UIText : UIElement
    {
        protected Text Text;
        public abstract override void Init(UIManager manager);
    }
}