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
        if (!_animator.ParameterExists(name)) return;

        _animator.SetFloat(name, value);
    }

    public void SetAnimatorBoolParameter(string name, bool value)
    {
        if (!_animator.ParameterExists(name)) return;

        _animator.SetBool(name, value);
    }
}
