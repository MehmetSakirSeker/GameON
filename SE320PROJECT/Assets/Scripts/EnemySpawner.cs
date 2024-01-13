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
        GameObject newEnemy = Instantiate(enemyAI, transform.position, Quaternion.identity);
        newEnemy.GetComponent<EnemyAI>().fpsc = GameObject.FindWithTag("Player").GetComponent<Hero>();
        isSpawned = true;
        gameObject.tag = "Untagged";
    }
}
