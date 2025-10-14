using TMPro;
using UnityEngine;

public class RaycastScript : MonoBehaviour
{
    [SerializeField] float distances;
    [SerializeField] LayerMask layerMask;
    [SerializeField] private Camera cam;
    [SerializeField] Color color;

    //Gameobjects
    public GameObject Distance, Left, Zoom, Object, origin;
    //Text
    public TMP_Text TotalDistance;
    //Camera Rotate
    public float speed = 5f;
    private float zoom;
    private float zoomMultiplier = 4f;
    private float minZoom = 2f;
    private float maxZoom = 8f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;
    //set rotation raycast to camera

    void Start()
    {
        //gameObject disabled
        Distance.SetActive(false);

        //camera get
        zoom = cam.orthographicSize;
    }
    void Update()
    {
        //Keyinput A & D, Gameobject rotates in Y-Axis
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

        //Interact UI Enable, lectclick
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
            Vector2 Distance1 = new Vector2(Object.transform.position.x,Object.transform.position.y);
            Vector2 Distance2 = new Vector2(origin.transform.position.x, origin.transform.position.y);
            var Distance = (Distance1, Distance2);
            TotalDistance.text = "Distance: " + Distance.ToString();
            
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * distances, Color.blue);
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
    //keycode E for interacting, another script to test
}
