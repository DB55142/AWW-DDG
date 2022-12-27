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


    SpawnManager spawnManager;



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

            if (target.collider.gameObject.tag == "EnemyShipMarker")
            {
                Debug.Log("target");
            }
        }

        if (playerController.playerGunManual == false && targetShip != null)
        {
            direction = targetCoords - playerController.playerGun.transform.position;
            Quaternion gunTrain = Quaternion.LookRotation(direction);
            Quaternion gunTargetFinal = new Quaternion(0, gunTrain.y, 0, playerController.playerGun.transform.rotation.w);
            Quaternion gunTargetFinalVertical = new Quaternion(gunTrain.x, gunTrain.y, 0, playerController.playerGunVertical.transform.rotation.w);
            Quaternion startRot = new Quaternion(0, 0, 0, 0);
            playerController.playerGun.transform.rotation = Quaternion.Lerp(startRot, gunTargetFinal, 0.01f);
            playerController.playerGunVertical.transform.rotation = Quaternion.Lerp(startRot, gunTargetFinalVertical, 0.01f);
            spawnManager.GunAutoFire();
        }




    }
}
