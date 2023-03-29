using System.Transactions;
using UnityEngine;

public class CurveTraveler : MonoBehaviour
{
    public Curve curve;     // public pour EnemyAi
    DevTools devtools;
    #region Attributes
    [SerializeField] float duration;
    int currentCurve;
    float progress;
    bool goingFwd = true;
    [SerializeField] bool lookFwd;
    public enum Behaviour { Once, OnceAndBack, Loop, PingPong }
    public Behaviour mode;
    #endregion

    private void Awake()
    { devtools = GameObject.Find("Player").GetComponent<DevTools>(); }
    private void Update()
    {
        if (goingFwd)
        {
            progress += (Time.deltaTime * curve.nbCurves) / duration;
            if (progress > 1f)
            {
                ++currentCurve;
                progress -= 1f;
                if (currentCurve >= curve.nbCurves)
                {
                    switch (mode)
                    {
                        case Behaviour.Once:
                            currentCurve = 0;
                            enabled = false;
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
        Vector3 pos = curve.GetPoint(progress);
        transform.position = pos;
        if (lookFwd)
        {
            transform.LookAt(pos + curve.GetVelocity(progress));        // si ternaire ici
            if (!goingFwd)
                transform.LookAt(pos - curve.GetVelocity(progress));
        }
    }
}