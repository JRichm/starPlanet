using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ShipHUD : MonoBehaviour
{
    [SerializeField] Image crossHair;
    [SerializeField] GameObject ship;
    [SerializeField] CinemachineVirtualCamera shipCamera;

    RaycastHit hitPos;
    LineRenderer lineRenderer;

    private void Start() {
        lineRenderer = ship.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetColors(Color.red, Color.yellow);
        lineRenderer.SetPosition(0, ship.transform.position);
        lineRenderer.SetPosition(3, ship.transform.position);
        lineRenderer.SetPosition(1, ship.transform.forward * 2);
        lineRenderer.SetPosition(2, ship.transform.forward * 2);
    }

    private void Update() {
        moveCrosshair();
    }

    private void moveCrosshair() {
        Debug.DrawRay(ship.transform.position + (ship.transform.up * 3), ship.transform.forward, Color.red);
    }
}
