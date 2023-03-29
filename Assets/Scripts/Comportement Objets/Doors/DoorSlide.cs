using UnityEngine;

public class DoorSlide : MonoBehaviour
{
    private bool isInteracting = false;

    public float hauteurMax = 5f;
    public float tempsMaxOuvert = 5f;
    private float tempsOuvert = 0f;
    private Vector3 positionBase;
    private bool isClosing = false;

    private void Awake()
    {
        positionBase = transform.position;
    }

    public void OnAttack()
    {
        isInteracting = GetComponent<InteractInput>().isUsing;
    }

    private void Update()
    {
        if (isInteracting && transform.position.y <= hauteurMax)
        {
            transform.position += Vector3.up * Time.deltaTime;
            tempsOuvert += Time.deltaTime;
        }
        if (transform.position.y >= hauteurMax)
            tempsOuvert += Time.deltaTime;

        if (tempsOuvert >= tempsMaxOuvert && transform.position.y >= positionBase.y)
            isClosing = true;

        if (isClosing)
        {
            transform.position += Vector3.down * (2f * Time.deltaTime);
            if (transform.position.y <= positionBase.y)
            {
                isClosing = false;
                transform.position = positionBase;
                isInteracting = false;
                tempsOuvert = 0f;
            }
        }
    }
}
