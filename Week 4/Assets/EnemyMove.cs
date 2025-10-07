using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float min = 2f;
    public float max = 3f;
    //initialization
    void Start()
    {
        min = transform.position.x;
        max = transform.position.x + 3;

    }
    void Update()
    {

        transform.position = new Vector3(Mathf.PingPong(Time.time * 5, max - min) + min, transform.position.y, transform.position.z);

    }
}
