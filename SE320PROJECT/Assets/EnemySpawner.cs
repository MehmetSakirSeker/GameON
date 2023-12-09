using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyAI;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject newEnemy = Instantiate(enemyAI, transform);
        newEnemy.GetComponent<EnemyAI>().fpsc = GameObject.Find("Player").GetComponent<Hero>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
