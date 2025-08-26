using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();
    void Exit();
    void Execute();

}
public class IdleState : IState
{
    PlayerController _controller;
    public IdleState(PlayerController player) 
    {
        _controller = player;
    }

    public void Enter()
    {
        Debug.Log("idle enter");
        _controller.animator.Play("Idle");
    }

    public void Execute()
    {
        Debug.Log("idle execute");
        if (Input.GetKeyDown(KeyCode.S))
        {
            _controller.DoKneel();
        }
        if(_controller.xInput != 0)
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
        Debug.Log("idle exit");
    }
}
