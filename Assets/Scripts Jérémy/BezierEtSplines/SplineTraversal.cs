using UnityEngine;

public class SplineTraversal : MonoBehaviour
{
    public BezierCurve curve;
    public float duration;
    float progress;
    bool goingFwd = true;
    public bool lookFwd;
    public enum Behaviour { Once, OnceAndBack, Loop, PingPong }
    public Behaviour mode;

    private void Update()
    {
        if (goingFwd)
        {
            progress += Time.deltaTime / duration;
            if (progress > 1f)
            {
                switch (mode)
                {
                    case Behaviour.Once:
                        enabled = false;
                        break;
                    case Behaviour.Loop:
                        progress = 0f;
                        break;
                    default:
                        progress = 2f - progress;
                        goingFwd = false;
                        break;
                }
            }
        }
        else
        {
            progress -= Time.deltaTime / duration;
            if (progress < 0f)
            {
                progress = 0f - progress;
                goingFwd = true;
                if (mode == Behaviour.OnceAndBack) { enabled = false; }
            }
        }
        Vector3 pos = curve.GetPoint(progress);
        transform.localPosition = pos;
        if (lookFwd) { transform.LookAt(pos + curve.GetVelocity(progress)); }
    }
}