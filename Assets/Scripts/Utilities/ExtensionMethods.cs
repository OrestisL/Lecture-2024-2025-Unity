using UnityEngine;

public static partial class ExtensionMethods
{
    public static void ResetLocal(this Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }

    public static bool ParameterExists(this Animator animator, string name)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name == name)
                return true;
        }

        return false;
    }
}
