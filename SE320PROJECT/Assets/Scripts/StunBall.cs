using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBall : MonoBehaviour
{
    [Header("References")] 
    public Transform cam;
    public Transform attackPoint;
    public GameObject stunball;

    [Header("Settings")] 
    public float stunballCooldown;


    [Header("Casting")]
    public KeyCode stunballKey = KeyCode.Y;
    public float stunballForce;

    private bool readyToCast;


    private void Start()
    {
        readyToCast = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(stunballKey) && readyToCast)
        {
            Cast();
        }
    }

    private void Cast()
    {
        readyToCast = false;

        GameObject stunballProjectile = Instantiate(stunball, attackPoint.position, cam.rotation);

        Rigidbody stunballRB = stunballProjectile.GetComponent<Rigidbody>();
        

        Vector3 forceToAdd = cam.transform.forward * stunballForce;
        
        stunballRB.AddForce(forceToAdd, ForceMode.Impulse);

        Invoke(nameof(ResetStunball), stunballCooldown);
    }

    private void ResetStunball()
    {
        readyToCast = true;
    }
}
