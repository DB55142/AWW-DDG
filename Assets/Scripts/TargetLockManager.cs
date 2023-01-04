using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class TargetLockManager : MonoBehaviour
{
    //Class Variables
    private GameObject target;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0.15f, 0);

        if (target != null)
        {
            transform.position = new Vector3(target.transform.position.x, -40, target.transform.position.z);
            
        }
    }

    //Additonal Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            target = other.gameObject;
        }
    }
}
