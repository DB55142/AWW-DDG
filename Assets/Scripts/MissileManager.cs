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

    public ParticleSystem missileTimeOutExplosion;

    private Vector3 offSet = new Vector3(0, 20, 0);

    // Start is called before the first frame update
    void Start()
    {
        localRocketTrans = GetComponent<Transform>(); 
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        targetingController = GameObject.Find("Spawn Manager").GetComponent<TargetingController>();
        shipForce = playerController.playerHullRigidBody.velocity;
        missileRb.AddForce(shipForce, ForceMode.Impulse);
        MissileClimb();
        TimeOut();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        startPos = transform.position;

        if (missileArmed == true)
        {
            Vector3 direction = (targetingController.targetCoords + offSet) - localRocketTrans.position;
            Quaternion rotateMissile = Quaternion.LookRotation(direction);
            Quaternion rotateMissileFinal = Quaternion.Euler(targetingController.targetCoords - transform.position);
            transform.rotation = Quaternion.LookRotation(targetingController.targetCoords - localRocketTrans.position);
            missileRb.AddRelativeForce(Vector3.forward * Time.deltaTime * 250, ForceMode.Impulse);
        }

    }

    //Additional Functions
    async void MissileClimb()
    {
        missileRb.AddForce(Vector3.up * Time.deltaTime * speed, ForceMode.Impulse);

        await Task.Delay(800);
        missileArmed = true;
        
    }

    async void TimeOut()
    {
        await Task.Delay(18250);
        Vector3 position = transform.position;
        Instantiate(missileTimeOutExplosion, position, missileTimeOutExplosion.transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyMissile")
        {
            Vector3 position = transform.position;
            Instantiate(missileTimeOutExplosion, position, missileTimeOutExplosion.transform.rotation);
        }
    }


}
