using System;
using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEngine;

public class PlayerScriptForChasing : MonoBehaviour
{
    public AudioClip shootSound;
    public float soundIntensity = 5f;
    private AudioSource audioSource;
    public LayerMask zombieLayer;
    private Hero player;
    private SphereCollider collider;
    public float walkEnemyPerceptionRadius = 1.5f;
    public float sprintEnemyPerceptionRadius = 2f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponent<Hero>();
        collider = GetComponent<SphereCollider>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        if (player.isRunning() == 0)
        {
            collider.radius = sprintEnemyPerceptionRadius;
        }
        else
        {
            collider.radius = walkEnemyPerceptionRadius;
        }
    }

    public void Fire()
    {
        audioSource.PlayOneShot(shootSound);
        Collider[] enemies = Physics.OverlapSphere(transform.position, soundIntensity, zombieLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyAI>().OnAware();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            other.GetComponent<EnemyAI>().OnAware();
        }
    }
}
