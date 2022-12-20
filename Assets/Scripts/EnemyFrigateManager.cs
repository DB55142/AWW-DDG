using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrigateManager : MonoBehaviour
{

    //Class Variables
    public ParticleSystem explosion;

    public ParticleSystem missileExplosion;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Additional Functions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MainGunProjectile")
        {
            Debug.Log("Hit");
            Instantiate(explosion, collision.transform.position, explosion.transform.rotation);
            Destroy(collision.gameObject);

        }

        else if (collision.gameObject.tag == "PlayerMissile")
        {
            Vector3 position = collision.transform.position;
            Destroy(collision.gameObject);
            Instantiate(missileExplosion, position, missileExplosion.transform.rotation);
        }
    }
}
