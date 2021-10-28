using SaveData;
using Services;
using UnityEngine;
using UnityEngine.UI;

public class TestLoadButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ServiceLocator.Instance.Get<SaveManager>().Load);
    }
}
