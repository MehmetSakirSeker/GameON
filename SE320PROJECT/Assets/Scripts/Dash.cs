using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    public Vector3 velocity;
    private Rigidbody rb;
    
    private bool dashing = true;
    private float dashingPower = 15f;
    private float dashingCooldown = 0.7f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && dashing)
        {
            StartCoroutine(DashMethod());
        }
        
    }

    private IEnumerator DashMethod()
    {
        dashing = false;

        velocity = new Vector3(transform.forward.x * dashingPower, 0f, transform.forward.z * dashingPower);

        rb.velocity = Vector3.zero;
        
        rb.AddForce(transform.forward + velocity, ForceMode.VelocityChange);
        
        yield return new WaitForSeconds(dashingCooldown);
        dashing = true;
    }
    
}
