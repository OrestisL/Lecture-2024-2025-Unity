using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PausePlayManager : GenericSingleton<PausePlayManager>
{
    private List<PlayPauseBehaviour> _behaviours = new();
    public UnityEvent<bool> OnGameStateChanged;
    private bool _isPlaying = true;

    public override void Awake()
    {
        base.Awake();

        SceneManager.sceneLoaded += ResetBehaviourList;
    }

    private void ResetBehaviourList(Scene scene, LoadSceneMode mode)
    {
        _behaviours = new();
        OnGameStateChanged.RemoveAllListeners();
    }

    public void Add<T>(T behaviour) where T : PlayPauseBehaviour
    {
        if (_behaviours.Contains(behaviour)) return;

        _behaviours.Add(behaviour);
    }

    public void PlayPause() 
    {
        _isPlaying = !_isPlaying;

        OnGameStateChanged.Invoke(_isPlaying);
    }
}
