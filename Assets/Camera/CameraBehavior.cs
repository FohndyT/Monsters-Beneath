using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        Application.targetFrameRate = 60;   //  Will be transferred to FSMJeu

        player = GameObject.Find("PlayerCharacter");
        transform.rotation = Quaternion.Euler(new(45, 45, 0));
    }

    void Update()
    {
        transform.position = player.transform.position + new Vector3(-7, 7, -7);
    }
}
