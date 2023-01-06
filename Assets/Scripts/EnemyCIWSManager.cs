using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCIWSManager : MonoBehaviour
{
    //Class Variables
    private GameObject target;

    public GameObject ciwsBullet;

    public GameObject ciwsFiringPoint;

    bool tracking = false;

    PlayerController playerController;

    public float detectionRange;

    [SerializeField] private float rotationSpeed;

    bool firing = false;

    bool autoMode = false;

    bool autoFire = false;

    bool safeToFire = false;

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
        AcquireTarget();
        TrackTarget();
    }

    //Aditional Functions
    private void AcquireTarget()
    {
        Ray ray = new Ray(ciwsFiringPoint.transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit unintended))
        {
            if (unintended.collider.gameObject.tag == "Enemy")
            {
                safeToFire = false;
            }

            else if (unintended.collider.gameObject.tag != "Enemy")
            {
                safeToFire = true;
            }
        }
    }

    private void TargetCheck()
    {
        target = GameObject.FindGameObjectWithTag("PlayerMissile");

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

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
            Vector3 offsetTarget = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(-30, 30));
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
        if (autoFire == true && target != null)
        {
            Instantiate(ciwsBullet, ciwsFiringPoint.transform.position, transform.rotation);
        }

        else
        {
            return;
        }
    }
}
