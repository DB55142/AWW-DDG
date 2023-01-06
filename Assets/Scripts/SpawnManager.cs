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
        ShipsRemaining();
        
        if (Input.GetKeyUp(KeyCode.Keypad5) && gunReady == true && playerController.playerGunManual)
        {
            GunFire();
        }

        VictoryCondition();
    }

    //Additonal Functions
    private void ShipsRemaining()
    {
        shipsRemaining = numbOfOpponents - numbOfEnemyShipsDestroyed;

        shipsLeft.text = "SHIPS REMAINING: " + shipsRemaining;

        if (shipsRemaining <= 0)
        {
            shipsLeft.color = Color.green;
        }
    }
    async void MakeGunReady()
    {
        await Task.Delay(2000);
        gunReady = true;
    }

    public void GunFire()
    {
        Instantiate(playerGunProjectile, playerGunFiringPoint.transform.position, playerController.playerGunVertical.transform.rotation);
        playerGunFireSound.Play();
        Instantiate(playerGunGas, playerGunFiringPoint.transform.position, playerController.playerGunVertical.transform.rotation);
        gunReady = false;
        MakeGunReady();
    }

    public void GunAutoFire()
    {
        if (gunReady && targetingController.targetShip != null && Vector3.Distance(playerController.transform.position, targetingController.targetShip.transform.position) <= gunRange && playerController.gameOver == false)
        {
            GunFire();
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

    public void VictoryCondition()
    {
        if (shipsRemaining == 0 && playerController.health > 0)
        {
            playerController.gameOverText.text = "VICTORY!";
            playerController.gameOverText.color = Color.green;
            playerController.gameOver = true;
        }
    }
}
