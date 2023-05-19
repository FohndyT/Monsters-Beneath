//Jeremy Legault

using System.Collections;
using UnityEngine;

public class CurveTraveler : MonoBehaviour
{
    public Curve curve;
    DevTools devtools;
    #region Attributes
    public float duration;
    int currentCurve;
    public float progress { get; set; }
    bool goingFwd = true;
    [SerializeField] bool lookFwd;
    public enum Behaviour { Once, OnceAndBack, Loop, PingPong }
    public Behaviour mode;
    #endregion

    private void Awake()
    { devtools = GameObject.Find("Player").GetComponent<DevTools>(); }
    void OnDisable()
    { devtools.RefreshCurveArray(); }
    IEnumerator ChangeProgressValueNextFrame(float value)
    {   // pour éviter le jitter de dernière frame avant disable
        yield return null;
        progress = value;
        StopCoroutine(ChangeProgressValueNextFrame(0));
    }
    private void Update()
    {
        if (curve != null)
        {
            if (goingFwd)
            {
                progress += (Time.deltaTime * curve.nbCurves) / duration;
                if (progress > 1f)
                {
                    ++currentCurve;
                    if (mode != Behaviour.Once)
                        progress -= 1f;
                    if (currentCurve >= curve.nbCurves)
                    {
                        switch (mode)
                        {
                            case Behaviour.Once:
                                currentCurve = 0;
                                enabled = false;
                                StartCoroutine(ChangeProgressValueNextFrame(0f));
                                break;
                            case Behaviour.Loop:
                                currentCurve = 0;
                                progress = 0f;
                                break;
                            default:
                                --currentCurve;
                                progress = 1f - progress;
                                goingFwd = false;
                                break;
                        }
                    }
                    else
                        curve.CalculateCoefficients(currentCurve);
                }
            }
            else if (!goingFwd)
            {
                progress -= (Time.deltaTime * curve.nbCurves) / duration;
                if (progress < 0f)
                {
                    --currentCurve;
                    progress += 1f;
                    if (currentCurve <= -1)
                    {
                        currentCurve = 0;
                        progress = 1f - progress;
                        goingFwd = true;
                        if (mode == Behaviour.OnceAndBack) { enabled = false; }
                    }
                    else
                        curve.CalculateCoefficients(currentCurve);
                }
            }
            if (devtools.debugStates[3])
                curve.CalculateCoefficients(currentCurve);
            Vector3 pos = curve.curveType == Curve.CurveType.LinearSpline ? curve.GetPoint(progress) + curve.points[currentCurve] :
                                                                            curve.GetPoint(progress);
            transform.position = pos;
            if (lookFwd)
            {
                Vector3 cible = goingFwd ? pos + curve.transform.InverseTransformPoint(curve.GetVelocity(progress)) :
                                           pos - curve.transform.InverseTransformPoint(curve.GetVelocity(progress));
                transform.LookAt(cible);
            }
        }
    }
}