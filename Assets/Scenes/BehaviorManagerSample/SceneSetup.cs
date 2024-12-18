using UnityEngine;
using UnityEngine.UI;

public class SceneSetup : MonoBehaviour
{
    public int Count = 20;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Setup);
    }

    private void Setup() 
    {
        for (int i = 0; i < Count; i++)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.AddComponent<SampleBehavior>();
            go.name = $"Cube {i}";
        }
    }
}
