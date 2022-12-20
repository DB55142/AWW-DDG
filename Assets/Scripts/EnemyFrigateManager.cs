using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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

    public GameObject ocean;

    public float buoyancyForce;

    public GameObject explosionpt1;
    public GameObject explosionpt2;
    public GameObject explosionpt3;
    public GameObject explosionpt4;
    public GameObject explosionpt5;
    public GameObject explosionpt6;
    public GameObject explosionpt7;

    private bool explode = false;


    // Start is called before the first frame update
    void Start()
    {
        enemyFrigateRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        draught = ocean.transform.position.y - enemyFrigateRb.transform.position.y;

        if (enemyFrigateRb.transform.position.y <= ocean.transform.position.y)
        {
            enemyFrigateRb.AddForce((Vector3.up * Time.deltaTime * buoyancyForce) * draught, ForceMode.Impulse);
        }

        
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
}
