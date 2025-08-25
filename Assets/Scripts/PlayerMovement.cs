using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    enum PlayerStates { idle, kneel, run, air}

    PlayerStates state;

    bool stateComplete;

    public Animator animator;

    bool isKneel;

    //scene instanced objects
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;

    //movement properties
    public float acceleration;
    [Range(0f, 1f)]
    public float groundDecay;
    public float maxXSpeed;

    public float jumpSpeed;

    //variables
    public bool grounded;
    float xInput;
    float yInput;


    // Update is called once per frame
    void Update() {
        CheckInput();
        HandleJump();

        CheckKneel();

        if (stateComplete)
        {
            SelectState();
        }
        UpdateState();
    }

    void FixedUpdate() {
        CheckGround();
        //CheckKneel();

        HandleXMovement();
        ApplyFriction();
    }


    void SelectState()
    {
        stateComplete = false;

        if (grounded)
        {
            if (isKneel)
            {
                state = PlayerStates.kneel;
                StartKneel();
            }
            else
            {
                if (xInput == 0f)
                {
                    state = PlayerStates.idle;
                    StartIdle();
                }
                else
                {
                    state = PlayerStates.run;
                    StartRunning();
                }
            }
        }
        else
        {
            state = PlayerStates.air;
            StartAirborn();
        }
    }

    void UpdateState()
    {
        switch(state)
        {
            case PlayerStates.idle:
                UpdateIdle();
                break;
            case PlayerStates.kneel:
                UpdateKneel();
                break;
            case PlayerStates.run:
                UpdateRun();
                break;
            case PlayerStates.air:
                UpdateAirborn();
                break;
        }
    }
    void StartKneel()
    {
        animator.Play("Kneel");
    }
    void StartIdle()
    {
        animator.Play("Idle");
    }
    void StartRunning()
    {
        animator.Play("Run");
    }
    void StartAirborn()
    {
        animator.Play("Air");
    }

    void UpdateIdle()
    {
        if(!grounded || xInput != 0f || isKneel)
        {
            stateComplete = true;
        }
    }

    void UpdateRun()
    {
        if(!grounded || xInput == 0f)
        {
            stateComplete = true;
        }
    }

    void UpdateAirborn()
    {
        if (grounded)
        {
            stateComplete = true;
        }
    }

    void UpdateKneel()
    {
        if (!isKneel || !grounded || xInput !=0)
        {
            stateComplete = true;
        }
    }

    void CheckInput() {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    void HandleXMovement() {
        if (Mathf.Abs(xInput) > 0) {

            //increment velocity by our accelleration, then clamp within max
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(body.velocity.x + increment, -maxXSpeed, maxXSpeed);
            body.velocity = new Vector2(newSpeed, body.velocity.y);

            FaceInput();
        }
    }

    void FaceInput() {
        float direction = Mathf.Sign(xInput);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    void HandleJump() {
        if (Input.GetButtonDown("Jump") && grounded) {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }

    void CheckGround() {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    void CheckKneel()
    {
        if(grounded && xInput == 0)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                isKneel = true;
            }
        }
        else
        {
            isKneel = false;
        }
        
    }

    void ApplyFriction() {
        if (grounded && xInput == 0 && body.velocity.y <= 0) {
            body.velocity *= groundDecay;
        }
    }

}
