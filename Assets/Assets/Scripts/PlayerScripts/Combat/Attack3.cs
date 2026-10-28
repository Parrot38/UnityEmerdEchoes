using UnityEngine;

public class Attack3 : AbstractState
{

    AdvancedPlayerController apc;
    public Attack3(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
    {
        apc = playerController;
    }

    public override void Enter()
    {
        apc.attackDuration = 0.55f;
        apc.actionVelocity = 1f;
        apc.isAttacking = true;
        apc.canAttack = true;
        apc.attackIndex = 1;
    }

    public override void FixedUpdate()
    {
        apc.PerformAttackAction();
    }

    public override void Update()
    {
        if (apc.attackDuration < 0.48f)
        {
            apc.actionVelocity = 8f;
        }

        if (apc.attackDuration < 0.2f)
        {
            apc.actionVelocity = 3f;
        }
        apc.AttackQueueEnter();
        if (apc.queueFirsElement == 1)
        {
            stateHandler.SetState<Attack1>();
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
