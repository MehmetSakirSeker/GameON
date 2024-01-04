using System;
using UnityEngine;
using AFPC;

/// <summary>
/// Example of setup AFPC with Lifecycle, Movement and Overview classes.
/// </summary>
public class Hero : MonoBehaviour {

    /* Movement class. Move, Jump, Run... */
    [SerializeField] Movement movement;

    /* Overview class. Look, Aim, Shake... */
    [SerializeField] Overview overview;

    public int isRunning()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

 

    /* Some classes need to initizlize */
    private void Start () {

        /* a few apllication settings for more smooth. This is Optional. */
        QualitySettings.vSyncCount = 0;
        Cursor.lockState = CursorLockMode.Locked;

        /* Initialize movement and add camera shake when landing */
        movement.Initialize();
        movement.AssignLandingAction (()=> overview.Shake(0.5f));
    }

    private void Update () {

        /* Read player input before check availability */
        ReadInput();

        /* Mouse look state */
        overview.Looking();

        /* Change camera FOV state */
        overview.Aiming();

        /* Shake camera state. Required "physical camera" mode on */
        overview.Shaking();

        /* Control the speed */
        movement.Running();
        
        
    }

    private void FixedUpdate () {

        /* Physical movement */
        movement.Accelerate();

        /* Physical rotation with camera */
        overview.RotateRigigbodyToLookDirection (movement.rb);
        
        /* Control the jumping, ground search... */
        movement.Jumping();
    }

    private void LateUpdate () {
        
        /* Camera following */
        overview.Follow (transform.position);
    }

    private void ReadInput () {

        overview.lookingInputValues.x = Input.GetAxis("Mouse X");
        overview.lookingInputValues.y = Input.GetAxis("Mouse Y");
        overview.aimingInputValue = Input.GetMouseButton(1);
        movement.movementInputValues.x = Input.GetAxis("Horizontal");
        movement.movementInputValues.y = Input.GetAxis("Vertical");
        movement.jumpingInputValue = Input.GetButtonDown("Jump");
        movement.runningInputValue = Input.GetKey(KeyCode.LeftShift);
    }

    
}
