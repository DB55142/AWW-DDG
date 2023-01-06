using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    //Game Variables
    public GameObject missileLauncher1;
    public GameObject missileLauncher2;
    public GameObject missileLauncher3;
    public GameObject missileLauncher4;
    public GameObject missileLauncher5;
    public GameObject missileLauncher6;

    public GameObject missileLaunchPoint1;
    public GameObject missileLaunchPoint2;
    public GameObject missileLaunchPoint3;
    public GameObject missileLaunchPoint4;
    public GameObject missileLaunchPoint5;
    public GameObject missileLaunchPoint6;

    public GameObject missileProjectile;

    Quaternion OpenFwd;
    Quaternion OpenAft;
    Quaternion Close;
    Quaternion missileStartPos;

    public bool weaponState = false;

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        missileStartPos = Quaternion.Euler(-90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSystemState();
        WeaponSystemOperation();
    }

    //Additional Functions
    private void WeaponSystemState()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (weaponState)
            {
                weaponState = false;
            }

            else
            {
                weaponState = true;
            }
        }
    }

    private void WeaponSystemOperation()
    {
        if (weaponState == true)
        {
            if (Input.GetKeyUp(KeyCode.PageUp))
            {
                if (missileLauncher1.transform.rotation != OpenFwd)
                {
                    LaunchMissile(missileLaunchPoint1);
                }
            }

            if (Input.GetKeyUp(KeyCode.Home))
            {
                if (missileLauncher1.transform.rotation != OpenFwd)
                {
                    LaunchMissile(missileLaunchPoint2);
                }
            }

            if (Input.GetKeyUp(KeyCode.Insert))
            {
                if (missileLauncher1.transform.rotation != OpenFwd)
                {
                    LaunchMissile(missileLaunchPoint3);
                }
            }

            if (Input.GetKeyUp(KeyCode.PageDown))
            {
                if (missileLauncher1.transform.rotation != OpenAft)
                {
                    LaunchMissile(missileLaunchPoint4);
                }
            }

            if (Input.GetKeyUp(KeyCode.End))
            {
                if (missileLauncher1.transform.rotation != OpenAft)
                {
                    LaunchMissile(missileLaunchPoint5);
                }
            }

            if (Input.GetKeyUp(KeyCode.Delete))
            {
                if (missileLauncher1.transform.rotation != OpenAft)
                {
                    LaunchMissile(missileLaunchPoint6);
                }
            }
        }
    }

    async void LaunchMissile(GameObject missileLaunchPoint)
    {
        Instantiate(missileProjectile, missileLaunchPoint.transform.position, missileStartPos);
    }
}
