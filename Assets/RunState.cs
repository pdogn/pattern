using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    PlayerController _controller;
    public RunState(PlayerController player)
    {
        _controller = player;
    }
    public void Enter()
    {
        Debug.Log("run enter");
        _controller.animator.Play("Run");
    }

    public void Execute()
    {
        Debug.Log("run execute");
        if(_controller.xInput == 0)
        {
            _controller.DoIdle();
        }
        if( _controller.grounded == false) 
        {
            _controller.DoJump();
        }
    }

    public void Exit()
    {
        Debug.Log("run exit");
    }

}
