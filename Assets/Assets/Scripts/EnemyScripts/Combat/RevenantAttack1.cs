
using UnityEngine;

public class RevenantAttack1: EnemyAbstactState
{
    EnemyController eController;
    public RevenantAttack1(EnemyStateHandler handler, EnemyController enemyController) : base(handler)
    {
        eController = enemyController;
    }

    public override void Enter()
    {
        Debug.Log("Enemy:Attack");
        eController.attackDuration = 0.5f;
        eController.actionVelocity = 1;
        eController.isAttacking = true;
        eController.attackIndex = 1;
        eController.hitBoxTrigger.enabled = false;
    }


    public override void Update()
    {
        if (eController.attackDuration<=0f)
        {
            stateHandler.SetState<EnemyIdle>();
            eController.hitBoxTrigger.enabled = false;
        }
    }

    public override void FixedUpdate()
    {
        eController.PerformAttackAction();
        eController.CalulateDistanceToPlayer();
        if (eController.attackDuration<=0.30f)
        {
            eController.hitBoxTrigger.enabled = true;
        }
        else if(eController.attackDuration<=0.15f)
        {
            eController.hitBoxTrigger.enabled = false;
        }
    }
    public override void Exit()
    {
        eController.isAttacking = false;
        eController.hitBoxTrigger.enabled = false;
        eController.canAttack = false;
        eController.queueFirsElement = 0;
    }


}
