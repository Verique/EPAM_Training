namespace UI
{
    public abstract class UIMenu<T> : UIElement<T> where T : IUIManager
    {
        public abstract override void Init(T manager);

        protected virtual void Show()
        {
            gameObject.SetActive(true);
        }

        protected virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}