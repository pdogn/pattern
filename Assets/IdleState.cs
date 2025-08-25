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
    }

    public void Execute()
    {
        Debug.Log("idle execute");
    }

    public void Exit()
    {
        Debug.Log("idle exit");
    }
}
