using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarController : MonoBehaviour
{

    //Class Variables
    private float rangeScale;

    public float shortRange;

    public float mediumRange;

    public float longRange;

    private Camera radarCamera;

    public Button shortRangeButton;

    public Button mediumRangeButton;

    public Button longRangeButton;

    // Start is called before the first frame update
    void Start()
    {
        radarCamera = GetComponent<Camera>();
        rangeScale = shortRange;
    }

    // Update is called once per frame
    void Update()
    {
        radarCamera.orthographicSize = rangeScale;

        if (Input.GetKeyDown(KeyCode.Pause))
        {
            rangeScale = longRange;
        }

        if (Input.GetKeyDown(KeyCode.ScrollLock))
        {
            rangeScale = mediumRange;
        }

        if (Input.GetKeyDown(KeyCode.Print))
        {
            rangeScale = shortRange;
        }

        ButtonColours();
    }

    //Additional Functions
    private void ButtonColours()
    {
        if (rangeScale == shortRange)
        {
            shortRangeButton.image.color = Color.green;
            mediumRangeButton.image.color = Color.white;
            longRangeButton.image.color = Color.white;
        }

        if (rangeScale == mediumRange)
        {
            shortRangeButton.image.color = Color.white;
            mediumRangeButton.image.color = Color.green;
            longRangeButton.image.color = Color.white;
        }

        if (rangeScale == longRange)
        {
            shortRangeButton.image.color = Color.white;
            mediumRangeButton.image.color = Color.white;
            longRangeButton.image.color = Color.green;
        }
    }
}
