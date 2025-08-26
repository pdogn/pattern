using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class PlayerController : MonoBehaviour
{
    StateManager stateManager;

    public Animator animator;

    //scene instanced objects
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    [SerializeField] Rigidbody2D rb;

    //movement properties
    public float acceleration;
    [Range(0f, 1f)]
    public float groundDecay;
    public float maxXSpeed;
    public float jumpSpeed;

    //variables
    public bool grounded;
    public float xInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        stateManager = GetComponent<StateManager>();
        stateManager.ChangeState(new IdleState(this));
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();

        HandleJump();
    }

    private void FixedUpdate()
    {
        CheckGround();
        HandleXMovement();
        ApplyFriction();
    }

    void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
    }
    
    void HandleXMovement()
    {
        if (xInput != 0)
        {

            //increment velocity by our accelleration, then clamp within max
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(rb.velocity.x + increment, -maxXSpeed, maxXSpeed);
            rb.velocity = new Vector2(newSpeed, rb.velocity.y);

            FaceInput();
        }
    }
    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }
    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    public void DoIdle()
    {
        stateManager.ChangeState(new IdleState(this));
    }
    public void DoRun()
    {
        stateManager.ChangeState(new RunState(this));
    }
    public void DoKneel()
    {
        stateManager.ChangeState(new GroundState(this));
    }
    public void DoJump()
    {
        stateManager.ChangeState(new AirState(this));
    }

    void FaceInput()
    {
        float direction = Mathf.Sign(xInput);
        transform.localScale = new Vector3(direction, 1, 1);
    }
    void ApplyFriction()
    {
        if (grounded && xInput == 0 && rb.velocity.y <= 0)
        {
            rb.velocity *= groundDecay;
        }
    }
}
