using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private float delai;
    private float time;
    void Start()
    {
        this.Attendre(3f, () =>
        {
            GetComponent<Renderer>().material.color = Color.red;
            if (true)
            {
                this.Attendre(3f, ()=> {GetComponent<Renderer>().material.color = Color.green;});
            }

        });
            
    }
}
