using UnityEngine;

public class PlayerScriptForChasing : MonoBehaviour
{
    public AudioClip shootSound;
    private AudioSource audioSource;
    public float soundIntensity = 20f;
    public LayerMask zombieLayer;
    
    private Hero player;
    private SphereCollider collider;
    public float walkEnemyPerceptionRadius = 1.5f;
    public float sprintEnemyPerceptionRadius = 2f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponent<Hero>();
        collider = GetComponent<SphereCollider>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        else
        {
            collider.radius = walkEnemyPerceptionRadius;
        }
    }

    public void Fire()
    {
        audioSource.PlayOneShot(shootSound);
        Collider[] enemies = Physics.OverlapSphere(transform.position, soundIntensity, zombieLayer);
        Debug.Log("Number of enemies detected: " + enemies.Length);
        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log(enemies[i].name);
            enemies[i].GetComponent<EnemyAI>().OnAware();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            other.GetComponent<EnemyAI>().OnAware();
        }
    }
}
