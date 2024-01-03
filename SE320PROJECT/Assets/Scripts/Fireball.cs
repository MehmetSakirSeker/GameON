using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("References")] 
    public Transform cam;
    public Transform attackPoint;
    public GameObject fireball;

    [Header("Settings")] 
    public float fireballCooldown;


    [Header("Casting")]
    public KeyCode fireballKey = KeyCode.T;
    public float fireballForce;

    private bool readyToCast;


    private void Start()
    {
        readyToCast = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(fireballKey) && readyToCast)
        {
            Cast();
        }
    }

    private void Cast()
    {
        readyToCast = false;

        GameObject fireballProjectile = Instantiate(fireball, attackPoint.position, cam.rotation);

        Rigidbody fireballRB = fireballProjectile.GetComponent<Rigidbody>();
        

        Vector3 forceToAdd = cam.transform.forward * fireballForce;
        
        fireballRB.AddForce(forceToAdd, ForceMode.Impulse);

        Invoke(nameof(ResetFireball), fireballCooldown);
    }

    private void ResetFireball()
    {
        readyToCast = true;
    }
}
