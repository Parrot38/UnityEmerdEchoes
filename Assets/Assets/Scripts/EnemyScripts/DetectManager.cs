using UnityEngine;

public class DetectManager : MonoBehaviour
{
    public EnemyController enemyController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.layer == LayerMask.NameToLayer("PlayerBodyCollider"))
        {
            enemyController.playerDetected = true;
            enemyController.AddInList();
            Debug.Log("Игрок вошёл");
        }
    }
    void OnTriggerExit2D(Collider2D player)
    {
        enemyController.RemoveFromList();
        Debug.Log("Игрок вышел");
    }
}
