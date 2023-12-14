using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
   public Hero fpsc;
   private bool isAware = false;
   private bool isDetecting = false;

   public float timeToLosePlayer = 5f;
   public float loseTimer = 0f;
   public float attackDistance = 5f;
   
   public float fov = 120f;
   public float viewDistance = 10f;
   public float wanderRadius = 6f;
   private Vector3 wanderPoint;
   private NavMeshAgent agent;
   private Renderer renderer;
    
   public float wanderingSpeed = 1.4f;
   public float chaseSpeed = 2f;
   public Animator animator;

   private void Start()
   {
      agent = GetComponent<NavMeshAgent>();
      renderer = GetComponent<Renderer>();
      wanderPoint = RandomWanderPoint();
      animator = GetComponentInChildren<Animator>();
   }

   private void Update()
   {
      if (isAware)
      {
         if (DistanceToPlayer() <= attackDistance)
         {
            AttackPlayer();
         }
         else
         {
            agent.SetDestination(fpsc.transform.position);
            animator.SetBool("Aware",true);
            agent.speed = chaseSpeed;
            if (!isDetecting)
            {
               loseTimer += Time.deltaTime;
               if (loseTimer>=timeToLosePlayer)
               {
                  isAware = false;
                  loseTimer = 0;
               }
            }
         }
         

      } else
      {
         Wander();
         animator.SetBool("Aware",false);
         agent.speed = wanderingSpeed;
      }
      SearchForPlayer();
      
   }
   public float DistanceToPlayer()
   {
      float distance = Vector3.Distance(transform.position, fpsc.transform.position);
      return distance;
   }

   public void AttackPlayer()
   {
      
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
      isDetecting = true;
      loseTimer = 0;
   }

   public void SearchForPlayer()
   {
      if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(fpsc.transform.position)) < fov / 2f)
      {
         if (Vector3.Distance(fpsc.transform.position, transform.position) < viewDistance)
         {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, fpsc.transform.position, out hit, -1))
            {
               if (hit.transform.CompareTag("Player"))
               {
                  OnAware();
               }
               else
                  isDetecting = false;
            }
            else
               isDetecting = false;

         }
         else
            isDetecting = false;
      }
      else
         isDetecting = false;
   }

   public Vector3 RandomWanderPoint()
   {
      Vector3 randomPoint = (UnityEngine.Random.insideUnitSphere * wanderRadius) + transform.position;
      NavMeshHit navHit;
      NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);

      return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
   }
}
