using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Class Variables

    [SerializeField] float rotateSpeed;

    public GameObject playerHull;
    public Rigidbody playerHullRigidBody;

    public TextMeshProUGUI speedTitle;
    public TextMeshProUGUI speedIndicator;
    public TextMeshProUGUI courseRepeater;
    public Scrollbar rudderAngleIndicator;

    private string[] presetSpeeds = new string[] { "-15kts", "-10kts", "-5kts", "0kts", "5kts", "10kts", "15kts", "20kts", "25kts", "30kts" };
    private float[] presetSpeedActual = new float[] { -75000f, -50000f, -25000f, 0.0f, 25000f, 50000f, 75000f, 140000f, 160000f, 200000 };
    private float[] presetSpeedWake = new float[] { -2.5f, -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 2.5f, 4.0f, 5.0f, 8.0f };
    private float[] presetSpeedWakeLife = new float[] { 3, 2, 1, 0.01f, 1, 2, 3, 4, 5, 7 };
    private float[] rudderResistance = new float[] { -0.4f, -0.2f, -0.1f, 0.0f, 0.1f, 0.25f, 0.3f, 0.55f, 0.75f, 1.0f };
    private float[] rudderInput = new float[] { -40.0f, -20.0f, -10.0f, 0.0f, 10.0f, 20.0f, 40.0f };
    private float[] rudderAngleIndicatorValue = new float[] { 0.0f, 0.212f, 0.424f, 0.50f, 0.576f, 0.788f, 1.0f };
    private float[] shipHeelAngle = new float[] { -15.0f, -10.0f, -3.0f, 0, 3.0f, 10.0f, 15.0f };
    private int presetSelected = 3;
    private int rudderAngleSelected = 3;

    public float buoyancyWaterline;


    public ParticleSystem playerWake;

    public GameObject playerGun;


    public GameObject playerGunVertical;

    private GameObject ocean;

    public bool playerGunManual = true;

    public float buoyancyForce;

    [SerializeField] float rotationSpeed;

    private float draught;

    public ParticleSystem gunHit;

    public ParticleSystem missileHit;

    public ParticleSystem ciwsHit;

    private float health = 100;

    private float gunImpact = 20.0f;

    private float missileImpact = 35.0f;

    private float ciwsImpact = 10.0f;

    public GameObject explosionpt1;
    public GameObject explosionpt2;
    public GameObject explosionpt3;
    public GameObject explosionpt4;
    public GameObject explosionpt5;
    public GameObject explosionpt6;

    public ParticleSystem destructionExplosion;

    public TextMeshProUGUI hullStatus;

    public TextMeshProUGUI gunMode;

    public TextMeshProUGUI missileMode;

    public TextMeshProUGUI ciwsMode;

    public Button gunButton;

    public Button missileButton;

    public Button ciwsButton;

    CIWSController ciwsController;

    MissileController missileController;

    public bool gameOver = false;

    SpawnManager spawnManager;

    public TextMeshProUGUI gameOverText;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI highScoreText;

    public int score = 0;

    public int highscore = 0;

    public Button returnToMainMenuButton;

    public Button exitGameButton;

    public TextMeshProUGUI highScoreCeleText;


    // Start is called before the first frame update
    void Start()
    {
        ocean = GameObject.Find("Ocean");
        ciwsController = GameObject.Find("CIWSBody").GetComponent<CIWSController>();
        missileController = GameObject.Find("MissileBank").GetComponent<MissileController>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        exitGameButton.onClick.AddListener(ExitGame);
        DisplayHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE: " + score;

        highScoreText.text = "HIGH SCORE: " + highscore;



        if (gameOver)
        {
            gameOverText.gameObject.SetActive(true);
            returnToMainMenuButton.gameObject.SetActive(true);

            if (score >= highscore)
            {
                highScoreCeleText.text = "NEW HIGH SCORE OF " + score + "!";
                highScoreCeleText.gameObject.SetActive(true);
                exitGameButton.gameObject.SetActive(true);
            }

            else
            {
                highScoreCeleText.gameObject.SetActive(false);
                
            }
        }

        if (!gameOver)
        {
            gameOverText.gameObject.SetActive(false);
            returnToMainMenuButton.gameObject.SetActive(false);
            highScoreCeleText.gameObject.SetActive(false);
            exitGameButton.gameObject.SetActive(false);
        }

        if (health <= 0)
        {
            health = 0;
        }

        hullStatus.text = "HULL: " + health + "%";


        if (health >= 75)
        {
            hullStatus.color = Color.green;
        }

        if (health < 75 && health > 50)
        {
            hullStatus.color = Color.yellow;
        }

        if (health < 50)
        {
            hullStatus.color = Color.red;
        }


        draught = ocean.transform.position.y - playerHullRigidBody.transform.position.y;

        if (playerHullRigidBody.transform.position.y <= ocean.transform.position.y)
        {
            playerHullRigidBody.AddForce((Vector3.up * Time.deltaTime * buoyancyForce) * draught, ForceMode.Impulse);
        }


        if (Input.GetKeyUp(KeyCode.W))
        {
            if (presetSelected < presetSpeedActual.Length - 1)
            {
                presetSelected += 1;
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (presetSelected > 0)
            {
                presetSelected -= 1;
            }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            if (rudderAngleSelected < rudderInput.LongLength - 1)
            {
                rudderAngleSelected += 1;
            }

            else
            {
                return;
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (rudderAngleSelected > 0)
            {
                rudderAngleSelected -= 1;   
            }

            else
            {
                return;
            }
        }
            
        speedTitle.text = "SPEED:";

        speedIndicator.text = presetSpeeds[presetSelected];

        rudderAngleIndicator.value = rudderAngleIndicatorValue[rudderAngleSelected];

        
        if (transform.rotation.eulerAngles.y < 10.0f)
        {
            courseRepeater.text = "00" + Convert.ToString(Math.Round(transform.rotation.eulerAngles.y, 1)) + "°";
        }

        else if (transform.rotation.eulerAngles.y >= 10.0f && transform.rotation.eulerAngles.y < 100.0f)
        {
            courseRepeater.text = "0" + Convert.ToString(Math.Round(transform.rotation.eulerAngles.y, 1)) + "°";
        }

        else if (transform.rotation.eulerAngles.y >= 100.0f)
        {
            courseRepeater.text = Convert.ToString(Math.Round(transform.rotation.eulerAngles.y, 1)) + "°";
        }





        if (rudderAngleIndicator.value < 0.5f)
        {
            rudderAngleIndicator.image.color = Color.red;
        }

        else if (rudderAngleIndicator.value > 0.5f)
        {
            rudderAngleIndicator.image.color = Color.green;
        }

        else
        {
            rudderAngleIndicator.image.color = Color.black;
        }

       playerHullRigidBody.AddRelativeForce(Vector3.forward * Time.deltaTime * presetSpeedActual[presetSelected], ForceMode.Impulse);

       playerHullRigidBody.transform.Rotate((Vector3.up * Time.deltaTime * rudderInput[rudderAngleSelected] * rudderResistance[presetSelected]) * rotationSpeed);

       


        var wake = playerWake.velocityOverLifetime;
        wake.z = presetSpeedWake[presetSelected];
        playerWake.startLifetime = presetSpeedWakeLife[presetSelected];

        if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            if (playerGunManual == false)
            {
                playerGunManual = true;
            }

            else
            {
                playerGunManual = false;
            }
        }



        if (playerGunManual == true)
        {
            if (Input.GetKey(KeyCode.Keypad4))
            {
                playerGun.transform.Rotate(Vector3.down * Time.deltaTime * rotateSpeed);
            }

            else if (Input.GetKey(KeyCode.Keypad6))
            {
                playerGun.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
            }

            else if (Input.GetKey(KeyCode.Keypad8))
            {
                playerGunVertical.transform.Rotate(Vector3.left * Time.deltaTime * rotateSpeed);
            }

            else if (Input.GetKey(KeyCode.Keypad2))
            {
                playerGunVertical.transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed);
            }
        }

        if (!playerGunManual)
        {
            gunMode.text = "AUTO";
            gunButton.image.color = Color.green;
        }

        if (playerGunManual)
        {
            gunMode.text = "MAN";
            gunButton.image.color = Color.yellow;
        }

        if (!ciwsController.autoMode)
        {
            ciwsMode.text = "MAN";
            ciwsButton.image.color = Color.yellow;
        }

        if (ciwsController.autoMode)
        {
            ciwsMode.text = "AUTO";
            ciwsButton.image.color = Color.green;
        }

        if (!missileController.weaponState)
        {
            missileMode.text = "DISABLED";
            missileButton.image.color = Color.red;
        }

        if (missileController.weaponState)
        {
            missileMode.text = "ENABLED";
            missileButton.image.color = Color.green;
        }

        DestructionExplosions();
    }

    //Additional Classes
    public class SaveScore
    {
        public int newHighScore;
    }

    //Additional Functions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyGunProjectile")
        {
            Vector3 position = collision.transform.position;
            Instantiate(gunHit, position, gunHit.transform.rotation);
            Destroy(collision.gameObject);
            health -= gunImpact;

            if (health < 1.0f)
            {
                DestructionExplosions();
            }
        }

        if (collision.gameObject.tag == "EnemyMissile")
        {
            Vector3 position = collision.transform.position;
            Instantiate(missileHit, position, missileHit.transform.rotation);
            Destroy(collision.gameObject);
            health -= missileImpact;

            if (health < 1.0f)
            {
                DestructionExplosions();
            }
        }

        if (collision.gameObject.tag == "EnemyCIWS")
        {
            Vector3 position = collision.transform.position;
            Instantiate(ciwsHit, position, ciwsHit.transform.rotation);
            Destroy(collision.gameObject);
            health -= ciwsImpact;

            if (health < 1.0f)
            {
                DestructionExplosions();
            }
        }
    }

    async private void DestructionExplosions()
    {
        if (health < 1.0f)
        {
            if (destructionExplosion == null)
            {
                Instantiate(destructionExplosion, explosionpt1.transform.position, destructionExplosion.transform.rotation);
                Instantiate(destructionExplosion, explosionpt2.transform.position, destructionExplosion.transform.rotation);
                Instantiate(destructionExplosion, explosionpt3.transform.position, destructionExplosion.transform.rotation);
                Instantiate(destructionExplosion, explosionpt4.transform.position, destructionExplosion.transform.rotation);
                Instantiate(destructionExplosion, explosionpt5.transform.position, destructionExplosion.transform.rotation);
                Instantiate(destructionExplosion, explosionpt6.transform.position, destructionExplosion.transform.rotation);
            }


            buoyancyForce = 0;

            gameOver = true;

            await Task.Delay(5000);
            Destroy(gameObject);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();

#endif
    }

    public void UpdateHighScore()
    {
        SaveScore newHighScorer = new SaveScore();

        newHighScorer.newHighScore = score;

        string json = JsonUtility.ToJson(score);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void DisplayHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveScore displayHighScore = JsonUtility.FromJson<SaveScore>(json);
            highscore = displayHighScore.newHighScore;

        }
        
    }
}
