using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour
{

    //Class Variables

    private float rangeScale;

    public float shortRange;

    public float mediumRange;

    public float longRange;

    private Camera radarCamera;

    // Start is called before the first frame update
    void Start()
    {
        radarCamera = GetComponent<Camera>();
        radarCamera.enabled = true;
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
    }

    //Additional Functions
}
