using UnityEngine;
using AFPC;

/// <summary>
/// Example of setup AFPC with Lifecycle, Movement and Overview classes.
/// </summary>
public class Hero : MonoBehaviour {

    /* UI Reference */
    public HUD HUD;

    /* Lifecycle class. Damage, Heal, Death, Respawn... */

    /* Movement class. Move, Jump, Run... */
    public Movement movement;

    /* Overview class. Look, Aim, Shake... */
    public Overview overview;

    /* Optional assign the HUD */
    private void Awake () {
        if (HUD) {
            HUD.hero = this;
        }
    }

    /* Some classes need to initizlize */
    private void Start () {

        /* a few apllication settings for more smooth. This is Optional. */
        QualitySettings.vSyncCount = 0;
        Cursor.lockState = CursorLockMode.Locked;

        /* Initialize lifecycle and add Damage FX */

        /* Initialize movement and add camera shake when landing */
        movement.Initialize();
        movement.AssignLandingAction (()=> overview.Shake(0.5f));
    }

    private void Update () {

        /* Read player input before check availability */
        ReadInput();

        /* Block controller when unavailable */

        /* Mouse look state */
        overview.Looking();

        /* Change camera FOV state */
        overview.Aiming();

        /* Shake camera state. Required "physical camera" mode on */
        overview.Shaking();

        /* Control the speed */
        movement.Running();

        /* Control the jumping, ground search... */
        movement.Jumping();

        /* Control the health and shield recovery */
    }

    private void FixedUpdate () {

        /* Block controller when unavailable */

        /* Physical movement */
        movement.Accelerate();

        /* Physical rotation with camera */
        overview.RotateRigigbodyToLookDirection (movement.rb);
    }

    private void LateUpdate () {
        
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
