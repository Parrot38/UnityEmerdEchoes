using UnityEngine;
using UnityEngine.EventSystems;

public class IdleState : AbstractState
{
    AdvancedPlayerController apc;
    public IdleState(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
    {
        apc = playerController;
    }

    public override void Enter()
    {
        apc.ClearFields();
        apc.canAttack = true;

        apc.playerCollider.enabled = true;
        apc.guardCollider.enabled = false;

        apc.rb.gravityScale = 6f;
        apc.attackIndex = 1;
        apc.rb.linearVelocity = Vector2.zero;
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        apc.AttackQueueEnter();
        if (apc.queueFirsElement == 1)
        {
            stateHandler.SetState<Attack1>();
        }
        else if (apc.queueFirsElement == 4)
        {
            stateHandler.SetState<LaunchAttack>();
        }

        else if (apc.inputAxisX != 0 && apc.grounded && apc.queueCount==0 && !Input.GetKey(KeyCode.Space))
        {
            stateHandler.SetState<RunState>();
        }
        else if (apc.jumpWasPressed && apc.queueCount == 0)
        {
            stateHandler.SetState<JumpState>();
        }
        apc.SetBlockState();

    }

    public override void Exit()
    {

    }
}
