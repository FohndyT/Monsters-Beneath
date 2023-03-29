using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public bool eagleView { get; set; } = true;
    public Vector3 camOffset;
    Transform playerTrans;
    CurveTraveler camTraveler;

    void Start()
    {
        Application.targetFrameRate = 60;

        playerTrans = GameObject.Find("Player").transform;
        camTraveler = GetComponent<CurveTraveler>();
        camOffset = new(-7, 7, -7);
    }
    void Update()
    {
        float LookY = 0;
        if (eagleView || camTraveler.isActiveAndEnabled)
        {
            transform.position = playerTrans.position + camOffset;
            LookY = playerTrans.position.y;
        }
        else if (!eagleView && !camTraveler.isActiveAndEnabled)
        {
            transform.position = new Vector3(playerTrans.position.x, 0f, playerTrans.position.z) + camOffset;
            LookY = transform.position.y;
        }
        // transform.position = eagleView ? playerTrans.position + offset2D : new Vector3(playerTrans.position.x, 0f, playerTrans.position.z) + offset2D;
        // LookY = eagleView ? playerTrans.position.y : transform.position.y;
        transform.LookAt(new Vector3(playerTrans.position.x, LookY, playerTrans.position.z));
    }
}
