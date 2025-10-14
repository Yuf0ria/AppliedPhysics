using TMPro;
using UnityEngine;

public class RaycastScript : MonoBehaviour
{
    [SerializeField] float distances;
    [SerializeField] LayerMask layerMask;
    [SerializeField] private Camera cam;
    [SerializeField] Color color;

    //Gameobjects
    public GameObject Distance, Left, Zoom;
    //Text
    public TMP_Text TotalDistance;
    //Camera Rotate
    public float rotateSpeed = 5f;
    private Quaternion targetRotation;
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

        //rotation initialization
        targetRotation = transform.rotation;
    }
    void Update()
    {
        //Keyinput A & D, Gameobject rotates in Y-Axis
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetRotation = Quaternion.Euler(0, -90, 0);
            
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            targetRotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            //targetRotation = Quaternion.Euler(0, 0, 0);
        }
        
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
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

    }

    public void Interact()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        //Appear at scope
        if (Physics.Raycast(ray, out hitInfo, distances, layerMask, QueryTriggerInteraction.Ignore))
        {
            Debug.Log($"Object name {hitInfo.transform.name}");
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red); // Testing
            TotalDistance.text = "Total Distance: " + hitInfo.transform.name;
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
