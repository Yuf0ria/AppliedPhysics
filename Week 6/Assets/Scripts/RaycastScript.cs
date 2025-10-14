using UnityEngine;

public class RaycastScript : MonoBehaviour
{
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        //call physics, physics2D for 2D
        if(Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red); // Testing
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction, Color.green);
        }
    }
}
