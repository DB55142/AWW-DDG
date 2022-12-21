using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class EnemyMissileManager : MonoBehaviour
{
    //Additional Variables
    public Rigidbody missileRb;

    public int speed;

    PlayerController playerController;

    Vector3 shipForce;

    bool missileArmed = false;

    public Vector3 startPos;

    private Transform localRocketTrans;

    public ParticleSystem missileTimeOutExplosion;

    private Vector3 offSet = new Vector3(0, 20, 0);

    // Start is called before the first frame update
    void Start()
    {
        localRocketTrans = GetComponent<Transform>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        shipForce = playerController.playerHullRigidBody.velocity;
        missileRb.AddForce(shipForce, ForceMode.Impulse);
        MissileClimb();
        TimeOut();

    }

    // Update is called once per frame
    void Update()
    {
        startPos = transform.position;

        if (missileArmed == true)
        {
            Vector3 direction = (playerController.transform.position + offSet) - localRocketTrans.position;
            Quaternion rotateMissile = Quaternion.LookRotation(direction);
            transform.rotation = rotateMissile;
            missileRb.AddRelativeForce(Vector3.forward * Time.deltaTime * 250, ForceMode.Impulse);
        }

    }

    //Additional Functions
    async void MissileClimb()
    {
        missileRb.AddRelativeForce(Vector3.forward * Time.deltaTime * speed, ForceMode.Impulse);

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




}
