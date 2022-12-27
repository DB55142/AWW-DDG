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
    public GameObject[] enemyShipSpawnPoints = new GameObject[4];
    public GameObject enemyShip;

    private bool gunReady = true;

    PlayerController playerController;

    public GameObject enemySpawnPt;
    public GameObject enemyMissile;

    public int numbOfOpponents;

    public int numbOfEnemyShipsDestroyed;



    // Start is called before the first frame update
    void Start()
    {
        
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnEnemyShip(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Keypad5) && gunReady == true && playerController.playerGunManual)
        {
            Instantiate(playerGunProjectile, playerGunFiringPoint.transform.position, playerController.playerGunVertical.transform.rotation);
            Instantiate(playerGunGas, playerGunFiringPoint.transform.position, playerController.playerGunVertical.transform.rotation);
            gunReady = false;
            MakeGunReady();
        }

        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            Instantiate(enemyMissile, enemySpawnPt.transform.position, enemyMissile.transform.rotation);
        }

        if (numbOfEnemyShipsDestroyed == numbOfOpponents)
        {
            playerController.gameOver = true;
        }
    }

    //Additonal Functions
    async void MakeGunReady()
    {
        await Task.Delay(2000);
        gunReady = true;
    }

    public void GunAutoFire()
    {
        if (gunReady)
        {
            Instantiate(playerGunProjectile, playerGunFiringPoint.transform.position, playerController.playerGunVertical.transform.rotation);
            Instantiate(playerGunGas, playerGunFiringPoint.transform.position, playerController.playerGunVertical.transform.rotation);
            gunReady = false;
            MakeGunReady();
        }
    }

    public void SpawnEnemyShip()
    {
        if (numbOfEnemyShipsDestroyed < numbOfOpponents)
        {
            int randomPoint = Random.Range(1, 4);

            Instantiate(enemyShip, enemyShipSpawnPoints[randomPoint].transform.position, enemyShip.transform.rotation);
        }
        
    }
}
