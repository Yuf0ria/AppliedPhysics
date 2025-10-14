using TMPro;
using UnityEngine;

public class RaycastScript : MonoBehaviour
{
    //serializefields
    [SerializeField] float distances;
    [SerializeField] LayerMask layerMask;
    [SerializeField] private Camera cam;
    [SerializeField] Color color;

    //Gameobjects
    public GameObject Distance, Left, Zoom;
    //Text
    public TMP_Text TotalDistance;
    //Camera Rotate
    public float speed = 5f;
    private float zoom,
                 zoomMultiplier = 4f,
                 minZoom = 2f,
                 maxZoom = 8f,
                 velocity = 0f,
                 smoothTime = 0.25f;

    //set rotation raycast to camera

    void Start()
    {
        //gameObject disabled, contains uninitialized text
        Distance.SetActive(false);

        //using orthographic mode in camera
        zoom = cam.orthographicSize;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        }
        //Inputs =  Done

        //Interact 
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
        //Scrollwheel = Done!

        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Ui enable, left click
            Interact();
            Distance.SetActive(true);
        }
        else
        {
            Distance.SetActive(false);
        }
        //left mouse Done!

    }

    public void Interact()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        //Appear at scope
        if (Physics.Raycast(ray, out hitInfo, distances, layerMask))
        {
            Debug.Log($"Object name {hitInfo.transform.name}");
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red); // Testing
            //Get totaldistance using raycast
            var Distance = Vector3.Cross(ray.direction, hitInfo.point + ray.origin).magnitude;
            TotalDistance.text = "Distance: " + Distance.ToString();
            //TotalDistance Fixed
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * distances, Color.blue);
            TotalDistance.text = "Not Yellow";
        }
    }
    

    //Initial lesson summary by sir maynard
    //call physics, physics2D for 2D

    /*        if (Physics.Raycast(ray, out hitInfo, distances, layerMask, QueryTriggerInteraction.Ignore))
            {
                Debug.Log($"Object name {hitInfo.transform.name}"); //or;
                MeshRenderer renderer = hitInfo.transform.GetComponent<MeshRenderer>();
                renderer.material.color = color; //pabigat raw
                Debug.DrawLine(ray.origin, hitInfo.point, Color.red); // Testing
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * distances, Color.blue);
            }*/

    //Project Settings -> Physics -> Settings -> GameObject -> Disable Query Hit Triggers -> Remove QueryTriggerInteraction.Ignore
}
