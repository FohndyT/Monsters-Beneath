using System.Collections;
using UnityEngine;

public class RelocatePlayerAfterDownfall : MonoBehaviour
{
    #region Components
    BoxCollider worldBorder;
    InputsManager inputManager;
    Transform transPlayer;
    Rigidbody playBody;
    CameraBehaviour camBehaviour;
    #endregion
    Vector3 safePt;
    bool playerDown;
    private void Awake()
    {
        worldBorder = GetComponent<BoxCollider>();
        GameObject player = GameObject.Find("Player");
        inputManager = player.GetComponent<InputsManager>();
        transPlayer = player.transform;
        playBody = player.GetComponent<Rigidbody>();
        camBehaviour = GameObject.FindObjectOfType<Camera>().GetComponent<CameraBehaviour>();
    }
    private void Start()
    { StartCoroutine(RelocateSafePt()); }

    private void OnTriggerExit(Collider other)
    { if (other.tag.Equals("Player")) { StartCoroutine(RelocatePlayer()); } }
    IEnumerator RelocateSafePt()
    {
        yield return new WaitForSeconds(1f);
        if (!playerDown && inputManager.rayHit.point != Vector3.zero && !inputManager.rayHit.transform.CompareTag("DangerZone") && inputManager.rayHit.point.y <= inputManager.rayHitMax)
            safePt = inputManager.rayHit.point;
        StopCoroutine(RelocateSafePt());
        StartCoroutine(RelocateSafePt());
    }
    public IEnumerator RelocatePlayer()
    {
        playerDown = true;
        camBehaviour.enabled = false;
        yield return new WaitForSeconds(1f);
        playBody.velocity = Vector3.zero;
        transPlayer.position = safePt + new Vector3(0, inputManager.rayHitMax, 0);
        camBehaviour.enabled = true;
        playerDown = false;
        StopCoroutine(RelocatePlayer());
    }
}