using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheckScrips : MonoBehaviour
{
    private bool _grounded;

    public bool _Grounded { get { return _grounded; } } 


    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            _grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _grounded = false;
    }
}
