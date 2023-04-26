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
        // Si quelqu'un révise le code dans le futur, on pourrait ajouter que s'il y a camLock près d'un ennemi, il est visé et target = ennemi.transform.position
        // Balistique de quel doit etre l'angle pour que le tir soit spot on
        float quartDeDistance = DistanceTir * 0.25f;
        Vector3 hauteurTir = HauteurTir * Vector3.up;
        transform.position = hauteurTir;
        curv.points = new Vector3[4] {  hauteurTir , quartDeDistance * Vector3.forward + hauteurTir,
                                       2 * quartDeDistance * Vector3.forward + hauteurTir,  DistanceTir * Vector3.forward + new Vector3(2,-2,0) };
        traveler.curve = curv;
        devtools.RefreshCurveArray();
        traveler.enabled = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer != 7)
        {
            playerInputs.canUseItem = true;
            traveler.enabled = false;
            Destroy(collision.collider.gameObject);     // Appeller Ennemi.Hurt() instead. For test purposes only
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
