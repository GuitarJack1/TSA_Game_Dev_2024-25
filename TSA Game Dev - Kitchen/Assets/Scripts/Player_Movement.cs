using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Player_Controls playerControls;

    private float currSpeedForce;
    private bool letGoAfterFreeze;

    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float speedForce;

    [SerializeField]
    private float rotationSpeed;

    public enum Controls
    {
        WASD,
        Arrow,
        TFGH,
        IJKL
    }

    public Controls controls;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerControls = new Player_Controls();
        playerControls.Players.Enable();

        currSpeedForce = 0;
        letGoAfterFreeze = true;
    }

    void FixedUpdate()
    {
        Vector2 movementInput;

        switch (controls)
        {
            case Controls.WASD:
                movementInput = playerControls.Players.WASD.ReadValue<Vector2>();
                break;
            case Controls.Arrow:
                movementInput = playerControls.Players.Arrows.ReadValue<Vector2>();
                break;
            case Controls.TFGH:
                movementInput = playerControls.Players.TFGH.ReadValue<Vector2>();
                break;
            case Controls.IJKL:
                movementInput = playerControls.Players.IJKL.ReadValue<Vector2>();
                break;
            default:
                movementInput = new Vector2(0, 0);
                break;
        }

        rb.AddForce(transform.forward * currSpeedForce);

        if (movementInput.x != 0 && letGoAfterFreeze)
        {
            currSpeedForce = speedForce;
        }
        else if (movementInput.x == 0)
        {
            letGoAfterFreeze = true;
        }

        if (movementInput.x < 0 && currSpeedForce != 0)
            transform.Rotate(-Vector3.up * rotationSpeed);
        else if (movementInput.x > 0 && currSpeedForce != 0)
            transform.Rotate(Vector3.up * rotationSpeed);

        // rb.AddForce(new Vector3(movementInput.x, 0, movementInput.y) * speedForce);
        // Vector3 movementInput3D = new Vector3(movementInput.x, 0, movementInput.y);
        // if (movementInput3D != Vector3.zero)
        // {
        //     Quaternion toRotation = Quaternion.LookRotation(movementInput3D, Vector3.up);
        //     transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);
        // }

        speedLimit();
    }

    void speedLimit()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVel.magnitude > maxSpeed)
        {
            flatVel = flatVel.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(flatVel.x, rb.linearVelocity.y, flatVel.z);
        }

    }

    public void Freeze()
    {
        currSpeedForce = 0;
        letGoAfterFreeze = false;
    }
}

