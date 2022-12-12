using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    //Additional Variables
    public Rigidbody missileRb;

    public int speed;

    PlayerController playerController;

    TargetingController targetingController;

    Vector3 shipForce;

    bool missileArmed = false;

    public Vector3 startPos;

    public Vector3 missileTarget;

    private Transform localRocketTrans;

    // Start is called before the first frame update
    void Start()
    {
        localRocketTrans = GetComponent<Transform>(); 
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        targetingController = GameObject.Find("PlayerCameraCentre").GetComponent<TargetingController>();
        shipForce = playerController.playerHullRigidBody.velocity;
        missileRb.AddForce(shipForce, ForceMode.Impulse);
        MissileClimb();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        startPos = transform.position;

        if (missileArmed == true)
        {
            Vector3 direction = targetingController.targetCoords - localRocketTrans.position;
            Quaternion rotateMissile = Quaternion.LookRotation(direction);
            Quaternion rotateMissileFinal = Quaternion.Euler(targetingController.targetCoords - transform.position);
            transform.rotation = Quaternion.LookRotation(targetingController.targetCoords - localRocketTrans.position);
            missileRb.AddRelativeForce(Vector3.forward * Time.deltaTime * 250, ForceMode.Impulse);
            Debug.Log("Fire!");
        }

    }

    //Additional Functions
    async void MissileClimb()
    {
        missileRb.AddForce(Vector3.up * Time.deltaTime * speed, ForceMode.Impulse);

        await Task.Delay(800);
        missileArmed = true;
        
    }



}
