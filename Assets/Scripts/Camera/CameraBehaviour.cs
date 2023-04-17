using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    Transform transPlayer;
    CurveTraveler camTraveler;
    public bool eagleView { get; set; } = true;
    public Vector3 camOffset { get; set; }

    void Start()
    {
        Application.targetFrameRate = 60;

        transPlayer = GameObject.Find("Player").transform;
        camTraveler = GetComponent<CurveTraveler>();
        camOffset = new(-7, 7, -7);
    }
    void Update()
    {
        float LookY = 0;
        if (eagleView || camTraveler.isActiveAndEnabled)
            LookY = transPlayer.position.y;
        else if (!eagleView && !camTraveler.isActiveAndEnabled)
            LookY = transform.position.y;
        //transform.position = transPlayer.position + camOffset;
        transform.position = eagleView ? transPlayer.position + camOffset : new Vector3(transPlayer.position.x, 0f, transPlayer.position.z) + camOffset;
        // LookY = eagleView ? playerTrans.position.y : transform.position.y;
        transform.LookAt(new Vector3(transPlayer.position.x, LookY, transPlayer.position.z));
    }
}
