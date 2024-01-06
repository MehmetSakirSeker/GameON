using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
   public float ballDamage = 30f;

   private void OnTriggerEnter(Collider other)
   {
      HeroHealth hH = other.gameObject.GetComponent<HeroHealth>();
      if (hH != null)
      {
         hH.TakeDamage(ballDamage);
         Destroy(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }
   }
}
