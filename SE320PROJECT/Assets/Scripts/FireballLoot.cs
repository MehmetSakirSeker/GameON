using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLoot : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Hero>() != null)
        {
            StartCoroutine(spawnEnemies());
            other.gameObject.GetComponent<Fireball>().enabled = true;
            other.gameObject.GetComponent<EnemyCount>().fireballPicked = true;
            Destroy(gameObject);
        }
    }


    private IEnumerator spawnEnemies()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].GetComponent<EnemySpawner>().isSpawned == false)
            {
                spawnPoints[i].GetComponent<EnemySpawner>().SpawnEnemy();
            }
        }

        yield return null;
    }
}
