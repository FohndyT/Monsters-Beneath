using UnityEngine;
using UnityEngine.Events;

public class Transition2D3D : MonoBehaviour
{
    CameraBehaviour camScript;
    Curve camPath;
    Transform transPlayer;
    public Vector3 sideviewdirection;
    [SerializeField] public Vector3 sideviewUpVector = new(0, 1, 0);
    [SerializeField] bool crossprodDirectionIsLeft;
    private void Awake()
    {
        camScript = FindObjectOfType<Camera>().GetComponent<CameraBehaviour>();
        camPath = GetComponent<Curve>();
        FindObjectOfType<Camera>().GetComponent<CurveTraveler>().curve = camPath;

        transPlayer = GameObject.Find("Player").transform;
        camPath.curveType = Curve.CurveType.CatmullromSpline;
        GameObject.Find("Player").GetComponent<DevTools>().RefreshCurveArray();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            GameObject.Find("Player").GetComponent<InputsManager>().transitionCam = this;
            camScript.eagleView = !camScript.eagleView;
            camScript.camOffset = camScript.eagleView ? new Vector3(-7, 7, -7) :
                                                   crossprodDirectionIsLeft ? Vector3.Cross(sideviewdirection, sideviewUpVector).normalized * 12 + new Vector3(0, 2, 0) :
                                                                              Vector3.Cross(sideviewUpVector, sideviewdirection).normalized * 12 + new Vector3(0, 2, 0);
            camPath.points[0] = transPlayer.position - transform.position;
            camPath.points[1] = camScript.transform.position - transform.position;
            camPath.points[2] = transPlayer.position + camScript.camOffset - transform.position;
            camPath.points[3] = transPlayer.position - transform.position;
            FindObjectOfType<Camera>().GetComponent<CurveTraveler>().enabled = true;
        }
    }
    public void OnAttack()
    {
        if (GetComponent<InteractInput>().isUsing)
            Invoke("OnTriggerEnter", 0);

    }
}
