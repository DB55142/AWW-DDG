using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class CIWSController : MonoBehaviour
{
    //Class Variables
    private GameObject target;
    bool tracking = false;

    PlayerController playerController;

    [SerializeField] private float detectionRange;

    [SerializeField] private float rotationSpeed;

    public ParticleSystem bullets;

    bool firing = false;



    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating("TargetCheck", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
        TrackTarget();

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.left * Time.deltaTime * rotationSpeed);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
        }

        if (Input.GetKey(KeyCode.M))
        {
            if (!bullets.isPlaying)
            {
                bullets.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            if (bullets.isPlaying)
            {
                bullets.Stop();
            }
        }
    }

    //Aditional Functions
    private void TargetCheck()
    {
        target = GameObject.FindGameObjectWithTag("EnemyMissile");

        if (target != null)
        {
            float distance = Vector3.Distance(target.transform.position, playerController.transform.position);

            if (distance < detectionRange)
            {
                tracking = true;
            }

            else if (distance >= detectionRange)
            {
                tracking = false;
            }
        }

    }

    private void TrackTarget()
    {
        if (tracking == true)
        {
            Debug.Log("tracking");
            Vector3 offsetTarget = new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), Random.Range(-15, 15));
            Vector3 targetPos = target.transform.position - offsetTarget;
            Quaternion ciwsRotation = Quaternion.LookRotation(targetPos - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, ciwsRotation, 0.5f);

            if (!bullets.isPlaying)
            {
                bullets.Play();
            }
        }

    }


}
