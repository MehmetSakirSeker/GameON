using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCount : MonoBehaviour
{
    private float spawnPointCount;

    private float enemyCount;

    private bool isActivated;

    public bool fireballPicked;

    [SerializeField]
    private GameObject fireballLoot;
    [SerializeField]
    private GameObject stunballLoot;
    
    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Scene2")
        { 
            spawnPointCount = GameObject.FindGameObjectsWithTag("SpawnPoint").Length;
        
            enemyCount = GameObject.FindGameObjectsWithTag("Agent").Length + GameObject.FindGameObjectsWithTag("Zombie").Length + GameObject.FindGameObjectsWithTag("Wizard").Length;
        
            if (spawnPointCount <= 0 && enemyCount <= 0)
            {
                if (fireballPicked == false)
                {
                    fireballLoot.SetActive(true);
                }
                else
                {
                    stunballLoot.SetActive(true);
                }
            }  
        }
        
        
        if (SceneManager.GetActiveScene().name == "Modular Dungeon")
        { 
            enemyCount = GameObject.FindGameObjectsWithTag("Wizard").Length;
        
            if (enemyCount <= 0)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadSceneAsync(3);
            }  
        }
        
        

    }
}
