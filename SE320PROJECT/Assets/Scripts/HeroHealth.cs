using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100000f;
    public Slider slider;
    public GameObject healthUI;
    private float maxHealth = 100000f;
    
    private void Start()
    {
        maxHealth = hitPoints;
        slider.value = CalculateHealth();
    }

    private void Update()
    {
        slider.value = CalculateHealth();

        if (hitPoints<maxHealth)
        {
            healthUI.SetActive(true);
        }
        
        
    }

    float CalculateHealth()
    {
        return hitPoints / maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        Debug.Log("Player health = "+hitPoints);
        if (hitPoints <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadSceneAsync(3, LoadSceneMode.Single);
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
