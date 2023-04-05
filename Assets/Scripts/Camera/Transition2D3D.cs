using System.Collections;
using Unity.VisualScripting;
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
    [SerializeField] public Vector3 sideviewUpVector = new(0, 1, 0);
    [SerializeField] bool crossprodDirectionIsLeft;
    Vector3 EndPt2D = new(0, 0, 2);
    Vector3 EndPt3D = new(0, 0, -2);
    float timer;
    const float slideDuration = 1f;
    bool noControlOnChracter;
    #endregion

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
        camScript = cam.GetComponent<CameraBehaviour>();
        camTraveler = cam.GetComponent<CurveTraveler>();
        camPath = GetComponent<Curve>();
        camTraveler.curve = camPath;
        playBody = GameObject.Find("Player").GetComponent<Rigidbody>();

        transPlayer = GameObject.Find("Player").transform;
        camPath.curveType = Curve.CurveType.CatmullromSpline;
        GameObject.Find("Player").GetComponent<DevTools>().RefreshCurveArray();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player") && !noControlOnChracter)
            Transition();
    }
    public void OnAttack()
    {
        if (GetComponent<InteractInput>().canInteract)
            Transition();
    }
    void Transition()
    {
        GameObject.Find("Player").GetComponent<InputsManager>().transitionCam = this;
        camScript.eagleView = !camScript.eagleView;
        camScript.camOffset = camScript.eagleView ? new Vector3(-7, 7, -7) :
                                               crossprodDirectionIsLeft ? Vector3.Cross(sideviewdirection, sideviewUpVector).normalized * 12 + new Vector3(0, 0, 0) :
                                                                          Vector3.Cross(sideviewUpVector, sideviewdirection).normalized * 12 + new Vector3(0, 0, 0);
        camPath.points[0] = transPlayer.position - transform.position;
        camPath.points[1] = camScript.transform.position - transform.position;
        camPath.points[2] = transPlayer.position + camScript.camOffset - transform.position;
        camPath.points[3] = transPlayer.position - transform.position;

        camTraveler.enabled = true;
        Vector3 tempV = camScript.eagleView ? EndPt3D + transform.position : EndPt2D + transform.position;
        GameObject.Find("Player").GetComponent<Rigidbody>().velocity = Vector3.zero;
        StartCoroutine(SlideToPt(tempV));
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
