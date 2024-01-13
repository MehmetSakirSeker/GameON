using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hero>() != null)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (spawnPoints[i].GetComponent<EnemySpawner>().isSpawned == false)
                {
                    spawnPoints[i].GetComponent<EnemySpawner>().SpawnEnemy();
                }
            }
        }
    }
}
