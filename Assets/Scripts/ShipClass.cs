using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class ShipClass : MonoBehaviour {
    [Header("=== Ship Movement Settings ===")]
    [SerializeField] private float yawTorque = 500f;
    [SerializeField] private float pitchTorque = 1000f;
    [SerializeField] private float rollTorque = 1000f;
    [SerializeField] private float thrust = 100f;
    [SerializeField] private float upThrust = 50;
    [SerializeField] private float strafeThrust = 50f;
    [SerializeField, Range(0.001f, 0.999f)] private float thrustGlideReduction = 0.999f;
    [SerializeField, Range(0.001f, 0.999f)] private float upDownGlideReduction = 0.111f;
    [SerializeField, Range(0.001f, 0.999f)] private float leftRightGlideReduction = 0.111f;
    [SerializeField, Range(0.001f, 0.999f)] private float angularDamping = 0.111f;

    private Rigidbody rb;
    private Vector2 pitchYaw;
    private float thrust1D;
    private float upDown1D;
    private float strafe1D;
    private float roll1D;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.angularDrag = angularDamping;

        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void OnThrust(InputAction.CallbackContext context) {
        thrust1D = context.ReadValue<float>();
    }

    public void OnStrafe(InputAction.CallbackContext context) {
        strafe1D = context.ReadValue<float>();
    }

    public void OnUpDown(InputAction.CallbackContext context) {
        upDown1D = context.ReadValue<float>();
    }

    public void OnBoost(InputAction.CallbackContext context) {
        upDown1D = context.ReadValue<float>();
    }

    public void OnRoll(InputAction.CallbackContext context) {
        roll1D = context.ReadValue<float>();
    }

    public void OnPitchYaw(InputAction.CallbackContext context) {
        pitchYaw = context.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        // Calculate forces and torques based on input
        Vector3 forwardThrust = transform.forward * thrust * thrust1D;
        Vector3 upThrustVec = transform.up * upThrust * upDown1D;
        Vector3 strafeThrustVec = transform.right * strafeThrust * strafe1D;
        Vector3 totalThrust = forwardThrust + upThrustVec + strafeThrustVec;
        rb.AddForce(totalThrust);

        // cacluate torques for yaw, pitch, and roll
        Vector3 torque = new Vector3(-pitchYaw.y * pitchTorque, 0, roll1D * rollTorque);
        rb.AddTorque(torque);

        // apply thrust reduction to simulate glide
        rb.velocity *= thrustGlideReduction;
        rb.angularVelocity *= leftRightGlideReduction;
        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, upDownGlideReduction);
    }
}