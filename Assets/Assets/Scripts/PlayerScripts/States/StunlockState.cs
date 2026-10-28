using UnityEngine;

public class StunlockState : AbstractState
{
    AdvancedPlayerController apc;

    public StunlockState(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
    {
        apc = playerController;
    }

    public override void Enter()
    {
        apc.ClearFields();
        apc.stunlockDuration = 0.4f;
        apc.actionVelocity = 4f;
        apc.stunLock = true;
        Debug.Log("Stun");
    }

    public override void Exit()
    {
        apc.stunLock = false;
    }

    public override void FixedUpdate()
    {
        apc.PerformStunlockAction();
        if (apc.stunlockDuration <= 0)
        {
            stateHandler.SetState<IdleState>();
        }
    }

    public override void Update()
    {
        
    }
}
