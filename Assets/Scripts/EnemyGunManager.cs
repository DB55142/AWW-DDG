using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class EnemyGunManager : MonoBehaviour
{
    //Class Variables
    public float gunRange;
    private float distance;

    PlayerController playerController;

    public GameObject projectile;
    public GameObject firingPoint;

    private bool gunReady = false;

    private Vector3 offSet = new Vector3(0, 15, 0);

    public AudioSource enemyGunFireSound;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("Engage", 2.0f, StartMenuController.gunFiringRate);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    //Additional Functions
    private void CheckDistance()
    {
        distance = Vector3.Distance(transform.position, playerController.gameObject.transform.position);

        if (distance <= gunRange)
        {
            Tracking();
        }

        if (distance > gunRange)
        {
            gunReady = false;
        }
    }

    private void Tracking()
    {
        Vector3 direction = playerController.transform.position - (transform.position - offSet);
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        gunReady = true;
    }

    private void Engage()
    {
        if (gunReady)
        {
            Instantiate(projectile, firingPoint.transform.position, transform.rotation);
            enemyGunFireSound.Play();
        }
    }
}
