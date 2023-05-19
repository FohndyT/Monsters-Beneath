//Jeremy Legault

using System.Collections;
using UnityEngine;

public class Transition2D3D : MonoBehaviour
{
    #region Components
    Camera cam;
    CameraBehaviour camScript;
    CurveTraveler camTraveler;
    Curve camPath;
    GameObject playerObj;
    Transform transPlayer;
    Rigidbody playBody;
    #endregion
    #region Attributes
    public Vector3 sideviewWorldDirection;
    public Vector3 sideviewWorldUpVector;
    [SerializeField] Vector3 EndPt2D;
    [SerializeField] Vector3 EndPt3D;
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
        camPath.curveType = Curve.CurveType.CatmullromSpline;

        playerObj = GameObject.Find("Player");
        transPlayer = playerObj.transform;
        playBody = playerObj.GetComponent<Rigidbody>();
        playerObj.GetComponent<DevTools>().RefreshCurveArray();
    }
    private void OnTriggerEnter(Collider other)
    {
        camVerticalOffset2D = 2 * sideviewWorldUpVector;
        if (other.gameObject.Equals(playerObj) && !noControlOnChracter)
            Transition();
    }
    public void OnAttack()
    { if (GetComponent<InteractInput>().canInteract && !noControlOnChracter) { Transition(); } }
    void Transition()
    {
        camTraveler.curve = camPath;
        playerObj.GetComponent<InputsManager>().transitionCam = this;
        camScript.eagleView = !camScript.eagleView;
        camScript.camOffset = camScript.eagleView ? new Vector3(-7, 7, -7) : 12 * Vector3.Cross(sideviewWorldUpVector, sideviewWorldDirection) + camVerticalOffset2D;

        Vector3 tempV = camScript.eagleView ? EndPt3D * 2 : EndPt2D * 2;
        tempV -= new Vector3(0, 6, 0);
        camPath.points[0] = Vector3.zero;
        camPath.points[1] = camScript.transform.position - transform.position;
        camPath.points[2] = transform.InverseTransformDirection(transform.TransformDirection(tempV) + camScript.camOffset);
        camPath.points[3] = Vector3.zero;

        camTraveler.enabled = true;
        playBody.velocity = Vector3.zero;
        StartCoroutine(SmoothCam());
        StartCoroutine(SlideToPt(transform.TransformDirection(tempV) + transform.position));
    }
    IEnumerator SmoothCam()
    {
        noControlOnChracter = true;
        for (float t = 0; t < camTraveler.duration; t += Time.deltaTime)
        {
            cam.transform.LookAt(transPlayer.position + camVerticalOffset2D);
            yield return null;
        }
        noControlOnChracter = false;
        StopCoroutine(SmoothCam());
    }
    IEnumerator SlideToPt(Vector3 tempV)
    {
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
        transPlayer.position = tempV;
        StopCoroutine(SlideToPt(Vector3.zero));
    }
}
