//Jeremy Legault

using UnityEngine;

public class SwordBehavior : MonoBehaviour
{
    Transform handPos;
    [SerializeField] private float dureeDeVie = 0.2f;
    private float chrono = 0;
    private void Awake()
    {
        handPos = GetComponentInParent<Transform>();
    }
    private void Update()
    {
        if (dureeDeVie < chrono)
            Destroy(gameObject);
        transform.RotateAround(handPos.position, Vector3.up, 120 / dureeDeVie * Time.deltaTime);
        chrono += Time.deltaTime;
    }
}
