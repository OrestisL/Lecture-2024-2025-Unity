using UnityEngine;

public class PlayPauseAnimations : PlayPauseBehaviour
{
    Animator _animator;
    public override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();

        PausePlayManager.Instance.OnGameStateChanged.
            AddListener(isPlaying => _animator.speed = isPlaying ? 1 : 0);
    }
}
