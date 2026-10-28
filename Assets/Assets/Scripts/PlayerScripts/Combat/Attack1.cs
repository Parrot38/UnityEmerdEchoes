using UnityEngine;

public class Attack1 : AbstractState
{

    AdvancedPlayerController apc;
    public Attack1(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
    {
        apc = playerController;
    }

    public override void Enter()
    {
        apc.attackDuration = 0.35f;
        apc.actionVelocity = 0f;
        apc.isAttacking = true;
        apc.canAttack = true;
        apc.attackIndex = 2;
    }

    public override void FixedUpdate()
    {
        apc.PerformAttackAction();
    }

    public override void Update()
    {
        if (apc.attackDuration < 0.25f)
        {
            apc.actionVelocity = 3f;
        }
        apc.AttackQueueEnter();
        if (apc.queueFirsElement == 4)
        {
            stateHandler.SetState<LaunchAttack>();
        }
        else if (apc.queueFirsElement == 2)
        {
            stateHandler.SetState<Attack2>();
        }
        else if (apc.queueCount==0)
        {
            apc.SetNeutralState();
        }


    }
    public override void Exit()
    {
        apc.isAttacking = false;
    }
}
