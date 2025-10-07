using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{
    private float speed = 0.1f;
    public GameObject object1;
    public List<GameObject> objects1 = new List<GameObject>();
    private Vector3 pos;
    float radius = 12;

    void Start()
    {
        pos = transform.position;
        objects1 = GameObject.FindGameObjectsWithTag("object").ToList();
        FindTarget();
    }

    /*void Update()
    {
        if (object1 != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, object1.transform.position, speed);
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("object"))
        {
            //destroy later
        }
    }

    void FindTarget()
    {

        float lowestDist = Mathf.Infinity;

        for (int i = 0; i < objects1.Count; i++)
        {

            float dist = Vector3.Distance(objects1[i].transform.position, pos);

            if (dist < lowestDist)
            {
                lowestDist = dist;
                object1 = objects1[i];
            }

        }
    }
}
