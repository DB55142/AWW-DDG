using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileManager : MonoBehaviour
{
    //Class Variables
    Rigidbody missileRb;
    int randomSpeed;

    public ParticleSystem missileDestroyedExplosion;


    // Start is called before the first frame update
    void Start()
    {
        randomSpeed = Random.Range(30000, 60000);
        missileRb = GetComponent<Rigidbody>();
        missileRb.AddForce(Vector3.up * Time.deltaTime * 5000, ForceMode.Impulse);
        missileRb.AddForce(Vector3.back * Time.deltaTime * randomSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Additional Functions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerCIWS")
        {
            Vector3 position = transform.position;
            Destroy(gameObject);
            Instantiate(missileDestroyedExplosion, position, missileDestroyedExplosion.transform.rotation);
        }
    }

}
