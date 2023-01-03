using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour
{
    //Class Variables
    public Button returnButton;

    // Start is called before the first frame update
    void Start()
    {
        returnButton.onClick.AddListener(BackToMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Additional Functions
    void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
