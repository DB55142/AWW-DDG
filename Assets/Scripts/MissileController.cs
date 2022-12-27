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

    

    // Start is called before the first frame update
    void Start()
    {
        OpenFwd = Quaternion.Euler(90, 0, 0);
        OpenAft = Quaternion.Euler(-90, 0, 0);
        Close = Quaternion.Euler(0, 0, 0);
        missileStartPos = Quaternion.Euler(-90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (weaponState)
            {
                weaponState = false;
                Debug.Log("on");
            }

            else
            {
                weaponState = true;
                Debug.Log("off");
            }
        }

        if (weaponState == true)
        {
            if (Input.GetKeyUp(KeyCode.PageUp))
            {
                if (missileLauncher1.transform.rotation != OpenFwd)
                {
                    LaunchMissile(missileLaunchPoint1, missileLauncher1, OpenFwd);
                }

                else
                {
                    CloseMissileHatch(missileLauncher1);
                }
            }

            if (Input.GetKeyUp(KeyCode.Home))
            {
                if (missileLauncher1.transform.rotation != OpenFwd)
                {
                    LaunchMissile(missileLaunchPoint2, missileLauncher2, OpenFwd);
                }

                else
                {
                    CloseMissileHatch(missileLauncher2);
                }
            }

            if (Input.GetKeyUp(KeyCode.Insert))
            {
                if (missileLauncher1.transform.rotation != OpenFwd)
                {
                    LaunchMissile(missileLaunchPoint3, missileLauncher3, OpenFwd);
                }

                else
                {
                    CloseMissileHatch(missileLauncher3);
                }
            }

            if (Input.GetKeyUp(KeyCode.PageDown))
            {
                if (missileLauncher1.transform.rotation != OpenAft)
                {
                    LaunchMissile(missileLaunchPoint4, missileLauncher4, OpenAft);
                }

                else
                {
                    CloseMissileHatch(missileLauncher4);
                }
            }

            if (Input.GetKeyUp(KeyCode.End))
            {
                if (missileLauncher1.transform.rotation != OpenAft)
                {
                    LaunchMissile(missileLaunchPoint5, missileLauncher5, OpenAft);
                }

                else
                {
                    CloseMissileHatch(missileLauncher5);
                }
            }

            if (Input.GetKeyUp(KeyCode.Delete))
            {
                if (missileLauncher1.transform.rotation != OpenAft)
                {
                    LaunchMissile(missileLaunchPoint6, missileLauncher6, OpenAft);
                }

                else
                {
                    CloseMissileHatch(missileLauncher6);
                }
            }
        }

      
    }

    //Additional Functions

    async void LaunchMissile(GameObject missileLaunchPoint, GameObject missileLaunchHatch, Quaternion OpenAngle)
    {
        missileLaunchHatch.transform.rotation = Quaternion.Lerp(missileLaunchHatch.transform.rotation, OpenAngle, 1.0f);
        await Task.Delay(200);
        Instantiate(missileProjectile, missileLaunchPoint.transform.position, missileStartPos);
        await Task.Delay(1500);
        missileLaunchHatch.transform.rotation = Quaternion.Lerp(missileLaunchHatch.transform.rotation, Close, 1.0f);
    }

    void CloseMissileHatch(GameObject missileLaunchHatch)
    {
        missileLauncher1.transform.rotation = Close;
    }
}
