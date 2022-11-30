using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Class Variables

    [SerializeField] float rotateSpeed;

    Rigidbody rigidBody;

    public TextMeshProUGUI speedTitle;
    public TextMeshProUGUI speedIndicator;
    public Scrollbar rudderAngleIndicator;

    private string[] presetSpeeds = new string[] { "-15kts", "-10kts", "-5kts", "0kts", "5kts", "10kts", "15kts", "20kts", "25kts", "30kts" };
    private float[] presetSpeedActual = new float[] { -5.95f, -5.94f, -5.9f, 0.0f, 5.9f, 5.94f, 5.95f, 6.0f, 6.2f, 6.5f };
    private float[] rudderResistance = new float[] { -0.4f, -0.2f, -0.1f, 0.0f, 0.1f, 0.25f, 0.3f, 0.55f, 0.75f, 1.0f };
    private float[] rudderInput = new float[] { -40.0f, -20.0f, -10.0f, 0.0f, 10.0f, 20.0f, 40.0f };
    private float[] rudderAngleIndicatorValue = new float[] { 0.0f, 0.212f, 0.424f, 0.50f, 0.576f, 0.788f, 1.0f };
    private int presetSelected = 3;
    private int rudderAngleSelected = 3;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
       
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

        rigidBody.AddRelativeForce(Vector3.forward * Time.deltaTime * presetSpeedActual[presetSelected], ForceMode.Impulse);

        rigidBody.transform.Rotate(Vector3.up * Time.deltaTime * rudderInput[rudderAngleSelected] * rudderResistance[presetSelected]);

    }
}
