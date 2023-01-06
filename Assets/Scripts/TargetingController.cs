using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingController : MonoBehaviour
{
    //Class Variables
    public Camera mainCamera;

    public GameObject targetLock;
    public GameObject targetShip;

    PlayerController playerController;

    private Vector3 direction;

    SpawnManager spawnManager;

    public float gunAutoRange;

    bool inRange = false;
    bool lockedOn = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        AcquireTarget();

        VerifyRange();

        TrainGun();

        FireGun();
    }

    //Additional Functions
    private void AcquireTarget()
    {
        Ray startPoint = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(startPoint, out RaycastHit target) && Input.GetMouseButtonDown(1))
        {
            if (target.collider.gameObject.tag == "Enemy" && target.collider.gameObject.GetComponent<EnemyFrigateManager>().targeted == false)
            {
                Instantiate(targetLock, target.transform.position, targetLock.transform.rotation);
                targetShip = target.collider.gameObject;
                targetShip.GetComponent<EnemyFrigateManager>().targeted = true;
            }
        }
    }

    private void VerifyRange()
    {
        if (targetShip != null)
        {
            if (Vector3.Distance(playerController.transform.position, targetShip.transform.position) <= gunAutoRange)
            {
                inRange = true;
            }
        }
    }

    private void TrainGun()
    {
        if (playerController.playerGunManual == false && targetShip != null)
        {
            direction = (targetShip.transform.position) - playerController.playerGun.transform.position;
            Quaternion gunTrain = Quaternion.LookRotation(direction);
            Quaternion gunTargetFinal = new Quaternion(0, gunTrain.y, 0, playerController.playerGun.transform.rotation.w);
            Quaternion gunTargetFinalVertical = new Quaternion(gunTrain.x, gunTrain.y, 0, playerController.playerGunVertical.transform.rotation.w);
            playerController.playerGun.transform.rotation = gunTargetFinal;
            playerController.playerGunVertical.transform.rotation = gunTargetFinalVertical;
            lockedOn = true;
        }
    }

    private void FireGun()
    {
        if (inRange == true && lockedOn == true && playerController.playerGunManual == false)
        {
            spawnManager.GunAutoFire();
        }
    }
}
