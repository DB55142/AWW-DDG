using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    //Class Variables
    Rigidbody projectile;
    public float charge;

    public ParticleSystem bulletDestroyed;


    // Start is called before the first frame update
    void Start()
    {
        projectile = GetComponent<Rigidbody>();
        projectile.AddRelativeForce(Vector3.forward * Time.deltaTime * charge, ForceMode.Impulse);
        DeleteBullet();

    }

    //Additional Functions
    async void DeleteBullet()
    {

        if (gameObject == true)
        {
            await Task.Delay(4000);
            Vector3 position = transform.position;
            Instantiate(bulletDestroyed, position, bulletDestroyed.transform.rotation);
            Destroy(gameObject);
        }

        else
        {
            return;
        }
    }
}

