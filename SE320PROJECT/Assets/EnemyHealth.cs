using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        Debug.Log("Enemy health = "+ hitPoints);
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }
}
