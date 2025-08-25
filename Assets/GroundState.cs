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
    }

    public void Execute()
    {
        Debug.Log("ground execute");
    }

    public void Exit()
    {
        Debug.Log("ground exit");
    }
}
