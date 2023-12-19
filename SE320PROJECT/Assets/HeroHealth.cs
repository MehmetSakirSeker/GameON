using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
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
}
