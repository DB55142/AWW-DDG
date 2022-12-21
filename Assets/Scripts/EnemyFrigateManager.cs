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


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        ciwsController = GameObject.Find("CIWSBody").GetComponent<CIWSController>();
        oceanManager = GameObject.Find("Ocean").GetComponent<OceanManager>();
        enemyFrigateRb = GetComponent<Rigidbody>();

        InvokeRepeating("Engage", 1.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        draught = oceanManager.transform.position.y - enemyFrigateRb.transform.position.y;

        if (enemyFrigateRb.transform.position.y <= oceanManager.transform.position.y)
        {
            enemyFrigateRb.AddForce((Vector3.up * Time.deltaTime * buoyancyForce) * draught, ForceMode.Impulse);
        }

        //enemyFrigateRb.AddRelativeForce(Vector3.forward * Time.deltaTime * force, ForceMode.Impulse);

        fire = Random.Range(0, 2);

    }

    //Additional Functions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MainGunProjectile")
        {
            Debug.Log("Hit");
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

            await Task.Delay(5000);
            Destroy(gameObject);
        }
    }

    async private void Engage()
    {
        float range = Vector3.Distance(gameObject.transform.position, playerController.gameObject.transform.position);

        if (range <= 8700 && fire == 1)
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
}
