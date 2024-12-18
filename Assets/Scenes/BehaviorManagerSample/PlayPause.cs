using UnityEngine;

public class PlayPause : MonoBehaviour
{
    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => BehaviorManager.Instance.ToggleStatus());
    }
}
