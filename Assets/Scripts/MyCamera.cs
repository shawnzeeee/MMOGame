using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public float speed = 75.0f; // Speed of camera movement
    public float zoomSpeed = 150.0f; // Speed of camera zoom
    public float borderThickness = 10.0f; // Thickness of screen edge for camera movement

    public GameObject farmPrefab; // Set this in the Unity editor
    public GameObject workerPrefab; // Set this in the Unity editor         
    public float minZoom = 2f;
    public float maxZoom = 10f;

    private GameObject selectedObject;

    public GameObject buildingMenuObject;
    private bool buildingMenuActive = false;
    public void triggerMenuSelection(GameObject obj){
        selectedObject = obj;
    }
    // Update is called once per frame
    void Update()
    {

        // Move camera based on mouse position
    Vector3 pos = transform.position;

    // Move camera based on mouse position
    if (Input.mousePosition.y >= Screen.height - borderThickness)
    {
        pos.y += speed * Time.deltaTime; // Change this line
    }
    if (Input.mousePosition.y <= borderThickness)
    {
        pos.y -= speed * Time.deltaTime; // Change this line
    }
    if (Input.mousePosition.x >= Screen.width - borderThickness)
    {
        pos.x += speed * Time.deltaTime;
    }
    if (Input.mousePosition.x <= borderThickness)
    {
        pos.x -= speed * Time.deltaTime;
    }

    // Rest of your code...

    // Update camera position
    transform.position = pos;
    if (Input.GetKeyDown(KeyCode.B))
    {
        //BuildingMenu buildingMenu = buildingMenuObject.GetComponent<BuildingMenu>();
        //Debug.Log(buildingMenu);
        //buildingMenu.toggleActive(); // Toggle the popup menu
        buildingMenuActive = !buildingMenuActive;
    }
    
    if (Input.GetMouseButtonDown(0))
    {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10; // Set this to be the distance you want the object to be placed in front of the camera
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Place the object at the mouse position
            if(selectedObject != null && !buildingMenuActive){
                GameObject instance = Instantiate(selectedObject, worldPosition, Quaternion.identity);
                instance.SetActive(true);
                Debug.Log(instance.activeSelf);
            }
    }
    if (Input.GetKeyDown(KeyCode.Q))
    {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10; // Set this to be the distance you want the object to be placed in front of the camera
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Place the object at the mouse position
            GameObject instance = Instantiate(farmPrefab, worldPosition, Quaternion.identity);
            instance.SetActive(true);
    }

    // Spawn Worker GameObject when 'w' is pressed
    if (Input.GetKeyDown(KeyCode.W))
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Instantiate(workerPrefab, hit.point, Quaternion.identity);
        }
    }

        // Zoom camera based on scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= scroll * zoomSpeed * Time.deltaTime;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        // Update camera position
        transform.position = pos;
    }
}