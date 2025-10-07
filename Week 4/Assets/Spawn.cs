using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform[] points; //list na lng para random spawn
    int i;
    //interval of spawn
    [SerializeField] float timer = 0f;
    private float delay = 3f;
    

    void Start()
    {
        SpawnObject();
    }

    void Update()
    {
        //update delay of the points
        if (Time.time > timer)
        {
            SpawnObject();
            timer = Time.time + delay;
        }
    }

    public void SpawnObject()
    {
        //random point to spawn
        int spawnIndex = Random.Range(0, points.Length);
        for (i = 0; i < 5; i++)
        {
            //each points will spawn up to 5 cubes.
            //Set delay for spawn
            timer += Time.deltaTime;
            if (timer >= 12f )
            {
                Instantiate(prefab, points[spawnIndex].position, points[spawnIndex].rotation);
                timer = 0f; //resets the timer  back to zero
            }
        }
    }
}
