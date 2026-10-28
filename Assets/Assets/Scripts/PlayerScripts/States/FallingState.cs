using UnityEngine;

public class FallingState : AbstractState
{
    AdvancedPlayerController apc;
    public FallingState(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
    {
        apc = playerController;
    }

    public override void Enter()
    {
        Debug.Log("No im falling!!!");
        apc.rb.gravityScale = 6f;
        apc.attackIndex = 5;
    }


    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        apc.NeutralDelayTimer();
        apc.AttackQueueEnter();
        if (apc.queueFirsElement == 5)
        {
            stateHandler.SetState<AirAttack1>();
        }
        else if (apc.grounded && apc.jumpBuffer < 0)
        {
            apc.SetNeutralState();
        }
        
    }
    public override void Exit()
    {
        apc.rb.gravityScale = 6f;
    }

}
