using UnityEngine;

public class DashState : AbstractState
{
    AdvancedPlayerController advancedPlayerController;

    public DashState(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
    {
        advancedPlayerController = playerController;
    }
    public override void Enter()
    {
        Debug.Log("Dash Enter");
        advancedPlayerController.ClearFields();

        advancedPlayerController.facingDirection = advancedPlayerController.inputAxisX;
        advancedPlayerController.rb.gravityScale = 0f;
        advancedPlayerController.isDashing = true;
        advancedPlayerController.dashDuration = 0.5f;
        advancedPlayerController.actionVelocity = 7.5f;
    }
    public override void Update()
    {

    }
    public override void FixedUpdate()
    {
        advancedPlayerController.PerformDashAction();
        if (advancedPlayerController.dashDuration <= 0.25f)
        {
            advancedPlayerController.actionVelocity = 3f;
        }
        if (advancedPlayerController.dashDuration <= 0f)
        {
            advancedPlayerController.SetNeutralState();
        }

    }

    public override void Exit()
    {
        advancedPlayerController.rb.gravityScale = 6f;
        advancedPlayerController.isDashing = false;
        Debug.Log("Dash Exit");
    }
}
