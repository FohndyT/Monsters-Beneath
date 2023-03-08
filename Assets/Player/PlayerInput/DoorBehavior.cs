using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
   private bool IsBeingUsed = false;

   public float hauteurMax = 5f;
   public float tempsMaxOuvert = 5f;
   private float tempsOuvert = 0f;
   private Vector3 positionBase;
   private bool isClosing = false;

   private void Awake()
   {
      positionBase = transform.position;
   }

   public void OnInteract()
   {
      IsBeingUsed = GetComponent<InteractInput>().IsUsing;
   }

   private void Update()
   {
      if (IsBeingUsed && transform.position.y <= hauteurMax)
      {
         transform.position += Vector3.up * Time.deltaTime;
         tempsOuvert += Time.deltaTime;
      }
      if(transform.position.y >= hauteurMax)
         tempsOuvert += Time.deltaTime;
      
      if (tempsOuvert >= tempsMaxOuvert)
         if (transform.position.y >= positionBase.y)
            isClosing = true;
         
      if (isClosing)
      {
         transform.position += Vector3.down * (2f * Time.deltaTime);
         if (transform.position.y <= positionBase.y)
         {
            isClosing = false;
            transform.position = positionBase;
            IsBeingUsed = false;
            tempsOuvert = 0f;
         }
      }


   }
}
