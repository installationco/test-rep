using UnityEngine;
using System.Collections;

public class MathToolkit
{
    static float Deg2Rad(float angleDegree)
    {
        return (angleDegree * Mathf.PI) / 180.0F;
    }
    public static Vector2 RotateVectorbyAngle(Vector2 vector, float angle)
    {
        return new Vector2(vector.x * Mathf.Cos(Deg2Rad(angle)) - vector.y * Mathf.Sin(Deg2Rad(angle)),
                           vector.y * Mathf.Cos(Deg2Rad(angle)) + vector.x * Mathf.Sin(Deg2Rad(angle)));
    }
    public static Vector2 VectorbyPointAngle(Vector2 point, float angle, float multiplier)
    {
        Vector2 endpoint = new Vector2(point.x + Mathf.Cos(Deg2Rad(angle)) * multiplier, point.y + Mathf.Sin(Deg2Rad(angle)) * multiplier);
        //return endpoint;
        return endpoint;
    }
}
