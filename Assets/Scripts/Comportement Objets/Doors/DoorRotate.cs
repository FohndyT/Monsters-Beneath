using System.Collections;
using UnityEngine;

public class DoorRotate : MonoBehaviour
{
    BoxCollider colle;
    #region Attributes
    Coroutine allerRetour;
    bool canOpen = true;
    bool enRetour = false;
    Vector3 pivot;
    [SerializeField] float dur�eTransition = 1;
    [SerializeField] float waitTime = 2;
    [SerializeField] float angleOuverture = 90;
    float timer;
    #endregion

    private void Awake()
    {
        colle = GetComponent<BoxCollider>();
        pivot = new Vector3(transform.position.x - (transform.localScale.x * 0.5f), transform.position.y, transform.position.z);
    }

    public void OnAttack()
    {
        if (GetComponent<InteractInput>().isUsing && canOpen && allerRetour == null)
            allerRetour = StartCoroutine(AllerRetour());
    }
    IEnumerator AllerRetour()
    {
        StartCoroutine(Pivoter(-angleOuverture));
        yield return new WaitForSeconds(dur�eTransition + waitTime);
        StartCoroutine(Pivoter(angleOuverture));
        yield return new WaitForSeconds(dur�eTransition);    // Emp�che d'ouvrir la porte midway lorsqu'elle se ferme
        canOpen = true;
        StopCoroutine(allerRetour);
        allerRetour = null;
    }
    IEnumerator Pivoter(float angle)
    {
        colle.enabled = false;
        while (timer < dur�eTransition)
        {
            transform.RotateAround(pivot, Vector3.up, angle * Time.deltaTime / dur�eTransition);
            timer += Time.deltaTime;
            yield return null;
        }
        colle.enabled = true;
        timer = 0f;
        enRetour = !enRetour;
        StopCoroutine(Pivoter(0));
    }
}
