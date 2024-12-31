using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject Menu;
    private GameObject _menu;

    private void Start()
    {
        BehaviorManager.Instance.OnGameStateChanged.AddListener(Toggle);
    }

    private void Toggle(bool toggle)
    {
        if (_menu == null)
        {
            _menu = Instantiate(Menu);
            _menu.transform.parent = GameObject.Find("Canvas").transform;
        }

        RectTransform rect = _menu.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector3.zero;
        rect.offsetMin = Vector3.zero;
        rect.offsetMax = Vector3.zero;

        _menu.SetActive(!toggle);
        _menu.transform.SetAsFirstSibling();
    }
}
