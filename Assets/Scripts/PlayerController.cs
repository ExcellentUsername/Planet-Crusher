using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputAction PropulseAction;
    public InputAction MoveAroundAction;
    public InputAction BrakeAction;
    public InputAction RotateAction;
    public float forcePropulseSize;
    public float forceMoveAroundSize;
    private Rigidbody rb;
    private bool isPropulsing = false;
    private bool isMoving = false;
    private bool isBraking = false;
    private bool isRotating = false;
    private float VelocityMagnitudeLastFrame;
    private bool hasDeaccelerationStarted = false;
    private bool brakeJustStarted = false;
    void Awake()
    {
        Cursor.visible = false; // Hide cursor
        Rigidbody rb = GetComponentInParent<Rigidbody>();
        rb.linearDamping = 0; // No linear damping for space movement
        // Enable input actions
        PropulseAction.Enable(); 
        MoveAroundAction.Enable();
        BrakeAction.Enable();
        RotateAction.Enable();
        
        // Subscribe to rotate action events
        PropulseAction.performed += ctx => isPropulsing = true;
        PropulseAction.canceled += ctx => isPropulsing = false;

        BrakeAction.started += ctx => brakeJustStarted = true;
        MoveAroundAction.performed += ctx => isMoving = true;
        MoveAroundAction.canceled += ctx => isMoving = false;

        BrakeAction.performed += ctx => isBraking = true;
        BrakeAction.canceled += ctx => isBraking = false;

        RotateAction.performed += ctx => isRotating = true;
        RotateAction.canceled += ctx => isRotating = false;
    }

    void FixedUpdate() // Update is called once per physics frame
    {
        if(isBraking || isPropulsing){ 
            Accelerate();
        }
        if(isMoving){ 
            MoveAround();
        }
        if(isRotating){
            Rotate();
        }
        VelocityMagnitudeLastFrame = GetComponentInParent<Rigidbody>().linearVelocity.magnitude; // Store velocity magnitude for next frame comparison
    }
    void MoveAround()
    {
        Vector2 rotation = MoveAroundAction.ReadValue<Vector2>();
        // Convert local rotation input to world space torque
        Vector3 torqueVector = -transform.right * rotation.y + transform.up * rotation.x;
        GetComponentInParent<Rigidbody>().AddTorque(torqueVector * forceMoveAroundSize, ForceMode.VelocityChange); 
    }
    void Propulse()
    {
        Vector3 propulse = transform.forward * forcePropulseSize; // Calculate force vector in the forward direction
        GetComponentInParent<Rigidbody>().AddForce(propulse, ForceMode.VelocityChange); // apply propulse force vector in the forward direction
    }
    void Brake()
    {
        if(hasDeaccelerationStarted == true) // If deacceleration has just started, the next check would set velocity to zero as brake has just been pressed. We do not want that.
        {
            ApplyBrakeForce();
        }
        else if(VelocityMagnitudeLastFrame < transform.GetComponentInParent<Rigidbody>().linearVelocity.magnitude)// If brake is no longer deaccelerating but instead accelerating in the opposite direction...
        { 
            GetComponentInParent<Rigidbody>().linearVelocity = Vector3.zero; // Then stop movement when velocity is about to cross over zero and change direction
            return;
        }
        else{
            ApplyBrakeForce(); // If none of the above is true, simply apply brake force
        }
    }
    void ApplyBrakeForce()
    {
        Vector3 brake = - transform.GetComponentInParent<Rigidbody>().linearVelocity.normalized * forcePropulseSize; // Calculate force vector in the opposite direction of current velocity
        GetComponentInParent<Rigidbody>().AddForce(brake, ForceMode.VelocityChange); // Apply brake force vector in the opposite direction of current velocity
    }
    void Accelerate()
    {
        if(isBraking && isPropulsing){ // If both propulse and brake are pressed, brake
            if(brakeJustStarted) // If brake was just pressed this frame, deacceleration has just started
            {
                hasDeaccelerationStarted = true;
                hasDeaccelerationStarted = false; // From now on deacceleration is not started but simply ongoing
                brakeJustStarted = false; // Reset brake as just started and now simply ongoing
            }
            else
            {
                Brake();
            }
        }
        else if(isPropulsing){ 
            Propulse();
        }
        else
        {
            Brake();
        }
    }
    void Rotate()
    {
        // Convert local rotation input to world space torque
        Vector3 torqueVector = -transform.forward * RotateAction.ReadValue<float>();
        GetComponentInParent<Rigidbody>().AddTorque(torqueVector * forceMoveAroundSize, ForceMode.VelocityChange); 
    }
}
