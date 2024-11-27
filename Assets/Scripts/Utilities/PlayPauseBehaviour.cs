using UnityEngine;

public class PlayPauseBehaviour : MonoBehaviour
{
    public bool IsPlaying = true;

    public virtual void Start() 
    {
        PausePlayManager.Instance.Add(this);
        PausePlayManager.Instance.OnGameStateChanged.AddListener(x => IsPlaying = x);
    }

    public virtual void Update() 
    {
        if (!IsPlaying) return;
    }

    public virtual void FixedUpdate() 
    {
        if (!IsPlaying) return;
    }

    public virtual void LateUpdate() 
    {
        if (!IsPlaying) return;
    }
}
