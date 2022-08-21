using UnityEngine;

public static class ExtentionMethods
{
    public static void MoveAlongXAxis(this Transform transform, Transform target, float speed)
    {
        bool negativeDirection = transform.position.x - target.position.x > 0;
        transform.MoveAlongXAxis(speed, negativeDirection);
    }

    public static void MoveAlongXAxis(this Transform transform, float speed, bool negativeDirection)
    {
        transform.LookInXDirection(negativeDirection);
        float _xShift = speed * (negativeDirection ? -1 : 1);
        transform.Translate(_xShift, 0, 0);
    }

    public static void LookInXDirection(this Transform transform, bool negativeDirection)
    {
        int direction = negativeDirection ? -1 : 1;

        if (transform.localScale.x * direction > 0)
            return;

        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
