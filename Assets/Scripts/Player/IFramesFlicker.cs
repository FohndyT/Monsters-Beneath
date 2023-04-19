using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFramesFlicker : MonoBehaviour
{
    [SerializeField] public Material PlayerTexture;
    private float maxFlicker = 0.2f;

        private void Update()
        {
            if (PlayerState.canBeHit && PlayerTexture.color.a >= maxFlicker)
            {
                var playerTextureColor = PlayerTexture.color;
                playerTextureColor.a -= Time.deltaTime;
                PlayerTexture.color = playerTextureColor;
            }
            else
                PlayerTexture.color = new Color(255, 255, 255, 255);
        }
}
