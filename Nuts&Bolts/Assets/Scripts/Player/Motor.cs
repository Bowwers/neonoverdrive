using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Motor : MonoBehaviour
{
    #region Variables
    public float MaxVerticalLookAngle;

    private Vector3 Velocity = Vector3.zero;
    private Vector3 Rotation = Vector3.zero;
    private Vector3 CameraRotation = Vector3.zero;

    private Rigidbody Body;
    private Camera PlayerCamera;
    private float CameraRotationAngle;
    #endregion

    void Start()
    {
        Body = GetComponent<Rigidbody>();
        PlayerCamera = GetComponentInChildren<Camera>();
    }

    public void Move(Vector3 velocity)
    {
        Velocity = velocity;
    }

    public void Rotate(Vector3 rotation)
    {
        Rotation = rotation;
    }

    public void RotateCamera(Vector3 rotation)
    {
        CameraRotation = rotation;
    }

    void FixedUpdate()
    {
        ApplyGravity();
        ApplyVelocity();
        ApplyRotation();
    }

    // Does a quick raycast to check how far it is from the floor
    void ApplyGravity()
    {
        RaycastHit hit;
        if (!Physics.Raycast(Body.transform.position, Vector3.down, out hit, 1.5f))
        {
            Move(Vector3.down * 9.8f);
        }
    }

    // Makes the player character move forward
    void ApplyVelocity()
    {
        Body.MovePosition(Body.position + Velocity * Time.fixedDeltaTime);
    }

    // Makes the player character rotate around the Y axis, and the camera around the X axis
    void ApplyRotation()
    {
        if (Rotation != Vector3.zero)
        {
            Body.MoveRotation(Body.rotation * Quaternion.Euler(Rotation));
        }
        if (CameraRotation != Vector3.zero)
        {
            // Keep track of the camera rotation angle
            if (Mathf.Abs(CameraRotationAngle + CameraRotation.x ) > MaxVerticalLookAngle)
            {
                CameraRotation = Vector3.zero;
            }
            else
            {
                CameraRotationAngle += CameraRotation.x;
            }

            PlayerCamera.transform.Rotate(CameraRotation);
        }
    }
}
