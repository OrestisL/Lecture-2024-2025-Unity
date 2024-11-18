using UnityEngine;

public static partial class ExtensionMethods
{
    public static void ResetLocal(this Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }
}
