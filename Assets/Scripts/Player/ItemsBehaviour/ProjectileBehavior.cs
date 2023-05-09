using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    GameObject parentObj;
    Curve curv;
    CurveTraveler traveler;
    InputsManager playerInputs;
    DevTools devtools;
    public float DistanceTir = 8f;
    public float HauteurTir = 0.8f;
    private void Awake()
    {
        parentObj = transform.parent.gameObject;
        curv = GameObject.Find("Parabole").GetComponent<Curve>();
        traveler = GetComponent<CurveTraveler>();
        GameObject player = GameObject.Find("Player");
        playerInputs = player.GetComponent<InputsManager>();
        devtools = player.GetComponent<DevTools>();
    }
    private void Start()
    {
        curv.curveType = Curve.CurveType.BezierCurve;
        Vector3 hauteurTir = HauteurTir * Vector3.up;
        transform.position = hauteurTir;
        if (playerInputs.zTargeting)
        {
            var distanceZTarget = (playerInputs.zTarget.position - transform.position) * 0.07f;
            Vector3 hauteurPara = (2 * Mathf.Abs(playerInputs.zTarget.position.y - transform.position.y) + HauteurTir) * Vector3.up;
            curv.points = new Vector3[4] { hauteurTir,
                                           0.25f * distanceZTarget + hauteurPara,
                                           0.5f * distanceZTarget + hauteurPara,
                                           distanceZTarget};
        }
        else
        {
            float quarterDistance = DistanceTir * 0.25f;
            curv.points = new Vector3[4] {  hauteurTir ,
                                            quarterDistance * Vector3.forward + hauteurTir,
                                            2 * quarterDistance * Vector3.forward + hauteurTir,
                                            DistanceTir * Vector3.forward + new Vector3(2f,-2f,0f) };
        }
        traveler.curve = curv;
        devtools.RefreshCurveArray();
        traveler.enabled = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer != 7)
        {
            playerInputs.canUseItem = true;
            traveler.enabled = false;
            if (collision.gameObject.CompareTag("Enemy"))
                collision.gameObject.GetComponent<Enemy>().Hurt(2f);
            Destroy(parentObj);
            devtools.RefreshCurveArray();
        }
    }
    private void Update()
    {
        if (!traveler.isActiveAndEnabled)
        {
            playerInputs.canUseItem = true;
            Destroy(parentObj);
            devtools.RefreshCurveArray();
        }
    }
}
