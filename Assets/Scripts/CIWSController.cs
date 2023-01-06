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
    public GameObject ciwsRadarRange;
    public GameObject ciwsBullet;
    public GameObject ciwsFiringPoint;

    PlayerController playerController;

    public float detectionRange;

    [SerializeField] private float rotationSpeed;

    bool firing = false;

    public bool autoMode = false;

    bool autoFire = false;

    public bool safeToFire = false;

    bool tracking = false;

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

        if (autoMode == true)
        {
            TrackTarget();
        }

        if (autoMode == false)
        {
            ManualOperation();
        }

        if (target == null)
        {
            autoFire = false;
        }
    }

    //Aditional Functions
    private void ManualOperation()
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

            if (Input.GetKey(KeyCode.RightControl))
            {
                if (!autoFire)
                {
                    autoFire = true;
                }
            }

            if (!Input.GetKey(KeyCode.RightControl))
            {
                autoFire = false;
            }
    }


    private void TargetCheck()
    {
        target = GameObject.FindGameObjectWithTag("AimPoint");

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
                target = null;
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
            autoFire = true;
            FiringRate();
        }
    }

    private async  void FiringRate()
    {
        if (autoFire == true && target != null)
        {
            Instantiate(ciwsBullet, ciwsFiringPoint.transform.position, transform.rotation);
            await Task.Delay(100);
        }

        else
        {
            return;
        }
        
    }
}
