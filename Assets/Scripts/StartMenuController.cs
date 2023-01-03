using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class StartMenuController : MonoBehaviour
{
    //Class Variables
    public Button startGame;

    public Button helpButton;



    public TMP_InputField numbOfEnemies;

    public static int enemies;

    // Start is called before the first frame update
    void Start()
    {
        startGame.onClick.AddListener(StartGame);
        helpButton.onClick.AddListener(Help);
    }

    //Additional Classes

    //Additional Functions
    private void StartGame()
    {
        SceneManager.LoadScene(1);

    }

    private void Help()
    {
        SceneManager.LoadScene(2);

    }

    public void NumberOfEnemies(string ships)
    {
        if (ships.All(char.IsDigit))
        {   
            enemies = Convert.ToInt32(ships);
            numbOfEnemies.image.color = Color.white;
        }
        
        else
        {
            numbOfEnemies.image.color = Color.red;
        }
    }
}
