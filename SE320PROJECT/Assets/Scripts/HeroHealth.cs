using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100000f;
    public float score = 0f;
    
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        Debug.Log("Player health = "+hitPoints);
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }

    public float getHitPoints()
    {
        return hitPoints;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("FireBall"))
        {
            TakeDamage(100);
            Destroy(other.gameObject);
        }
    }
}
