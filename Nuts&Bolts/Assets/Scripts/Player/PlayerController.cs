using UnityEngine;

public enum MovementStatus { IDLE, WALK, DASH, JUMP, CROUCHING, ZOOMED, MELEE }

[RequireComponent(typeof(Motor))]
public class PlayerController : MonoBehaviour
{
    #region Variables
    public string VerticalAxisName;
    public string HorizontalAxisName;
    public string VerticalRightAxisName;
    public string HorizontalRightAxisName;

    public float CrouchSpeed = 4f;
    public float WalkSpeed = 5f;
    public float RunSpeed = 8f;

    public float DashDurationMs = 800f;

    public float LookSensitivity = 3f;

    private Motor PhysicsMotor;
    private MovementStatus CurrentMovementStatus;

    private int PlayerId = 1;
    private float CurrentSpeed;
    private bool RunFlag;
    #endregion

    // Use this for initialization
    void Start ()
    {
        // Get the physics motor for this player
        PhysicsMotor = gameObject.GetComponent<Motor>();
        CurrentMovementStatus = MovementStatus.IDLE;
        CurrentSpeed = 0;
	}

    void Update ()
    {
        DealWithInput();
        DealWithMovement();
        LookControl();
    }

    // Moves the character
    void DealWithMovement ()
    {
        // Check which buttons are down, etc
        if (CurrentMovementStatus == MovementStatus.IDLE)
        {
            PhysicsMotor.Move(Vector3.zero);
        }

        // Handle input
        if (CurrentMovementStatus == MovementStatus.WALK ||
            CurrentMovementStatus == MovementStatus.CROUCHING)
        {
            float xAxis = Input.GetAxisRaw(HorizontalAxisName);
            float yAxis = Input.GetAxisRaw(VerticalAxisName);

            Vector3 horizontalMovement = transform.right * xAxis;
            Vector3 verticalMovement = transform.forward * yAxis;

            Vector3 velocity = (horizontalMovement + verticalMovement).normalized * CurrentSpeed;

            PhysicsMotor.Move(velocity);
        }
    }

    // Looks around
    void LookControl()
    {
        // Rotates the character to look left/right
        float yRotation = Input.GetAxisRaw(HorizontalRightAxisName);

        Vector3 rotation = new Vector3(0f, yRotation, 0f) * LookSensitivity;

        PhysicsMotor.Rotate(rotation);

        // Rotates the camera to look up/down
        float xRotation = Input.GetAxisRaw(VerticalRightAxisName);

        Vector3 cameraRotation = new Vector3(xRotation, 0f, 0f) * LookSensitivity;

        PhysicsMotor.RotateCamera(cameraRotation);
    }

    // Create a state machine to validate state changes
    void DealWithInput()
    {
        float HAxisValue = Mathf.Abs(Input.GetAxisRaw(HorizontalAxisName));
        float VAxisValue = Mathf.Abs(Input.GetAxisRaw(VerticalAxisName));

        if (HAxisValue < 0.02f &&
            VAxisValue < 0.02f)
        {
            CurrentMovementStatus = MovementStatus.IDLE;
            CurrentSpeed = 0;
        }
        else
        {
            CurrentMovementStatus = MovementStatus.WALK;
            CurrentSpeed = WalkSpeed;
        }
    }

    // Sets the player Id for input purposes
    public void SetPlayerId(int Id)
    {
        PlayerId = Id;
        HorizontalAxisName = HorizontalAxisName + Id.ToString();
        HorizontalRightAxisName = HorizontalRightAxisName + Id.ToString();

        VerticalAxisName = VerticalAxisName + Id.ToString();
        VerticalRightAxisName = VerticalRightAxisName + Id.ToString();
    }
}
