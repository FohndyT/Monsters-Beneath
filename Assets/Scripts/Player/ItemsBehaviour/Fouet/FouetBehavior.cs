using UnityEngine;

public class FouetBehavior : MonoBehaviour
{
    InputsManager playerInputs;
    Transform transParent;
    Vector3 posIni;
    [SerializeField] private float dureeDeVie = 0.5f;
    float chrono = 0f;
    public bool retainSize = false;
    private void Awake()
    {
        playerInputs = GameObject.Find("Player").GetComponent<InputsManager>();
        transParent = transform.parent;
        posIni = transParent.position;
    }
    private void Update()
    {
        if (!retainSize)
        {
            float x = chrono * Mathf.PI / dureeDeVie;
            transParent.localScale = new(1, 1, Mathf.Abs(Mathf.Sin(x)));
            transParent.position += Mathf.Cos(x) / Mathf.PI * transform.up + (transParent.position - posIni);
        }
        if (chrono > dureeDeVie)
        {
            Destroy(transParent.gameObject);
            playerInputs.canUseItem = true;
        }
        chrono += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);     /* Appeller Ennemi.Hurt() instead. For test purposes only*/
    }
}
