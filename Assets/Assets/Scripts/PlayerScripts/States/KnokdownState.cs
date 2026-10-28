using UnityEngine;

public class KnokdownState : AbstractState
{
    AdvancedPlayerController apc;
    public KnokdownState(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
    {
        apc = playerController;
    }

    public override void Enter()
    {
        apc.isDashing = false;
        apc.isBlocking = false;
        apc.canLaunch = false;
        apc.canAttack = false;
        apc.knokedDown = true;
        apc.knokDownDuration = 0.5f;

        apc.attackDuration = 0;
        apc.queueCount = 0;
        apc.queueFirsElement = 0;
        apc.attackIndexQueue.Clear();

        apc.rb.linearVelocity = Vector2.zero;
        apc.rb.linearVelocityX = (apc.facingDirection * 6f)* -1f;
    }

    public override void FixedUpdate()
    {
        apc.knokDownDuration -= Time.fixedDeltaTime;
    }

    public override void Update()
    {

        if (apc.grounded)
        {
            apc.rb.linearVelocityX = (4f * apc.facingDirection) * -1f;
        }
        if (apc.knokDownDuration<=0)
        {
            apc.playerCollider.enabled = true;
            stateHandler.SetState<IdleState>();
        }
    }

    public override void Exit()
    {

    }
}
