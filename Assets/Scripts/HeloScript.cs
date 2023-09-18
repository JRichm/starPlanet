using UnityEngine;
using UnityEngine.InputSystem;

public class HeloScript : MonoBehaviour {

    public float throttleSpeed = 10f;
    public float pitchSpeed = 10f;
    public float rollSpeed = 10f;
    public float yawSpeed = 10f;
    public float damping = 20f;

    public Transform shipFirePosition;

    private float thrust1D;
    private float pitch1D;
    private float roll1D;
    private float yaw1D;
    private Vector3 forwardBoost;
    private Vector3 upBoost;
    private bool isFiring;

    private Rigidbody rb;
    private int fireRoutine = 0;

    [SerializeField] ShipWeapon currentWeapon;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {

        // Check for fire input.
        if (isFiring) {
            // Call the Shoot method of the current gun.
            if (currentWeapon != null) {
                currentWeapon.Shoot();
            }
        } else {
            if (currentWeapon != null) {
                currentWeapon.CoolWeapon();
            }
        }
    }

    private void FixedUpdate() {

        // Apply helicopter movement based on input.
        Vector3 rotation = new Vector3(-pitch1D * pitchSpeed, yaw1D * yawSpeed, -roll1D * rollSpeed);
        transform.Rotate(rotation * Time.deltaTime);

        Vector3 forwardForce = (transform.up + transform.forward) * thrust1D * throttleSpeed;

        Vector3 boostForce = (upBoost + forwardBoost) * thrust1D * throttleSpeed;

        Vector3 dampingForce = -rb.velocity * damping;

        rb.AddForce(dampingForce * Time.deltaTime);

        rb.AddForce(forwardForce * Time.deltaTime);

        rb.AddForce(boostForce * Time.deltaTime);
    }

    public void OnThrust(InputAction.CallbackContext context) {
        thrust1D = context.ReadValue<float>();
    }

    public void OnPitch(InputAction.CallbackContext context) {
        pitch1D = context.ReadValue<float>();
    }

    public void OnYaw(InputAction.CallbackContext context) {
        yaw1D = context.ReadValue<float>();
    }

    public void OnRoll(InputAction.CallbackContext context) {
        roll1D = context.ReadValue<float>();
    }

    public void OnUp(InputAction.CallbackContext context) {
        upBoost = new Vector3(0, context.ReadValue<float>(), 0);
    }

    public void OnBoost(InputAction.CallbackContext context) {
        forwardBoost = new Vector3(0, 0, context.ReadValue<float>()); 
    }

    public void OnFire(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started) {
            // The button was pressed, start firing.
            isFiring = true;
        } else if (context.phase == InputActionPhase.Canceled) {
            // The button was released, stop firing.
            isFiring = false;
        }
    }
}