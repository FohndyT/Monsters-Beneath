// Fohndy Nomerth Tah

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// https://www.youtube.com/watch?v=Y8yq9fBU4hI&ab_channel=HamzaHerbou

public static class AttenteEnSeconde
{
    public static void Attendre(this MonoBehaviour mono, float attenteEnSeconde, UnityAction action)
    {
        mono.StartCoroutine(ExecuteAction(attenteEnSeconde, action));
    }

    private static IEnumerator ExecuteAction(float attenteEnSeconde, UnityAction action)
    {
        yield return new WaitForSecondsRealtime(attenteEnSeconde);
        action.Invoke();
        yield break;
    }
    
    // Exemples
    
    // void Start()
    // {
    //     this.Attendre(5f, () =>
    //     {
    //         Destroy(gameObject);
    //     });
    // }
    
    // ou
    
    // void Start()
    // {
    //     this.Attendre(5f, Destroy);
    // }
    
    // void Destroy()
    // {
    //     Destroy(gameObject);
    // }
}
