using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform cameraTarget;

    [Header("Value")]
    public float rotationSpeed;
    public float minXRotation;
    public float maxXRotation;

    float y;
    float x;

    private Camera worldCamera;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        worldCamera = GameObject.Find("WorldCamera")?.GetComponent<Camera>();

        worldCamera.depth = 2;
    }

    private void Start()
    {
        mainCamera.rect = new Rect(0f, 0f, 1f, 1f);
        worldCamera.rect = new Rect(0.75f, 0.75f, 0.24f, 0.24f);
    }

    void LateUpdate()
    {
        if (GameManager.Instance.isGameOver) return;

        y += Input.GetAxis("Mouse X") * rotationSpeed;
        x -= Input.GetAxis("Mouse Y") * rotationSpeed;
        x = Mathf.Clamp(x, minXRotation, maxXRotation);

        cameraTarget.rotation = Quaternion.Euler(x, y, 0);
    }
}
