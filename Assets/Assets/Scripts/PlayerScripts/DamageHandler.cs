
using UnityEngine;

public class DamageHandler:MonoBehaviour
{
    private Rigidbody2D rb;
    private AdvancedPlayerController apc;
    public float parryWindow;
    public bool gotHit;
    public bool parryConfirmed = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        apc = GetComponent<AdvancedPlayerController>();
    }
    private void FixedUpdate()
    {

    }

}
