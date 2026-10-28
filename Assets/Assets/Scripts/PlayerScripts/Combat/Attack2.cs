using UnityEngine;

public class Attack2 : AbstractState
{
    AdvancedPlayerController apc;
    public Attack2(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
    {
        apc = playerController;
    }
    public override void Enter()
    {
        apc.attackDuration = 0.4f;
        apc.actionVelocity = 1f;
        apc.isAttacking = true;
        apc.canAttack = true;
        apc.attackIndex = 3;
    }

    public override void FixedUpdate()
    {
        apc.PerformAttackAction();
    }

    public override void Update()
    {
        if (apc.attackDuration < 0.33f)
        {
            apc.actionVelocity = 4f;
        }
        apc.AttackQueueEnter();
        if (apc.queueFirsElement == 3)
        {
            stateHandler.SetState<Attack3>();
        }
        if (apc.queueFirsElement == 4)
        {
            stateHandler.SetState<LaunchAttack>();
        }
        apc.SetNeutralState();

    }
    public override void Exit()
    {
        apc.isAttacking = false;
    }
}
