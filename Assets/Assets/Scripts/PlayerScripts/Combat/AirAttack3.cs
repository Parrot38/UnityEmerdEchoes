using UnityEngine;

public class AirAttack3 : AbstractState
{

    AdvancedPlayerController apc;
    public AirAttack3(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
    {
        apc = playerController;
    }

    public override void Enter()
    {
        apc.rb.gravityScale = 1f;
        apc.rb.linearVelocity = Vector2.zero;
        apc.attackDuration = 0.35f;
        apc.rb.linearVelocityY = 2f;
        apc.actionVelocity = 4f;
        apc.isAttacking = true;
        apc.canAttack = true;
        apc.attackIndex = 5;
    }

    public override void Update()
    {
        apc.NeutralDelayTimer();
        apc.AttackQueueEnter();
        if (apc.queueFirsElement == 1)
        {
            stateHandler.SetState<AirAttack1>();
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
