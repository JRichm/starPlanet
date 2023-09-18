using UnityEngine;
using UnityEngine.InputSystem;

public class HeloScript : MonoBehaviour {

    public float throttleSpeed = 10f;
    public float pitchSpeed = 10f;
    public float rollSpeed = 10f;
    public float yawSpeed = 10f;

    private float thrust1D;
    private float pitch1D;
    private float roll1D;
    private float yaw1D;

    private void FixedUpdate() {

        // Apply helicopter movement based on input.
        Vector3 rotation = new Vector3(-pitch1D * pitchSpeed, yaw1D * yawSpeed, -roll1D * rollSpeed);
        transform.Rotate(rotation * Time.deltaTime);

        Vector3 forwardForce = transform.up * thrust1D * throttleSpeed;

        GetComponent<Rigidbody>().AddForce(forwardForce * Time.deltaTime);
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
}