using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    //Class Variables
    public Camera mainCamera;
    private Vector2 turnCamera;
    public float sensitivity;

    GameObject playerShip;

    float zoom;

    // Start is called before the first frame update
    void Start()
    {
        playerShip = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        zoom = Input.mouseScrollDelta.y;
        transform.position = playerShip.transform.position;

        if (Input.GetMouseButton(0))
        {
            turnCamera.x += Input.GetAxis("Mouse X") * sensitivity;

            turnCamera.y += Input.GetAxis("Mouse Y") * sensitivity;

            turnCamera.y = Mathf.Clamp(turnCamera.y, -3.0f, 90.0f);


            transform.localRotation = Quaternion.Euler(turnCamera.y, turnCamera.x, 0);


            
            mainCamera.fieldOfView -= zoom;


            
        }

    }
}
