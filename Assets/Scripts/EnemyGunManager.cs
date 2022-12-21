using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemyGunManager : MonoBehaviour
{
    //Class Variables
    public float gunRange;

    PlayerController playerController;

    private float distance;

    public GameObject projectile;

    public GameObject firingPoint;

    private bool gunReady = false;

    private Vector3 offSet = new Vector3(0, 20, 0);

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("Engage", 2.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, playerController.gameObject.transform.position);

        if (distance <= gunRange)
        {
            Tracking();
        }
    }

    //Additional Functions
    private void Tracking()
    {
        Vector3 direction = playerController.transform.position - (transform.position - offSet);
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        gunReady = true;
        
    }

    private void Engage()
    {
        Instantiate(projectile, firingPoint.transform.position, transform.rotation);
    }
}
