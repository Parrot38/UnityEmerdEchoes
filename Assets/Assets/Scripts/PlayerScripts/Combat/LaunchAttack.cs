using UnityEngine;

public class LaunchAttack: AbstractState
{

    AdvancedPlayerController apc;
    public LaunchAttack(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
    {
        apc = playerController;
    }
    public override void Enter()
    {
        apc.attackDuration = 0.4f;
        apc.actionVelocity = 2f;
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
        apc.AttackQueueEnter();
        if (apc.queueFirsElement == 1)
        {
            stateHandler.SetState<Attack1>();
        }
        apc.SetNeutralState();

    }
    public override void Exit()
    {
        apc.isAttacking = false;
    }
}