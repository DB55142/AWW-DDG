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

    public GameObject missileProjectile;

    Quaternion OpenFwd;
    Quaternion OpenAft;
    Quaternion Close = new Quaternion(0, 0, 0, 0);

    

    // Start is called before the first frame update
    void Start()
    {
        OpenFwd = Quaternion.Euler(90, 0, 0);
        OpenAft = Quaternion.Euler(-90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyUp(KeyCode.PageUp))
        {
            if (missileLauncher1.transform.rotation != OpenFwd )
            {
                missileLauncher1.transform.rotation = Quaternion.Lerp(missileLauncher1.transform.rotation, OpenFwd, 1.0f);
                LaunchMissile();
                
            }

            else
            {
                missileLauncher1.transform.rotation = Close;
            }
        }

        if (Input.GetKeyUp(KeyCode.Home))
        {
            if (missileLauncher2.transform.rotation != OpenFwd)
            {
                missileLauncher2.transform.rotation = Quaternion.Lerp(missileLauncher1.transform.rotation, OpenFwd, 1.0f); ;
            }

            else
            {
                missileLauncher2.transform.rotation = Close;
            }
        }

        if (Input.GetKeyUp(KeyCode.Insert))
        {
            if (missileLauncher3.transform.rotation != OpenFwd)
            {
                missileLauncher3.transform.rotation = Quaternion.Lerp(missileLauncher1.transform.rotation, OpenFwd, 1.0f); ;
            }

            else
            {
                missileLauncher3.transform.rotation = Close;
            }
        }

        if (Input.GetKeyUp(KeyCode.PageDown))
        {
            if (missileLauncher4.transform.rotation != OpenAft)
            {
                missileLauncher4.transform.rotation = Quaternion.Lerp(missileLauncher1.transform.rotation, OpenAft, 1.0f); ;
            }

            else
            {
                missileLauncher4.transform.rotation = Close;
            }
        }

        if (Input.GetKeyUp(KeyCode.End))
        {
            if (missileLauncher5.transform.rotation != OpenAft)
            {
                missileLauncher5.transform.rotation = Quaternion.Lerp(missileLauncher1.transform.rotation, OpenAft, 1.0f); ;
            }

            else
            {
                missileLauncher5.transform.rotation = Close;
            }
        }

        if (Input.GetKeyUp(KeyCode.Delete))
        {
            if (missileLauncher6.transform.rotation != OpenAft)
            {
                missileLauncher6.transform.rotation = Quaternion.Lerp(missileLauncher1.transform.rotation, OpenAft, 1.0f); ;
            }

            else
            {
                missileLauncher6.transform.rotation = Close;
            }
        }
    }

    //Additional Functions

    async void LaunchMissile()
    {
        await Task.Delay(200);
        Instantiate(missileProjectile, missileLaunchPoint1.transform.position, missileProjectile.transform.rotation);
        await Task.Delay(1500);
        missileLauncher1.transform.rotation = Quaternion.Lerp(missileLauncher1.transform.rotation, Close, 0.5f).normalized;
    }
}
