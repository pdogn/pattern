using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class PlayerController : MonoBehaviour
{
    StateManager stateManager;
    // Start is called before the first frame update
    void Start()
    {
        stateManager = GetComponent<StateManager>();
        stateManager.ChangeState(new IdleState(this));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            stateManager.ChangeState(new RunState(this));
        }
        else
        {
            stateManager.ChangeState(new IdleState(this));
        }
    }
}
