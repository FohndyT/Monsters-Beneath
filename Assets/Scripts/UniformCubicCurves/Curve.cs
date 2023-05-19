//Jeremy Legault

using System;
using UnityEngine;

//[ExecuteInEditMode]     // Permet a CurveCustomEditor d'acceder a GetPoint(), but also d'afficher splines dans éditeur. Laissez jusqu'à ce qu'on ship le jeu.
public class Curve : MonoBehaviour
{
    DevTools devtools;
    #region Attributes
    public enum CurveType { BezierCurve, LinearSpline, CatmullromSpline, BSpline };
    public CurveType curveType;
    public Vector3[] points = new Vector3[4];
    Vector3[] coefficients = new Vector3[4];
    float[] corrections = { 1f, 1f, 0.5f, 1 / 6f };
    public int nbCurves { get { return points.Length - 3; } }
    public int nbPtsMin { get; } = 4;
    #endregion

    private void Awake()
    {
        for (int i = 0; i < 4; i++) { coefficients[i] = Vector3.zero; }
        CalculateCoefficients(0);
        devtools = GameObject.Find("Player").GetComponent<DevTools>();
    }
    public void Reset()
    {
        curveType = CurveType.BezierCurve;
        for (int i = 0; i < 4; i++)
        {
            points[i] = new Vector3(i, 1, i);
            coefficients[i] = Vector3.zero;
        }
        CalculateCoefficients(0);
    }
    private void OnEnable()
    { devtools?.RefreshCurveArray(); }
    private void Update()
    {
        if (transform.hasChanged)
            CalculateCoefficients(0);
    }

    public void AddPoint(Vector3 newPt)
    {
        Array.Resize(ref points, points.Length + 1);
        points[points.Length - 1] = newPt;
    }
    public void CalculateCoefficients(int u)
    {
        switch (curveType)
        {
            case CurveType.BezierCurve:
                {
                    coefficients[0] = points[u];
                    coefficients[1] = -3f * points[u] + 3f * points[u + 1];
                    coefficients[2] = 3f * points[u] - 6f * points[u + 1] + 3f * points[u + 2];
                    coefficients[3] = -points[u] + 3f * points[u + 1] - 3f * points[u + 2] + points[u + 3];
                    break;
                }
            case CurveType.LinearSpline:
                {
                    coefficients[0] = Vector3.zero;
                    coefficients[1] = points[u + 1] - points[u];
                    coefficients[2] = Vector3.zero;
                    coefficients[3] = Vector3.zero;
                    break;
                }
            case CurveType.CatmullromSpline:
                {
                    coefficients[0] = 2f * points[u + 1];
                    coefficients[1] = -points[u] + points[u + 2];
                    coefficients[2] = 2f * points[u] - 5f * points[u + 1] + 4f * points[u + 2] - points[u + 3];
                    coefficients[3] = -points[u] + 3f * points[u + 1] - 3f * points[u + 2] + points[u + 3];
                    break;
                }
            case CurveType.BSpline:
                {
                    coefficients[0] = points[u] + 4f * points[u + 1] + points[u + 2];
                    coefficients[1] = -3f * points[u] + 3f * points[u + 2];
                    coefficients[2] = 3f * points[u] - 6f * points[u + 1] + 3f * points[u + 2];
                    coefficients[3] = -points[u] + 3f * points[u + 1] - 3f * points[u + 2] + points[u + 3];
                    break;
                }
        }
    }
    public Vector3 GetPoint(float t)
    {
        t = Mathf.Clamp01(t);
        Vector3 pt = (coefficients[0] +
                      coefficients[1] * t +
                      coefficients[2] * Mathf.Pow(t, 2) +
                      coefficients[3] * Mathf.Pow(t, 3)) * corrections[(int)curveType];
        return transform.TransformPoint(pt);
    }
    public Vector3 GetVelocity(float t)
    {
        t = Mathf.Clamp01(t);
        Vector3 pt = (coefficients[1] +
                      coefficients[2] * 2 * t +
                      coefficients[3] * 3 * Mathf.Pow(t, 2)) * corrections[(int)curveType];
        return transform.TransformPoint(pt);
    }
    public Vector3 GetAcceleration(float t)
    {
        t = Mathf.Clamp01(t);
        Vector3 pt = (coefficients[2] * 2 +
                      coefficients[3] * 6 * t) * corrections[(int)curveType];
        return transform.TransformPoint(pt);
    }
}