using UnityEngine;

public class BlockState : AbstractState
{
        AdvancedPlayerController apc;
        public BlockState(StateHandler handler, AdvancedPlayerController playerController) : base(handler)
        {
            apc = playerController;
        }

        public override void Enter()
        {
            Debug.Log("BlockEnter");
            apc.rb.gravityScale = 6f;
            apc.blockDuration = 0.5f;
            apc.attackIndex = 1;
            apc.canAttack = false;
            apc.rb.linearVelocity = Vector2.zero;
        }

        public override void FixedUpdate()
        {
            apc.blockDuration -=Time.fixedDeltaTime;
            if (apc.blockDuration<0.45f)
            {
                apc.isBlocking = true;
            }
        }

        public override void Update()
        {
            if (apc.blockDuration <= 0.25f && apc.inputAxisX != 0 && apc.specialWasPressed)
            {
                stateHandler.SetState<DashState>();
            }

            else if (apc.blockDuration <= 0.25f && apc.attackWasPressed)
            {
                stateHandler.SetState<IdleState>();
                apc.AttackQueueEnter();
            }
            else if (apc.blockDuration <= 0f)
            {
                apc.SetNeutralState();
            }
        }

        public override void Exit()
        {

        }
}
