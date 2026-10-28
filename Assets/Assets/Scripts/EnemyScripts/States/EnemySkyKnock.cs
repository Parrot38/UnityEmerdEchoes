using UnityEngine;

public class EnemySkyKnock : EnemyAbstactState
{
    EnemyController eController;
    public EnemySkyKnock(EnemyStateHandler handler, EnemyController enemyController) : base(handler)
    {
        eController = enemyController;
    }

    public override void Enter()
    {
        Debug.Log("Enemy:KnokedOut");
        eController.rb.linearVelocity = Vector3.zero;
        eController.isAttacking = false;
        
        if (eController.playerController.queueFirsElement==4)
        {
            eController.actionVelocityY = 8f;
            eController.KnokOutDuration = 0.3f;
        }
        else
        {
            eController.actionVelocityY = 4f;
            eController.KnokOutDuration = 0.18f;
        }
        
        eController.actionVelocity = -2f;
    }


    public override void Update()
    {
        if (eController.grounded && eController.KnokOutDuration<=0)
        {
            stateHandler.SetState<EnemyIdle>();
        }

    }

    public override void FixedUpdate()
    {
        if (eController.KnokOutDuration>=0)
        {
            eController.PerformKnokOutAction();
        }
    }
    public override void Exit()
    {

    }


}
