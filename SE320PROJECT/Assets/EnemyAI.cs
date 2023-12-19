using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
   public enum WanderType
   {
      Random,Waypoint
   }
   public Hero fpsc;
   private bool isAware = false;
   public WanderType wanderType = WanderType.Random;
   private bool isDetecting = false;
   public Transform[] waypoints;
   public int waypointIndex = 0;
   public double waitingTimeForWandering = 3f;
   public double waitingTimeForAttacking = 1f;

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
   private Animator animator;

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
            if (waitingTimeForAttacking<=0f)
            {
               
               if (fpsc.GetComponent<HeroHealth>().getHitPoints() <=0)
               {
                  animator.SetBool("isAttacking",false);
                  animator.SetBool("isPlayerLiving",false);
               }
               else
               {
                  animator.SetBool("isAttacking",true);
                  animator.SetBool("isPlayerLiving",true);
                  AttackPlayer();
                  waitingTimeForAttacking = 1f;
               }
               
            }
            else
            {
               waitingTimeForAttacking -= Time.deltaTime;
            }
         }
         else
         {
            agent.SetDestination(fpsc.transform.position);
            animator.SetBool("Aware",true);
            //renderer.material.color = Color.red;
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
         //renderer.material.color = Color.blue;
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
      
      fpsc.GetComponent<HeroHealth>().TakeDamage(20);
      
   }

   public void Wander()
   {
      if (wanderType == WanderType.Random)
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
      else
      {
         if (waypoints.Length>=2f)
         {
            if (Vector3.Distance(waypoints[waypointIndex].position, transform.position)<2f)
            {
               if (waypointIndex == waypoints.Length-1)
               {
                  waypointIndex = 0;
               }
               else
               {
                  waypointIndex++;
               }

               waitingTimeForWandering = 5f;
            }
            else
            {
               if (waitingTimeForWandering<=0f)
               {
                  animator.SetBool("isStanding",false);
                  agent.SetDestination(waypoints[waypointIndex].position);
               }
               else
               {
                  animator.SetBool("isStanding",true);
                  waitingTimeForWandering -= Time.deltaTime;
               }
            }
         }
         else
         {
            Debug.LogWarning("Waypoint number must be bigger than 2. => "+gameObject.name);
         }
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
