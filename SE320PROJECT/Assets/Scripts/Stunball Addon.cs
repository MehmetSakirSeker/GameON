using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunballAddon : MonoBehaviour
{
    public float stunDuration = 5f;
    
    private Rigidbody rigidBody;
    private Renderer rdr;
    private float timer;
    private float lifeTime = 10f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rdr = GetComponent<Renderer>();
        timer = 0f;
    }
    
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<EnemyAI>() != null)
        {
            EnemyAI enemyAI = other.gameObject.GetComponent<EnemyAI>();
            StartCoroutine(enemyAI.Stun(3f)); ;
            rdr.enabled = false;
            Destroy(rigidBody);
        }
    }
    
}
