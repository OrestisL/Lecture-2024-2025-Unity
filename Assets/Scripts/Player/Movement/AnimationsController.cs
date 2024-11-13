using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetAnimatorFloatParameter(string name, float value)
    {
        _animator.SetFloat(name, value);
    }
}
