using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerData : GenericSingleton<PlayerData>
{
    public Transform Player;
    public float SomeFloat;

    public override void Awake()
    {
        base.Awake();

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // initialize data per scene
        Debug.Log("Scene loaded: " +  scene.name);
        SomeFloat += Random.Range(-500.0f, 2500.0f);
    }

    public void Test() { Debug.Log($"Calling {nameof(Test)} from {nameof(PlayerData)} singleton"); }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }
}
