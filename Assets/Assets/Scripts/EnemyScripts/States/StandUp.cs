using UnityEngine;

public class StandUp : EnemyAbstactState
{
    EnemyController eController;
    public StandUp(EnemyStateHandler handler, EnemyController enemyController) : base(handler)
    {
        eController = enemyController;
    }

    public override void Enter()
    {
        Debug.Log("Enemy:Idle enter");
        eController.animator.Play("");
        eController.isAttacking = false;
        eController.stuned = true;
        eController.hitBoxTrigger.enabled = false;
        eController.canAttack = false;
        eController.standUpDuration = 0.5f;
        eController.animator.Play("StandUp");
    }


    public override void Update()
    {

    }

    public override void FixedUpdate()
    {
        eController.PerformStandUpAction();
        if(eController.standUpDuration<=0)
            stateHandler.SetState<EnemyIdle>(); 
    }
    public override void Exit()
    {

    }


}
