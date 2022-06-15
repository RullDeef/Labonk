using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 3.0f;

    private Rigidbody rigidBody; // cached reference
    private Camera activeCamera; // set from outside

    private Vector2 moveInputValue; // value for continious movement control
    private Vector3 lookDirection = Vector3.forward; // target viewing direction for model

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        activeCamera = FindObjectOfType<Camera>();
    }

    private void Start()
    {
        if (IsLocalPlayer)
            FindObjectOfType<CameraController>().Target = transform;
        else
            DestroyImmediate(this);
    }

    private void FixedUpdate()
    {
        UpdateMovement();
        UpdateRotation();
    }

    private void OnMove(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();
    }

    private void UpdateMovement()
    {
        Vector3 cameraForward = activeCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = activeCamera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        Vector3 direction = moveInputValue.y * cameraForward + moveInputValue.x * cameraRight;
        rigidBody.AddForce(movementSpeed * direction, ForceMode.VelocityChange);

        if (direction.magnitude > 0.1f)
            lookDirection = direction.normalized;
    }

    private void UpdateRotation()
    {
        Vector3 forward = transform.forward;
        forward = Vector3.Slerp(forward, lookDirection, 0.25f);
        transform.LookAt(transform.position + forward, Vector3.up);
    }
}
