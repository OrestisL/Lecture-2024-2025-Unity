using UnityEngine;

public class PlayPauseAnimations : ManagedBehavior
{
    Animator _animator;

    public override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
        BehaviorManager.Instance.OnGameStateChanged.AddListener(isPlaying =>
                                                        _animator.speed = isPlaying ? 1 : 0);
    }
    public override void OnFixedUpdate()
    {

    }

    public override void OnLateUpdate()
    {

    }

    public override void OnUpdate()
    {

    }
}
