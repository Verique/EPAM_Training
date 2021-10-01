using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public class UIBar : MonoBehaviour
{
    private Vector2 initialBarSize;
    private RectTransform barTransform;

    protected int maxValue;
    protected Image image;

    private void Start()
    {
        SetupBar();
    }

    protected void UpdateBar(int newValue)
    {
        var height = initialBarSize.y * newValue / maxValue;
        barTransform.sizeDelta = new Vector2(initialBarSize.x, height);
    }

    protected virtual void SetupBar()
    {
        image = GetComponent<Image>();
        barTransform = GetComponent<RectTransform>();
        initialBarSize = barTransform.sizeDelta;
    }
}