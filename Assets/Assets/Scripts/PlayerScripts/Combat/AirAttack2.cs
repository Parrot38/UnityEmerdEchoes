using UnityEngine;

public class AirAttack2 : AbstractState
{

    AdvancedPlayerController apc;
    public AirAttack2(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
    {
        apc = playerController;
    }

    public override void Enter()
    {
        apc.rb.gravityScale = 1f;
        apc.rb.linearVelocity = Vector2.zero;
        apc.attackDuration = 0.33f;
        apc.rb.linearVelocityY = 1f;
        apc.actionVelocity = 3f;
        apc.isAttacking = true;
        apc.canAttack = true;
        apc.attackIndex = 7;
    }

    public override void Update()
    {
        apc.NeutralDelayTimer();
        apc.AttackQueueEnter();
        if (apc.queueFirsElement == 7)
        {
            stateHandler.SetState<AirAttack3>();
        }


        else if (apc.jumpBuffer < 0)
        {
            apc.SetNeutralState();
        }
    }
    public override void FixedUpdate()
    {
        apc.PerformAttackAction();
    }
    public override void Exit()
    {
        apc.jumpBuffer = 0.1f;
        apc.rb.gravityScale = 6f;
        apc.isAttacking = false;
    }
}
