//Jeremy Legault

using UnityEngine;

public class FouetBehavior : MonoBehaviour
{
    GameObject player;
    GameObject leftHand;
    InputsManager playerInputs;
    Transform transParent;
    [SerializeField] private float dureeDeVie = 0.25f;
    float chrono = 0f;
    public bool retainSize = false;
    private void Awake()
    {
        player = GameObject.Find("Player");
        leftHand = GameObject.Find("PlayerLeftHandPos");
        playerInputs = player.GetComponent<InputsManager>();
        transParent = transform.parent;
    }
    private void Update()
    {
        transParent.rotation = player.transform.rotation;
        transParent.position = leftHand.transform.position + (player.transform.rotation * new Vector3(0, 0, transform.localScale.y));
        playerInputs.skewedMovement = Vector3.zero;

        if (chrono > dureeDeVie)
        {
            Destroy(transParent.gameObject);
            playerInputs.canUseItem = true;
        }
        chrono += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    { if (other.gameObject.CompareTag("Enemy")) { other.GetComponent<Enemy>().Hurt(2f); } }
}
