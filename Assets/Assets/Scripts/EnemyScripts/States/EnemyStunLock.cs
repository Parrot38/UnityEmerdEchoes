using UnityEngine;

public class EnemyStunLock : EnemyAbstactState
{
    EnemyController eController;
    public EnemyStunLock(EnemyStateHandler handler, EnemyController enemyController) : base(handler)
    {
        eController = enemyController;
    }

    public override void Enter()
    {
        Debug.Log("Enemy:Balance is out");
        eController.isAttacking = false;
        eController.stunlockDuration = 0.33f;
        eController.actionVelocity = -7;
        eController.stuned = false;
        eController.attackIndexQueue.Clear();
        eController.queueFirsElement = 0;
    }


    public override void Update()
    {
        if (eController.stunlockDuration <= 0)
        {
            stateHandler.SetState<EnemyIdle>();
            eController.attackIndexQueue.Clear();
            eController.queueFirsElement = 0;
        }
    }

    public override void FixedUpdate()
    {
        eController.PerformStunlockAction();

            eController.stuned = true; 
    }
    public override void Exit()
    {

    }
}
