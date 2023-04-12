using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Transition2D3D : MonoBehaviour
{
    #region Components
    Camera cam;
    CameraBehaviour camScript;
    Transform transPlayer;
    Rigidbody playBody;
    CurveTraveler camTraveler;
    Curve camPath;
    #endregion
    #region Attributes
    public Vector3 sideviewdirection;
    public Vector3 sideviewUpVector;
    [SerializeField] bool crossprodDirectionIsLeft;
    Vector3 EndPt2D;
    Vector3 EndPt3D;
    Vector3 camVerticalOffset2D;
    float timer;
    float slideDuration = 1f;
    bool noControlOnChracter;
    #endregion

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
        camScript = cam.GetComponent<CameraBehaviour>();
        camTraveler = cam.GetComponent<CurveTraveler>();
        camPath = GetComponent<Curve>();
        playBody = GameObject.Find("Player").GetComponent<Rigidbody>();

        transPlayer = GameObject.Find("Player").transform;
        camPath.curveType = Curve.CurveType.CatmullromSpline;
        GameObject.Find("Player").GetComponent<DevTools>().RefreshCurveArray();
    }
    private void OnTriggerEnter(Collider other)
    {
        EndPt2D = crossprodDirectionIsLeft ? Vector3.Cross(sideviewdirection, sideviewUpVector).normalized * 2 : Vector3.Cross(sideviewUpVector, sideviewdirection).normalized * 2;
        EndPt3D = -EndPt2D;
        camVerticalOffset2D = sideviewUpVector * 2;
        if (other.gameObject.name.Equals("Player") && !noControlOnChracter)
            Transition();
    }
    public void OnAttack()
    { if (GetComponent<InteractInput>().canInteract) { Transition(); } }
    void Transition()
    {
        camTraveler.curve = camPath;
        GameObject.Find("Player").GetComponent<InputsManager>().transitionCam = this;
        camScript.eagleView = !camScript.eagleView;
        camScript.camOffset = camScript.eagleView ? new Vector3(-7, 7, -7) : EndPt3D * 7 + camVerticalOffset2D;

        Vector3 tempV = camScript.eagleView ? EndPt3D : EndPt2D;
        camPath.points[0] = Vector3.zero;
        camPath.points[1] = camScript.transform.position - transform.position;
        camPath.points[2] = tempV + camScript.camOffset;
        camPath.points[3] = Vector3.zero;

        camTraveler.enabled = true;
        GameObject.Find("Player").GetComponent<Rigidbody>().velocity = Vector3.zero;
        StartCoroutine(SmoothCam(tempV));
        StartCoroutine(SlideToPt(tempV + transform.position));
    }
    IEnumerator SmoothCam(Vector3 tempV)
    {
        Vector3 tempPlayerPos = transPlayer.position;
        for (float t = 0; t < camTraveler.duration; t += Time.deltaTime)
        {
            cam.transform.LookAt(transPlayer.position + camVerticalOffset2D);
            yield return null;
        }
        StopCoroutine(SmoothCam(Vector3.zero));
    }
    IEnumerator SlideToPt(Vector3 tempV)
    {
        noControlOnChracter = true;
        playBody.useGravity = false;
        Vector3 posIni = transPlayer.position;
        while (timer < slideDuration)
        {
            transPlayer.position = posIni + (tempV - posIni) * timer / slideDuration;
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0f;
        playBody.useGravity = true;
        noControlOnChracter = false;
        transPlayer.position = tempV;
        StopCoroutine(SlideToPt(Vector3.zero));
    }
}
