using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BehaviorManager : GenericSingleton<BehaviorManager>
{
    private List<ManagedBehavior> _behaviors = new();
    private bool _isPlaying = true;

    public UnityEvent<bool> OnGameStateChanged = new();

    public int SplitThershold = 10;

    public override void Awake()
    {
        base.Awake();
        StartCoroutine(Updater());
        SceneManager.sceneLoaded += RestOnSceneLoad;
    }

    private void RestOnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        // clear to avoid null refs
        _behaviors = new();
        OnGameStateChanged.RemoveAllListeners();

        // should be playing when changing scene
        _isPlaying = true;
    }

    public void Add(ManagedBehavior behavior)
    {
        if (_behaviors.Contains(behavior))
            return;

        _behaviors.Add(behavior);
    }

    public void Remove(ManagedBehavior behavior)
    {
        _behaviors.Remove(behavior);
    }

    // Update every behacior all at once
    //public void Update()
    //{
    //    if (!_isPlaying) return;
    //
    //    foreach (var behavior in _behaviors) 
    //    {
    //        behavior.OnUpdate();
    //    }
    //}

    public void FixedUpdate()
    {
        if (!_isPlaying) return;

        foreach (var behavior in _behaviors)
        {
            behavior.OnFixedUpdate();
        }
    }

    public void LateUpdate()
    {
        if (!_isPlaying) return;

        foreach (var behavior in _behaviors)
        {
            behavior.OnLateUpdate();
        }
    }

    public void ToggleStatus()
    {
        _isPlaying = !_isPlaying;
        OnGameStateChanged?.Invoke(_isPlaying);
    }

    // Split updates in 2 frames
    private IEnumerator Updater()
    {
        while (true)
        {
            if (!_isPlaying) 
            {
                yield return new WaitForEndOfFrame();
                continue;
            }

            int count = _behaviors.Count;

            if (count < SplitThershold)
            {
                for (int i = 0; i < count; i++)
                {
                    _behaviors[i].OnUpdate();
                }
                yield return new WaitForEndOfFrame();
                continue;
            }

            for (int i = 0; i < SplitThershold; i++)
            {
                _behaviors[i].OnUpdate();
            }
            yield return new WaitForEndOfFrame();

            for (int j = SplitThershold; j < count; j++)
            {
                _behaviors[j].OnUpdate();
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
