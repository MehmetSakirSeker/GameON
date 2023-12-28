using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    private HeroHealth heroHealth;


    private void Start()
    {
        heroHealth = GameObject.Find("Player").GetComponent<HeroHealth>();
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        Debug.Log("Enemy health = "+ hitPoints);
        if (hitPoints <= 0)
        {
            heroHealth.score++;
            Debug.Log(heroHealth.score);
            Destroy(gameObject);
            return;
        }
    }
}
