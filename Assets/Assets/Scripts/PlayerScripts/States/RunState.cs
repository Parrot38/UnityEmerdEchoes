using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunState : AbstractState
{

    AdvancedPlayerController apc;

    public RunState(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
    {
        apc = playerController;
    }

    public override void Enter()
    {
        apc.isBlocking = false;
        apc.rb.gravityScale = 6f;
        apc.attackIndex = 1;
        apc.canLaunch = true;
        apc.canAttack = true;
        apc.isRunning = true;
    }
    public override void FixedUpdate()
    {
        apc.Run();
    }
    public override void Update()
    {
        apc.AttackQueueEnter();
        if (apc.queueFirsElement == 1)
        {
            stateHandler.SetState<Attack1>();
        }
        else if(apc.queueFirsElement == 4)
        {
            stateHandler.SetState<LaunchAttack>();
        }
        else if(apc.inputAxisX == 0 && apc.grounded || Input.GetKey(KeyCode.Space))
        {
            stateHandler.SetState<IdleState>();
        }
        else if(apc.jumpWasPressed)
        {
            stateHandler.SetState<JumpState>();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            stateHandler.SetState<DashState>();
        }
    }
    public override void Exit()
    {
        apc.isRunning = false;
    }



}

