using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject Hero;
    public Transform heroTransform;
    public float positionY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        heroTransform = Hero.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(heroTransform.position.x, positionY ,0f);
    }
}
