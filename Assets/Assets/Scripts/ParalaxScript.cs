using UnityEngine;

public class ParalaxScript : MonoBehaviour
{
    [Range(0,1f)] public float paralaxSpeed = 0f;

    public float paralaxValue;
    public float positionY;
    [SerializeField] private Transform cameraPosition;

    [SerializeField] private Rigidbody2D playerRB;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ParalaxSpeedValue();
        transform.position = new Vector3(cameraPosition.position.x+paralaxValue, positionY, 0f);
    }
    private void ParalaxSpeedValue()
    {
        paralaxValue -= playerRB.linearVelocityX * paralaxSpeed * Time.deltaTime;
    }
}
