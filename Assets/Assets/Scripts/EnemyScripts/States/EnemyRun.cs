using UnityEngine;

public class EnemyRun : EnemyAbstactState
{
    EnemyController eController;
    public EnemyRun(EnemyStateHandler handler, EnemyController enemyController) : base(handler)
    {
        eController = enemyController;
    }

    public override void Enter()
    {
        Debug.Log("Enemy:Run enter");
        eController.isAttacking = false;
        eController.attackIndex = 1;
    }


    public override void Update()
    {


    }

    public override void FixedUpdate()
    {
        if (eController.playerDetected)
        {
            eController.CalulateDistanceToPlayer();
            eController.SetFacigDirection();
            if (eController.distanceToPlayer>1f) {eController.Run();}
        }
        else
        {
            stateHandler.SetState<EnemyIdle>();
        }
        if (eController.distanceToPlayer != 0 && eController.distanceToPlayer < 1.5f && eController.playerDetected)
        {
            eController.AttackQueueEnter();
        }
        if (eController.queueFirsElement == 1)
        {
            stateHandler.SetState<RevenantAttack1>();
        }
    }
    public override void Exit()
    {

    }


}
