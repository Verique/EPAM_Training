using UnityEngine;

namespace UI
{
    public abstract class UIElement<T> : MonoBehaviour where T : IUIManager
    {
        public abstract void Init(T manager);
    }
}