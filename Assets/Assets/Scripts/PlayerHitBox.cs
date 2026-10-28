using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    public GameObject playerHitBox;
    public BoxCollider2D boxCollider;

    public BoxCollider2D playerCollider;
    public GameObject playerGuardbBox;
    public BoxCollider2D guardCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = playerHitBox.GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        guardCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ColliderOn()
    {
        boxCollider.enabled = true;
    }
    public void ColliderOff()
    {
        boxCollider.enabled = false;
    }
    public void GuardOn()
    {
        playerCollider.enabled = false;
        guardCollider.enabled = true;
    }
    public void GuardOff()
    {
        playerCollider.enabled = true;
        guardCollider.enabled = false;
    }
}
