using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //SpawnObject
    public GameObject gameobject;
    public Vector3 origin = Vector3.zero;
    //radius for collission
    public float radius = 5;
    public float movespeed = 1.0f;//pang move towards na lng.
    [SerializeField] Transform target;

    void Update()
    {
        //each second ay walk ng object
        var step = movespeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        //detect
        DetectTower(transform.position, radius);

    }

    void DetectTower(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Tower"))
                {
                    Destroy(gameobject);
                }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
