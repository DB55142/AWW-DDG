using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    //Class Variables
    public ParticleSystem waterRipple;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Additional Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainGunProjectile" || other.gameObject.tag == "EnemyGunProjectile")
        {
            Debug.Log("In The Water");
            Vector3 position = other.transform.position;
            Instantiate(waterRipple, position, waterRipple.transform.rotation);
            Destroy(other.gameObject);
            
        }

        else if (other.gameObject.tag == "PlayerCIWS" || other.gameObject.tag == "EnemyCIWS")
        {
            Vector3 position = other.transform.position;
            Instantiate(waterRipple, position, waterRipple.transform.rotation);
            Destroy(other.gameObject);

        }

        else if (other.gameObject.tag == "PlayerMissile" || other.gameObject.tag == "EnemyMissile")
        {
            Vector3 position = other.transform.position;
            Instantiate(waterRipple, position, waterRipple.transform.rotation);
            Destroy(other.gameObject);

        }
    }
}
