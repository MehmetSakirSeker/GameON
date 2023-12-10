using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
   public Hero fpsc;
   private bool isAware = false;
   public float fov = 120f;
   public float viewDistance = 10f;
   public float wanderRadius = 6f;
   private Vector3 wanderPoint;
   private NavMeshAgent agent;
   private Renderer renderer;

   private void Start()
   {
      agent = GetComponent<NavMeshAgent>();
      renderer = GetComponent<Renderer>();
      wanderPoint = RandomWanderPoint();
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
         Wander();
         renderer.material.color = Color.blue;
      }
   }

   public void Wander()
   {
      if (Vector3.Distance(transform.position, wanderPoint)<2f)
      {
         wanderPoint = RandomWanderPoint();
      }
      else
      {
         agent.SetDestination(wanderPoint);
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
            RaycastHit hit;
            if (Physics.Linecast(transform.position, fpsc.transform.position, out hit, -1))
            {
               if (hit.transform.CompareTag("Player"))
               {
                  OnAware();
               }
            }
            
         }
      }
   }

   public Vector3 RandomWanderPoint()
   {
      Vector3 randomPoint = (UnityEngine.Random.insideUnitSphere * wanderRadius) + transform.position;
      NavMeshHit navHit;
      NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);

      return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
   }
}
