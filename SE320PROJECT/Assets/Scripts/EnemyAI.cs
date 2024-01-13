using System.Collections;
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
   public float attackDistance = 8f;
   
   public float fov = 120f;
   public float viewDistance = 10f;
   public float wanderRadius = 6f;
   private Vector3 wanderPoint;
   private NavMeshAgent agent;
    
   public float wanderingSpeed = 1.4f;
   public float chaseSpeed = 2f;
   private bool isStunned = false;
   private Renderer renderer;
   public GameObject projectile;
   public Transform attackPointForWizard;
   
   [SerializeField] ParticleSystem muzzleFlash;

   private void Start()
   {
      agent = GetComponent<NavMeshAgent>();
      wanderPoint = RandomWanderPoint();
      renderer = GetComponent<Renderer>();
   }

   private void Update()
   {
      if (isAware)
      {
         if (agent.tag =="Zombie")
         {
            wanderType = WanderType.Random;
         }
         
         if (DistanceToPlayer() <= attackDistance)
         {
            LookPlayer();
            if (waitingTimeForAttacking<=0f)
            {
               if (!isStunned){
                     renderer.material.color = Color.blue;
                     if (agent.tag =="Zombie")
                     {
                        AttackPlayerByZombie();
                     }else if (agent.tag == "Agent")
                     {
                        AttackPlayerByAgent();
                     }else if (agent.tag == "Wizard")
                     {
                        AttackPlayerByWizard();
                     }
                     
                     waitingTimeForAttacking = 2.4f;
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
            Debug.Log("movedd");
            renderer.material.color = Color.red;
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
         agent.speed = wanderingSpeed;
         Wander();
         renderer.material.color = Color.green;
         
      }
      SearchForPlayer();
      
   }

   private void LookPlayer()
   {
      Vector3 directionToPlayer = fpsc.transform.position - gameObject.transform.position;
      directionToPlayer.y = 0f; // Ensure the enemy doesn't tilt up or down

      // Rotate the enemy to face the player
      Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
      transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 4*Time.deltaTime);
   }
   public float DistanceToPlayer()
   {
      float distance = Vector3.Distance(transform.position, fpsc.transform.position);
      return distance;
   }

   public void AttackPlayerByZombie()
   {
      fpsc.GetComponent<HeroHealth>().TakeDamage(10);
      
   }
   public void AttackPlayerByWizard()
   {
      Vector3 hitPoint = attackPointForWizard.position;
      Rigidbody rb = Instantiate(projectile, hitPoint, Quaternion.identity).GetComponent<Rigidbody>();
      Vector3 shootPoint = fpsc.transform.position - transform.position;
      rb.AddForce(shootPoint*2f,ForceMode.Impulse);
      rb.AddForce(transform.up*2.5f,ForceMode.Impulse);
   }
   public void AttackPlayerByAgent()
   {
      
      if (IsPlayerInFrontOfGun())
      {
         muzzleFlash.Play();
         fpsc.GetComponent<HeroHealth>().TakeDamage(20);
      }
      
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
                  agent.SetDestination(waypoints[waypointIndex].position);
               }
               else
               {
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

   public bool IsPlayerInFrontOfGun()
   {
      Vector3 directionToTarget = transform.position - fpsc.transform.position;
      float angel = Vector3.Angle(transform.forward, directionToTarget);
      return angel >90 && angel<270;
   }

   public Vector3 RandomWanderPoint()
   {
      Vector3 randomPoint = (UnityEngine.Random.insideUnitSphere * wanderRadius) + transform.position;
      NavMeshHit navHit;
      NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);

      return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
   }

   public IEnumerator Stun(float stunduration)
   {
      if (!isStunned)
      {
         Debug.Log("stun");
         isStunned = true;
         agent.isStopped = true;
         Color oldColor = renderer.material.color;
         renderer.material.color = Color.magenta;
         yield return new WaitForSeconds(stunduration);
         Debug.Log("end of the stun");
         renderer.material.color = oldColor;
         isStunned = false;
         agent.isStopped = false;
      }
   }
}
