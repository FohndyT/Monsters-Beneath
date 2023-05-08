using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuchFlicker : MonoBehaviour
{
    Material mat;
    private Enemy enemyScript;
    private float ouchFlicker = 1f;
    public string entity;
    void Start()
    {
        enemyScript = GameObject.Find(entity).GetComponent<Enemy>();
        mat = GetComponent<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyScript.invincible)
        {
            ouchFlicker -= Time.deltaTime * 3f;
            mat.SetFloat("_DitheredAlpha",ouchFlicker);
            if (ouchFlicker <= 0.5f)
                ouchFlicker = 1f;
        }

        else
        {
            mat.SetFloat("_DitheredAlpha", 1);
            ouchFlicker = 1f;
        }
            
    }
}
