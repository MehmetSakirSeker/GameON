using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
   public Hero fpsc;
   private bool isAware = false;
   public float fov = 120f;
   public float viewDistance = 10f;
   private NavMeshAgent agent;
   private Renderer renderer;

   private void Start()
   {
      agent = GetComponent<NavMeshAgent>();
      renderer = GetComponent<Renderer>();
   }

   private void Update()
   {
      if (isAware)
      {
         agent.SetDestination(fpsc.transform.position);
         renderer.material.color = Color.red;

      } else
      {
         SearchForPlayer();
         renderer.material.color = Color.blue;
      }
   }

   public void OnAware()
   {
      isAware = true;
   }

   public void SearchForPlayer()
   {
      if (Vector3.Angle(Vector3.forward,transform.InverseTransformPoint(fpsc.transform.position)) < fov/2f)
      {
         if (Vector3.Distance(fpsc.transform.position,transform.position)<viewDistance)
         {
            OnAware();
         }
      }
   }
}
