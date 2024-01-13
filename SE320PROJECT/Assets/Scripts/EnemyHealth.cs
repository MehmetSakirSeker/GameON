using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, EnemyTakeDamage
{
    [SerializeField] float hitPoints = 100f;
    public GameObject burnEffectPrefab;
    private bool isBurning;

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        Debug.Log(hitPoints);
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void burn(float burnDamage)
    {
        if (isBurning == false)
        { 
            StartCoroutine(burnCoroutine(burnDamage));
        }
    }
    
    
    private IEnumerator burnCoroutine(float burnDamage)
    {
        isBurning = true;
        GameObject burnEffect = Instantiate(burnEffectPrefab, transform.position, quaternion.identity, transform);
        for (int i = 0; i < 4; i++)
        { 
            TakeDamage(burnDamage);
            yield return new WaitForSeconds(1);  
        }
        Destroy(burnEffect);
        isBurning = false;
    }
}