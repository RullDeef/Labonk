using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float lerpFactor = 0.25f;
    [SerializeField] private float lookSensitivity = 0.85f;

    [SerializeField] private float minYawAngle = 20.0f;
    [SerializeField] private float maxYawAngle = 40.0f;

    public Transform Target { get; set; }
    
    private void FixedUpdate()
    {
        if (Target != null)
            UpdatePosition();
    }

    private void UpdatePosition()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position, lerpFactor);
    }

    private void OnLook(InputValue value)
    {
        Vector2 viewDelta = value.Get<Vector2>();
        float deltaPitch = lookSensitivity * viewDelta.x;
        float deltaYaw = lookSensitivity * viewDelta.y;

        float currYaw = transform.localEulerAngles.z;
        if (currYaw + deltaYaw < minYawAngle)
        {
            deltaYaw = minYawAngle - currYaw;
        }
        else if (currYaw + deltaYaw > maxYawAngle)
        {
            deltaYaw = maxYawAngle - currYaw;
        }

        transform.Rotate(deltaPitch * Vector3.up, Space.Self);
    }
}
