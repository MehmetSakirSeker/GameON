using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyAI;
    public bool isSpawned = false;
    
    public void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyAI, transform);
        newEnemy.GetComponent<EnemyAI>().fpsc = GameObject.Find("Player").GetComponent<Hero>();
    }
}
