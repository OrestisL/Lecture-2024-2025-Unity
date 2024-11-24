using UnityEngine;
using UnityEngine.Assertions;

public class AnimationsController : MonoBehaviour
{
    private Animator _animator;
    private bool _isInit;
    private void Start()
    {
        if (!_isInit)
            Init();
    }

    public void Init()
    {
        if (_isInit)
            return;

        _isInit = true;
        _animator = GetComponentInChildren<Animator>();

        if (_animator == null)
            _animator = GetComponent<Animator>();

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
