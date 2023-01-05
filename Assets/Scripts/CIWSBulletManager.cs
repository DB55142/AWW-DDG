using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CIWSBulletManager : MonoBehaviour
{
    //Class Variables
    public Rigidbody bulletRb;

    [SerializeField] float charge;

    CIWSController ciwsController;

    // Start is called before the first frame update
    void Start()
    {
        ciwsController = GameObject.Find("CIWSBody").GetComponent<CIWSController>();
        bulletRb.AddRelativeForce(Vector3.forward * Time.deltaTime * charge, ForceMode.Impulse);
        Timer();
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    //Additional Functions
    async private void Timer()
    {
        await Task.Delay(7500);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


}
