using UnityEngine;

public class JumpState : AbstractState
{
    AdvancedPlayerController apc;
    public JumpState(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
    {
        apc = playerController;
    }

    public override void Enter()
    {

        apc.jumpBuffer = 0.1f;

        apc.rb.linearVelocityY = 6f;

        switch(apc.inputAxisX)
        {
            case 1:
                apc.rb.linearVelocity = new Vector2(6f, 14f);
                break;
            case -1:
                apc.rb.linearVelocity = new Vector2(-6f, 14f);
                break;
            case 0:
                apc.rb.linearVelocity = new Vector2(0f, 14f);
                break;
        }
        apc.attackIndex = 5;
    }


    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        apc.AttackQueueEnter();
        if (apc.queueFirsElement == 5)
        {
            stateHandler.SetState<AirAttack1>();
        }
        apc.NeutralDelayTimer();
        if(apc.jumpBuffer<0 && apc.grounded)
        {
            apc.SetNeutralState(); 
        }
    }
    public override void Exit()
    {
        apc.jumpBuffer = 0.1f;
    }

}
