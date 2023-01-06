using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class TargetLockManager : MonoBehaviour
{
    //Class Variables
    private GameObject target;

    // Update is called once per frame
    void Update()
    {
        TargetLockBehaviour();
    }

    //Additonal Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            target = other.gameObject;
        }
    }

    private void TargetLockBehaviour()
    {
        transform.Rotate(0, 0.15f, 0);

        if (target != null)
        {
            transform.position = new Vector3(target.transform.position.x, -35, target.transform.position.z);

        }

        if (target.GetComponent<EnemyFrigateManager>().health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
