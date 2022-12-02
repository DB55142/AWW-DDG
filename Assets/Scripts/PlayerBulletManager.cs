using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerBulletManager : MonoBehaviour
{
    //Class Variables
    Rigidbody projectile;
    public float charge;


    // Start is called before the first frame update
    void Start()
    {
        projectile = GetComponent<Rigidbody>();
        projectile.AddRelativeForce(Vector3.forward * Time.deltaTime * charge, ForceMode.Impulse);
        DeleteBullet();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Additional Functions
    async void DeleteBullet()
    {
        await Task.Delay(2000);

        if (gameObject == true)
        {
            Destroy(gameObject);
        }

        else
        {
            return;
        }
        
    }
}
