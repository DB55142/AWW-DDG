using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;
using System.Threading.Tasks;

public class CIWSController : MonoBehaviour
{
    //Class Variables
    private GameObject target;
    public GameObject firingPoint;
    bool tracking = false;

    PlayerController playerController;

    public float detectionRange;

    [SerializeField] private float rotationSpeed;

    bool firing = false;

    bool autoMode = false;

    bool autoFire = false;

    bool safeToFire = false;

    public GameObject ciwsRadarRange;

    public GameObject ciwsBullet;

    public GameObject ciwsFiringPoint;



    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating("TargetCheck", 0.0f, 0.5f);
        
        InvokeRepeating("FiringRate", 0.0f, 0.1f);
 
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(firingPoint.transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit unintended))
        {
            if (unintended.collider.gameObject.tag == "Player")
            {
                safeToFire = false;
            }

            else if (unintended.collider.gameObject.tag != "Player")
            {
                safeToFire = true;
            }
            
        }

        if (autoMode == true)
        {
            TrackTarget();
        }

        if (autoMode == false)
        {
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

            if (Input.GetKey(KeyCode.Backslash))
            {
                if (!autoFire)
                {
                    autoFire = true;
                }
            }

            if (!Input.GetKey(KeyCode.Backslash))
            {
                autoFire = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (!autoMode)
            {
                autoMode = true;
            }

            else 
            {
                autoMode = false;
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
            Vector3 offsetTarget = new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), Random.Range(-15, 15));
            Vector3 targetPos = target.transform.position - offsetTarget;
            Quaternion ciwsRotation = Quaternion.LookRotation(targetPos - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, ciwsRotation, 0.5f);

            if (safeToFire == true)
            {
                autoFire = true;
            }
        }

    }

    private void FiringRate()
    {
        if (autoFire == true)
        {
            Instantiate(ciwsBullet, ciwsFiringPoint.transform.position, transform.rotation);
        }

        else
        {
            return;
        }
        
    }


}
