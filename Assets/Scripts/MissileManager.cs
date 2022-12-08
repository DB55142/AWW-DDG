using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    //Additional Variables
    public Rigidbody missileRb;

    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        missileRb.AddForce(Vector3.up * Time.deltaTime * speed, ForceMode.Impulse);
    }
}
