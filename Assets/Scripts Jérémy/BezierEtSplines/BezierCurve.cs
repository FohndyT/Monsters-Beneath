using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Vector3[] points;

    public void Reset()
    { points = new Vector3[] { Vector3.one, new Vector3(2f, 2.5f, 2f), new Vector3(3f, 3f, 3f), new Vector3(4f, 2f, 4f) }; }
    public Vector3 GetPoint(float t)
    {
        return transform.TransformPoint(Bezier.GetPoint(points[0], points[1], points[2], points[3], t));
    }
    public Vector3 GetVelocity(float t)
    {
        return transform.TransformPoint(Bezier.Derivee(points[0], points[1], points[2], points[3], t)) - transform.position;
    }
}

public static class Bezier
{
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float unMoinsT = 1f - t;
        return Mathf.Pow(unMoinsT, 3) * p0 + 3f * Mathf.Pow(unMoinsT, 2) * t * p1 + 3f * unMoinsT * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;
    }

    public static Vector3 Derivee(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float unMoinsT = 1f - t;
        return 3f * Mathf.Pow(unMoinsT, 2) * (p1 - p0) + 6f * unMoinsT * t * (p2 - p1) + 3f * Mathf.Pow(t, 2) * (p3 - p2);
    }
}