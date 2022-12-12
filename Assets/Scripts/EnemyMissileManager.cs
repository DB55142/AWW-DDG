using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileManager : MonoBehaviour
{
    Rigidbody missileRb;
    int randomSpeed;
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
}
