using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : IState
{
    PlayerController _controller;
    public AirState(PlayerController player)
    {
        _controller = player;
    }
    public void Enter()
    {
        Debug.Log("air enter");
        _controller.animator.Play("Air");
    }

    public void Execute()
    {
        Debug.Log("air execute");
        if (_controller.grounded)
        {
            _controller.DoIdle();
        }
    }

    public void Exit()
    {
        Debug.Log("air exit");
    }
}
