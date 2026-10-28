using Unity.VisualScripting;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    [SerializeField]private AdvancedPlayerController playerController;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform enemyTransform;
    private bool playerOnRightSide;
    private void Update()
    {
        if (playerTransform.position.x > enemyTransform.position.x) playerOnRightSide = true;
        else playerOnRightSide = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBodyCollider") && playerController.grounded)
        {
            if (playerOnRightSide) playerController.SetFacingDirection(1f);
            else playerController.SetFacingDirection(-1f);
            playerController.SetStunlockState();
            playerController.health -= 1;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBodyCollider") && !playerController.grounded)
        {
            if (playerOnRightSide) playerController.SetFacingDirection(1f);
            else playerController.SetFacingDirection(-1f);
            playerController.SetKnockDownState();
            playerController.health -= 1;
        }
    }
}
