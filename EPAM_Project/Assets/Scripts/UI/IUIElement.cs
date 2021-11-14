using Services;
using UnityEngine;

namespace UI
{
    public abstract class UIElement : MonoBehaviour
    {
        public abstract void Init(UIManager manager);
    }
}