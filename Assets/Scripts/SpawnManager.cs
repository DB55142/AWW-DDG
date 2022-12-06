using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Class Variables
    public GameObject playerGunFiringPoint;
    public GameObject playerGunProjectile;
    public ParticleSystem playerGunGas;

    private bool gunReady = true;

    PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Keypad5) && gunReady == true)
        {
            Instantiate(playerGunProjectile, playerGunFiringPoint.transform.position, playerController.playerGunVertical.transform.rotation);
            Instantiate(playerGunGas, playerGunFiringPoint.transform.position, playerController.playerGunVertical.transform.rotation);
            gunReady = false;
            MakeGunReady();
        }
    }

    //Additonal Functions
    async void MakeGunReady()
    {
        await Task.Delay(2000);
        gunReady = true;
    }
}
