using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAddon : MonoBehaviour
{
    private Rigidbody rigidBody;
    private float lifeTime = 5f;
    private float timer;
    private float fireballDamage;
    private float burnDamage;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        fireballDamage = GameObject.Find("Player").GetComponent<Fireball>().fireballDamage;
        burnDamage = GameObject.Find("Player").GetComponent<Fireball>().burnDamage;
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
            enemyHealth.TakeDamage(fireballDamage);
            enemyHealth.burn(burnDamage);
            Destroy(gameObject);
        }
    }

 
}
