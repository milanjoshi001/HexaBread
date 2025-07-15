using UnityEngine;

public static class TransformExtensions
{
    /// <summary>
    /// Destroys all the transform's children
    /// </summary>
    /// <param name="transform"></param>
    public static void Clear(this Transform transform)
    {
        while (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            child.SetParent(null);
            Object.DestroyImmediate(child.gameObject);
        }
    }
}
