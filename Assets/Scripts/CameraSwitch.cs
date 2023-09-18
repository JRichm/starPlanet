using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitch : MonoBehaviour {
    public GameObject thirdPersonCamera;
    public GameObject firstPersonCamera;
    public float transitionSpeed = 5.0f; // Adjust the speed as needed.

    private bool isFirstPersonView = false;
    private float transitionProgress = 0.0f;

    private void Start() {
        // Initially, enable the third-person camera and disable the first-person camera.
        SetCameraViews(false, 0f);
    }

    private void Update() {
        // Check for right mouse button input to toggle between views.
        if (Mouse.current.rightButton.wasPressedThisFrame) {
            isFirstPersonView = !isFirstPersonView;
        }

        // Smoothly interpolate between cameras.
        transitionProgress = Mathf.Clamp01(transitionProgress + Time.deltaTime * transitionSpeed);

        // Set the camera views based on the interpolation progress.
        SetCameraViews(isFirstPersonView, transitionProgress);
    }

    private void SetCameraViews(bool isFirstPerson, float progress) {
        if (progress >= 1.0f) {
            // Transition is complete, switch to the new view.
            thirdPersonCamera.SetActive(!isFirstPerson);
            firstPersonCamera.SetActive(isFirstPerson);
            transitionProgress = 0.0f;
        } else {
            // Interpolate between camera positions.
            thirdPersonCamera.transform.position = Vector3.Lerp(thirdPersonCamera.transform.position, firstPersonCamera.transform.position, progress);
            thirdPersonCamera.transform.rotation = Quaternion.Slerp(thirdPersonCamera.transform.rotation, firstPersonCamera.transform.rotation, progress);
        }
    }
}
