using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //Class Variables

    [SerializeField] float rotateSpeed;

    public GameObject playerHull;
    public Rigidbody playerHullRigidBody;

    public TextMeshProUGUI speedTitle;
    public TextMeshProUGUI speedIndicator;
    public TextMeshProUGUI courseRepeater;
    public Scrollbar rudderAngleIndicator;

    private string[] presetSpeeds = new string[] { "-15kts", "-10kts", "-5kts", "0kts", "5kts", "10kts", "15kts", "20kts", "25kts", "30kts" };
    private float[] presetSpeedActual = new float[] { -75000f, -50000f, -25000f, 0.0f, 25000f, 50000f, 75000f, 140000f, 160000f, 200000 };
    private float[] presetSpeedWake = new float[] { -2.5f, -1.0f, -0.5f, 0.0f, 0.5f, 1.0f, 2.5f, 4.0f, 5.0f, 8.0f };
    private float[] presetSpeedWakeLife = new float[] { 3, 2, 1, 0.01f, 1, 2, 3, 4, 5, 7 };
    private float[] rudderResistance = new float[] { -0.4f, -0.2f, -0.1f, 0.0f, 0.1f, 0.25f, 0.3f, 0.55f, 0.75f, 1.0f };
    private float[] rudderInput = new float[] { -40.0f, -20.0f, -10.0f, 0.0f, 10.0f, 20.0f, 40.0f };
    private float[] rudderAngleIndicatorValue = new float[] { 0.0f, 0.212f, 0.424f, 0.50f, 0.576f, 0.788f, 1.0f };
    private float[] shipHeelAngle = new float[] { -15.0f, -10.0f, -3.0f, 0, 3.0f, 10.0f, 15.0f };
    private int presetSelected = 3;
    private int rudderAngleSelected = 3;

    public float buoyancyWaterline;


    public ParticleSystem playerWake;

    public GameObject playerGun;


    public GameObject playerGunVertical;

    private GameObject ocean;

    public bool playerGunManual = false;

    public float buoyancyForce;

    [SerializeField] float rotationSpeed;

    private float draught;

    public GameObject torquePoint;

    public GameObject torquePointForward;



    // Start is called before the first frame update
    void Start()
    {
        ocean = GameObject.Find("Ocean");

    }

    // Update is called once per frame
    void Update()
    {
        draught = ocean.transform.position.y - playerHullRigidBody.transform.position.y;

        if (playerHullRigidBody.transform.position.y <= ocean.transform.position.y)
        {
            playerHullRigidBody.AddForce((Vector3.up * Time.deltaTime * buoyancyForce) * draught, ForceMode.Impulse);
        }


        if (Input.GetKeyUp(KeyCode.W))
        {
            if (presetSelected < presetSpeedActual.Length - 1)
            {
                presetSelected += 1;
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (presetSelected > 0)
            {
                presetSelected -= 1;
            }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            if (rudderAngleSelected < rudderInput.LongLength - 1)
            {
                rudderAngleSelected += 1;
            }

            else
            {
                return;
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (rudderAngleSelected > 0)
            {
                rudderAngleSelected -= 1;   
            }

            else
            {
                return;
            }
        }
            
        speedTitle.text = "SPEED:";

        speedIndicator.text = presetSpeeds[presetSelected];

        rudderAngleIndicator.value = rudderAngleIndicatorValue[rudderAngleSelected];

        
        if (transform.rotation.eulerAngles.y < 10.0f)
        {
            courseRepeater.text = "00" + Convert.ToString(Math.Round(transform.rotation.eulerAngles.y, 1)) + "°";
        }

        else if (transform.rotation.eulerAngles.y >= 10.0f && transform.rotation.eulerAngles.y < 100.0f)
        {
            courseRepeater.text = "0" + Convert.ToString(Math.Round(transform.rotation.eulerAngles.y, 1)) + "°";
        }

        else if (transform.rotation.eulerAngles.y >= 100.0f)
        {
            courseRepeater.text = Convert.ToString(Math.Round(transform.rotation.eulerAngles.y, 1)) + "°";
        }





        if (rudderAngleIndicator.value < 0.5f)
        {
            rudderAngleIndicator.image.color = Color.red;
        }

        else if (rudderAngleIndicator.value > 0.5f)
        {
            rudderAngleIndicator.image.color = Color.green;
        }

        else
        {
            rudderAngleIndicator.image.color = Color.black;
        }

       playerHullRigidBody.AddRelativeForce(Vector3.forward * Time.deltaTime * presetSpeedActual[presetSelected], ForceMode.Impulse);

       playerHullRigidBody.transform.Rotate((Vector3.up * Time.deltaTime * rudderInput[rudderAngleSelected] * rudderResistance[presetSelected]) * rotationSpeed);

       


        var wake = playerWake.velocityOverLifetime;
        wake.z = presetSpeedWake[presetSelected];
        playerWake.startLifetime = presetSpeedWakeLife[presetSelected];

        if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            if (playerGunManual == false)
            {
                playerGunManual = true;
            }

            else
            {
                playerGunManual = false;
            }
        }



        if (playerGunManual == true)
        {
            if (Input.GetKey(KeyCode.Keypad4))
            {
                playerGun.transform.Rotate(Vector3.down * Time.deltaTime * rotateSpeed);
            }

            else if (Input.GetKey(KeyCode.Keypad6))
            {
                playerGun.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
            }

            else if (Input.GetKey(KeyCode.Keypad8))
            {
                playerGunVertical.transform.Rotate(Vector3.left * Time.deltaTime * rotateSpeed);
            }

            else if (Input.GetKey(KeyCode.Keypad2))
            {
                playerGunVertical.transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed);
            }
        }
    }
}
