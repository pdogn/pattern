using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : IState
{
    PlayerController _controller;
    public GroundState(PlayerController player)
    {
        _controller = player;
    }
    public void Enter()
    {
        Debug.Log("ground enter");
        _controller.animator.Play("Kneel");
    }

    public void Execute()
    {
        Debug.Log("ground execute");
        
        if (_controller.xInput != 0)
        {
            _controller.DoRun();
        }
        if (!_controller.grounded)
        {
            _controller.DoJump();
        }
    }

    public void Exit()
    {
        Debug.Log("ground exit");
    }
}
