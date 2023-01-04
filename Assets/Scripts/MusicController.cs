using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    //Class Variables
    AudioSource intromusic;


    //Awake Function
    private void Awake()
    {
        GameObject[] musicBoxes = GameObject.FindGameObjectsWithTag("IntroMusic");

        if (musicBoxes.Length > 1)
        {
            Destroy(this.gameObject);
        }

        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
