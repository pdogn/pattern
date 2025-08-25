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
    }

    public void Execute()
    {
        Debug.Log("run execute");
    }

    public void Exit()
    {
        Debug.Log("run exit");
    }

}
