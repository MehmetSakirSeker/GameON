using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAddon : MonoBehaviour
{
    private Rigidbody rigidBody;
    private float lifeTime = 5f;
    private float timer;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
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
        if (other.gameObject.GetComponent<EnemyHealth>() != null)
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(300);
            Destroy(gameObject);
        }
    }
}
