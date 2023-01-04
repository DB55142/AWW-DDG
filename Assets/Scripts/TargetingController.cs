using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingController : MonoBehaviour
{
    //Class Variables
    public Camera mainCamera;
    public GameObject targetLock;
    private GameObject targetShip;

    PlayerController playerController;

    public Vector3 targetCoords;
    private Vector3 direction;

    public GameObject point1;

    public GameObject point2;


    SpawnManager spawnManager;

    public float gunAutoRange;

    bool inRange = false;

    bool lockedOn = false;

    bool ownShip = true;

    float xAngle;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {

        Ray startPoint = mainCamera.ScreenPointToRay(Input.mousePosition);



        if (Physics.Raycast(startPoint, out RaycastHit target) && Input.GetMouseButtonDown(1))
        {
            if (target.collider.gameObject.tag == "Enemy")
            {
                Instantiate(targetLock,target.transform.position, targetLock.transform.rotation );
                targetCoords = target.transform.position;
                targetShip = target.collider.gameObject;
            }
        }

        if (targetShip != null)
        {
            if (Vector3.Distance(playerController.transform.position, target.transform.position) <= gunAutoRange)
            {
                inRange = true;
                Debug.Log("In range");
            }
        }

        if (playerController.playerGunManual == false && targetShip != null)
        {
            direction = targetCoords - playerController.playerGun.transform.position;
            Quaternion gunTrain = Quaternion.LookRotation(direction);
            Quaternion gunTargetFinal = new Quaternion(0, gunTrain.y, 0, playerController.playerGun.transform.rotation.w);
            Quaternion gunTargetFinalVertical = new Quaternion(gunTrain.x, gunTrain.y, 0, playerController.playerGunVertical.transform.rotation.w);
            playerController.playerGun.transform.rotation = Quaternion.Lerp(playerController.playerGun.transform.rotation, gunTargetFinal, 0.01f);
            playerController.playerGunVertical.transform.rotation = Quaternion.Lerp(playerController.playerGunVertical.transform.rotation, gunTargetFinalVertical, 0.01f);
            lockedOn = true;
        }

        xAngle = playerController.playerGun.transform.rotation.eulerAngles.x;



        if (playerController.playerGunManual == false || playerController.playerGunManual == true)
        {
            if (xAngle >= -30 && xAngle <= 30)
            {
                ownShip = true;
            }

            if (xAngle >= -214 && xAngle <= -146)
            {
                ownShip = true;
            }

            if (xAngle >= 140 && xAngle >= 214)
            {
                ownShip = true;
            }

            else
            {
                ownShip = false;
                //Debug.Log("safe to fire");
            }
        }

        if (inRange == true && lockedOn == true && playerController.playerGunManual == false && ownShip == false)
        {
            spawnManager.GunAutoFire();
        }
    }
}
