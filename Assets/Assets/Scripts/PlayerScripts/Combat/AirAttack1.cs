using UnityEngine;

public class AirAttack1 : AbstractState
{

    AdvancedPlayerController apc;
    public AirAttack1(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
    {
        apc = playerController;
    }

    public override void Enter()
    {
        apc.rb.gravityScale = 1f;
        apc.rb.linearVelocity = Vector2.zero;
        apc.attackDuration = 0.3f;
        apc.actionVelocity = 1.5f;
        apc.isAttacking = true;
        apc.canAttack = true;
        apc.attackIndex = 6;
    }

    public override void FixedUpdate()
    {
        apc.PerformAttackAction();
    }

    public override void Update()
    {
        apc.NeutralDelayTimer();
        apc.AttackQueueEnter();
        if (apc.queueFirsElement == 6)
        {
            stateHandler.SetState<AirAttack2>();
        }

        else if (apc.jumpBuffer < 0 && apc.attackDuration <= 0)
        {
            apc.SetNeutralState();
        }
    }
    public override void Exit()
    {
        apc.jumpBuffer = 0.1f;
        apc.rb.gravityScale = 6f;
        apc.isAttacking = false;
    }
}
