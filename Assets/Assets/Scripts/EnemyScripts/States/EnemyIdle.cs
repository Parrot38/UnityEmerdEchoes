using UnityEngine;

public class EnemyIdle : EnemyAbstactState
{
    EnemyController eController;
    public EnemyIdle(EnemyStateHandler handler, EnemyController enemyController) : base(handler)
    {
        eController = enemyController;
    }

    public override void Enter()
    {
        Debug.Log("Enemy:Idle enter");
        eController.SetFacigDirection();
        eController.isAttacking = false;
        eController.stuned = false;
        eController.hitBoxTrigger.enabled = false;
        eController.canAttack = false;
    }


    public override void Update()
    {

    }

    public override void FixedUpdate()
    {
        if (eController.playerDetected && eController.canAttack)
        {
            eController.SetFacigDirection();
            stateHandler.SetState<EnemyRun>();
        }    
    }
    public override void Exit()
    {

    }


}
