using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFrigateManager : MonoBehaviour
{

    //Class Variables
    private Rigidbody enemyFrigateRb;

    public ParticleSystem explosion;

    public ParticleSystem missileExplosion;

    public ParticleSystem ciwsHit;

    public ParticleSystem destructionExplosion;

    [SerializeField] private float health;

    private float gunHit = 20.0f;

    private float missileHit = 35.0f;

    private float ciwsImpact = 10.0f;

    private float draught;

    OceanManager oceanManager;

    public float buoyancyForce;

    public GameObject explosionpt1;
    public GameObject explosionpt2;
    public GameObject explosionpt3;
    public GameObject explosionpt4;
    public GameObject explosionpt5;
    public GameObject explosionpt6;
    public GameObject explosionpt7;

    private bool explode = false;

    public GameObject[] missileHatch = new GameObject[3];

    public GameObject[] missileLaunchPt = new GameObject[3];

    PlayerController playerController;

    CIWSController ciwsController;

    private int fire;

    public GameObject missile;

    public float force;

    public float turningForce;

    public float missileDetectRange = 7000;

    float rangeToPlayer;

    public float[] cardinalPoints = new float[4];

    private int headingWanted;

    private int[] speed = new int[] {0, 15000, 40000, 60000, 100000};

    private string[] speedPreset = new string[] {"Stop", "Slow", "Half", "Standard", "Full"};

    private int speedSelected;

    SpawnManager spawnManager;

    public bool targeted = false;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        ciwsController = GameObject.Find("CIWSBody").GetComponent<CIWSController>();
        oceanManager = GameObject.Find("Ocean").GetComponent<OceanManager>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        enemyFrigateRb = GetComponent<Rigidbody>();
        InvokeRepeating("HeadingGenerator", 0.0f, StartMenuController.courseChangeRate);
        InvokeRepeating("Engage", 1.0f, StartMenuController.firingRate);

        Debug.Log(StartMenuController.firingRate);
    }

    // Update is called once per frame
    void Update()
    {
        draught = oceanManager.transform.position.y - enemyFrigateRb.transform.position.y;

        if (enemyFrigateRb.transform.position.y <= oceanManager.transform.position.y)
        {
            enemyFrigateRb.AddForce((Vector3.up * Time.deltaTime * buoyancyForce) * draught, ForceMode.Impulse);
        }

        //maneuvering
        rangeToPlayer = Vector3.Distance(transform.position, playerController.transform.position);

        if (rangeToPlayer > missileDetectRange)
        {
            Vector3 direction = playerController.transform.position - transform.position;
            Quaternion heading = Quaternion.LookRotation(direction);

            if (transform.rotation != heading && transform.position.x < playerController.transform.position.x)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * turningForce);
                enemyFrigateRb.AddRelativeForce(Vector3.forward * Time.deltaTime * speed[4], ForceMode.Impulse);
            }

            else if (transform.rotation != heading && transform.position.x > playerController.transform.position.x)
            {
                transform.Rotate(Vector3.down * Time.deltaTime * turningForce);
                enemyFrigateRb.AddRelativeForce(Vector3.forward * Time.deltaTime * speed[4], ForceMode.Impulse);
            }

            else if (transform.rotation != heading && transform.position.x == playerController.transform.position.x && transform.position.z != playerController.transform.position.z)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * turningForce);
                enemyFrigateRb.AddRelativeForce(Vector3.forward * Time.deltaTime * speed[4], ForceMode.Impulse);
            }

            else if (transform.rotation == heading)
            {
                transform.rotation = heading;
                enemyFrigateRb.AddRelativeForce(Vector3.forward * Time.deltaTime * speed[4], ForceMode.Impulse);
            }
        }

        if (rangeToPlayer <= missileDetectRange)
        {
            if (transform.rotation.eulerAngles.y < cardinalPoints[headingWanted])
            {
                transform.Rotate(Vector3.up * Time.deltaTime * turningForce);
                enemyFrigateRb.AddRelativeForce(Vector3.forward * Time.deltaTime * speed[3], ForceMode.Impulse);
            }

            else if (transform.rotation.eulerAngles.y > cardinalPoints[headingWanted])
            {
                transform.Rotate(Vector3.down * Time.deltaTime * turningForce);
                enemyFrigateRb.AddRelativeForce(Vector3.forward * Time.deltaTime * speed[3], ForceMode.Impulse);
            }

            else if (transform.rotation.eulerAngles.y == cardinalPoints[headingWanted])
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, cardinalPoints[headingWanted], transform.rotation.z);
                enemyFrigateRb.AddRelativeForce(Vector3.forward * Time.deltaTime * speed[3], ForceMode.Impulse);
            }
        }

        fire = Random.Range(0, 2);

    }

    //Additional Functions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MainGunProjectile")
        {
            Instantiate(explosion, collision.transform.position, explosion.transform.rotation);
            Destroy(collision.gameObject);
            health -= gunHit;

            if (health < 1.0f)
            {
                DestructionExplosions();
            }
        }

        else if (collision.gameObject.tag == "PlayerMissile")
        {
            Vector3 position = collision.transform.position;
            Destroy(collision.gameObject);
            Instantiate(missileExplosion, position, missileExplosion.transform.rotation);
            health -= missileHit;

            if (health < 1.0f)
            {
                DestructionExplosions();
            }
        }

        else if (collision.gameObject.tag == "PlayerCIWS")
        {
            Vector3 position = collision.transform.position;
            Destroy(collision.gameObject);
            Instantiate(ciwsHit, position, ciwsHit.transform.rotation);
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
            Instantiate(destructionExplosion, explosionpt1.transform.position, destructionExplosion.transform.rotation);
            Instantiate(destructionExplosion, explosionpt2.transform.position, destructionExplosion.transform.rotation);
            Instantiate(destructionExplosion, explosionpt3.transform.position, destructionExplosion.transform.rotation);
            Instantiate(destructionExplosion, explosionpt4.transform.position, destructionExplosion.transform.rotation);
            Instantiate(destructionExplosion, explosionpt5.transform.position, destructionExplosion.transform.rotation);
            Instantiate(destructionExplosion, explosionpt6.transform.position, destructionExplosion.transform.rotation);
            Instantiate(destructionExplosion, explosionpt7.transform.position, destructionExplosion.transform.rotation);

            buoyancyForce = 0;

            spawnManager.numbOfEnemyShipsDestroyed++;
            playerController.score += 10;
            spawnManager.SpawnEnemyShip();
            await Task.Delay(5000);
            Destroy(gameObject);
            
        }
    }

    async private void Engage()
    {
        Debug.Log("Engaging");
        float range = Vector3.Distance(gameObject.transform.position, playerController.gameObject.transform.position);

        if (range <= missileDetectRange && fire == 1)
        {
            int missileBank = Random.Range(0, 3);
            missileHatch[missileBank].transform.rotation = Quaternion.Euler(90, 0, 0);
            await Task.Delay(300);
            Instantiate(missile, missileLaunchPt[missileBank].transform.position, Quaternion.Euler(-90, 0, 0));
            await Task.Delay(500);
            missileHatch[missileBank].transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        else
        {
            return;
        }
    }

    private void HeadingGenerator()
    {
        int headingSelected = Random.Range(0, 4);
        headingWanted =  headingSelected;
    }
}
