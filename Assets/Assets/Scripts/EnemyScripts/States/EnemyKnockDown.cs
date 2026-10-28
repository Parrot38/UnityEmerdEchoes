using UnityEngine;

public class EnemyKnockDown: EnemyAbstactState
{
    EnemyController eController;
    public EnemyKnockDown(EnemyStateHandler handler, EnemyController enemyController) : base(handler)
    {
        eController = enemyController;
    }
    public override void Enter()
    {
        Debug.Log("Enemy:KnokedDown");
        eController.rb.linearVelocity = Vector3.zero;
        eController.isAttacking = false;

        if (eController.playerController.queueFirsElement == 7 && !eController.grounded)
        {
            eController.actionVelocity = -10f;
            eController.KnokOutDuration = 0.3f;
            eController.actionVelocityY = -6f;
        }
        else if (eController.playerController.queueFirsElement == 3 || eController.playerController.queueFirsElement == 7 )
        {
            eController.actionVelocity = -10f;
            eController.actionVelocityY = 3f;
            eController.KnokOutDuration = 0.3f;
        }


    }


    public override void Update()
    {
        if (eController.grounded && eController.KnokOutDuration <= 0)
        {
            stateHandler.SetState<EnemyIdle>();
        }

    }

    public override void FixedUpdate()
    {
        if (eController.KnokOutDuration >= 0)
        {
            eController.PerformKnokOutAction();
        }
    }
    public override void Exit()
    {

    }
}
