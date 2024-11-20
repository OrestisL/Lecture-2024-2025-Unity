using UnityEngine;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public int ToLoadIndex;
    public string SceneName;
    public Button Button;

    private void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(() => Utilities.LoadSceneAsync(ToLoadIndex, this));

        PlayerData.Instance.Test();
    }
}
