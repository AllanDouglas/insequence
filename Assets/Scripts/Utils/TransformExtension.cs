using UnityEngine;
using DG.Tweening;
public static class TransformExtension
{

    public static void MoveTo(this Transform transform, Vector2 endPosition, float time)
    {
        Vector3[] path = new Vector3[2];

        path[0] = transform.position;
        path[0].x = endPosition.x;

        path[1] = endPosition;

        transform.DOPath(path, time);
    }

    public static void ScaleTo(this Transform transform, Vector2 newSize, float time)
    {
        transform.DOScale(newSize, time);
    }

}
