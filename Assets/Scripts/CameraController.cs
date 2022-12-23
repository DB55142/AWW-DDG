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
    public Camera gunCamera;
    public Camera ciwsCamera;
    public Camera radarCamera;
    private Vector2 turnCamera;
    public float sensitivity;

    PlayerController playerController;
    CIWSController ciwsController;

    GameObject playerShip;

    float zoom;

    // Start is called before the first frame update
    void Start()
    {
        playerController= GameObject.Find("Player").GetComponent<PlayerController>();
        ciwsController = GameObject.Find ("CIWSBody").GetComponent<CIWSController>();
        playerShip = GameObject.Find("Player");
        mainCamera.enabled = true;
        gunCamera.enabled = true;
        ciwsCamera.enabled = true;
        radarCamera.enabled = true;
        mainCamera.gameObject.SetActive(true);
        gunCamera.gameObject.SetActive(false);
        ciwsCamera.gameObject.SetActive(false);
        radarCamera.gameObject.SetActive(true);

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


        if (Input.GetKeyDown(KeyCode.F1))
        {
            mainCamera.gameObject.SetActive(true);
            gunCamera.gameObject.SetActive(false);
            ciwsCamera.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F2) && playerController.playerGunManual == true)
        {
            mainCamera.gameObject.SetActive(false);
            gunCamera.gameObject.SetActive(true);
            ciwsCamera.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            mainCamera.gameObject.SetActive(false);
            gunCamera.gameObject.SetActive(false);
            ciwsCamera.gameObject.SetActive(true);
        }
    }
}
