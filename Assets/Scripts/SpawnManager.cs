using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Class Variables
    public GameObject playerGunFiringPoint;
    public AudioSource playerGunFireSound;

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

    int shipsRemaining;

    int lastPointUsed;

    public TextMeshProUGUI shipsLeft;

    Quaternion gunRotationStart;

    TargetingController targetingController;

    public float gunRange;


    // Start is called before the first frame update
    void Start()
    {
        GetEnemyForceSize();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnEnemyShip();

        targetingController = GameObject.Find("Spawn Manager").GetComponent<TargetingController>();
    }

    // Update is called once per frame
    void Update()
    {
        shipsRemaining = numbOfOpponents - numbOfEnemyShipsDestroyed;

        shipsLeft.text = "SHIPS REMAINING: " + shipsRemaining;

        if (shipsRemaining <= 0)
        {
            shipsLeft.color = Color.green;
        }

        if (Input.GetKeyUp(KeyCode.Keypad5) && gunReady == true && playerController.playerGunManual)
        {
            Instantiate(playerGunProjectile, playerGunFiringPoint.transform.position, playerController.playerGunVertical.transform.rotation);
            playerGunFireSound.Play();
            Instantiate(playerGunGas, playerGunFiringPoint.transform.position, playerController.playerGunVertical.transform.rotation);
            gunReady = false;
            MakeGunReady();
        }

        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            Instantiate(enemyMissile, enemySpawnPt.transform.position, enemyMissile.transform.rotation);
        }

        if (numbOfEnemyShipsDestroyed >= numbOfOpponents)
        {
            playerController.gameOverText.text = "VICTORY!";
            playerController.gameOverText.color = Color.green;
            playerController.gameOver = true;
        }

        gunRotationStart = new Quaternion (playerController.playerGun.transform.rotation.x,playerController.playerGunVertical.transform.rotation.y, playerController.playerGun.transform.rotation.z, playerController.playerGun.transform.rotation.w );
    }

    //Additional Classes


    //Additonal Functions
    async void MakeGunReady()
    {
        await Task.Delay(2000);
        gunReady = true;
    }

    public void GunAutoFire()
    {
        if (gunReady && targetingController.targetShip != null && Vector3.Distance(playerController.transform.position, targetingController.targetShip.transform.position) <= gunRange && playerController.gameOver == false)
        {
            Instantiate(playerGunProjectile, playerGunFiringPoint.transform.position, gunRotationStart);
            Instantiate(playerGunGas, playerGunFiringPoint.transform.position, playerController.playerGunVertical.transform.rotation);
            playerGunFireSound.Play();
            gunReady = false;
            MakeGunReady();
        }
    }

    public void SpawnEnemyShip()
    {
        if (numbOfEnemyShipsDestroyed < numbOfOpponents)
        {
            int randomPoint = Random.Range(1, 4);

            while (lastPointUsed == randomPoint)
            {
                randomPoint = Random.Range(1, 4);
            }

            lastPointUsed = randomPoint;

            Instantiate(enemyShip, enemyShipSpawnPoints[randomPoint].transform.position, enemyShip.transform.rotation);
        }
    }

    public void GetEnemyForceSize()
    {
        numbOfOpponents = StartMenuController.enemies;
    }
}
