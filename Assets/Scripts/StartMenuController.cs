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
using System.Threading.Tasks;

public class StartMenuController : MonoBehaviour
{
    //Class Variables
    public Button startGame;

    public Button helpButton;

    public Button easyDifficulty;

    public Button mediumDifficulty;

    public Button highDifficulty;

    public Button exitGame;



    public TMP_InputField numbOfEnemies;

    public static int enemies;

    public static int firingRate;

    public static int gunFiringRate;

    public static int courseChangeRate;

    GameObject musicBox;

    // Start is called before the first frame update
    void Start()
    {
        startGame.onClick.AddListener(StartGame);
        helpButton.onClick.AddListener(Help);
        easyDifficulty.onClick.AddListener(EasyDifficulty);
        mediumDifficulty.onClick.AddListener(MediumDifficulty);
        highDifficulty.onClick.AddListener(HighDifficulty);
        exitGame.onClick.AddListener(ExitGame);
        musicBox = GameObject.FindGameObjectWithTag("IntroMusic");

        if (musicBox != null)
        {
            Debug.Log("Found it");
        }
    
    }

    //Update function is called once every frame
    private void Update()
    {
        if (firingRate == 25)
        {
            easyDifficulty.image.color = Color.green;
            mediumDifficulty.image.color = Color.white;
            highDifficulty.image.color = Color.white;
        }

        if (firingRate == 20)
        {
            easyDifficulty.image.color = Color.white;
            mediumDifficulty.image.color = Color.yellow;
            highDifficulty.image.color = Color.white;
        }

        if (firingRate == 15)
        {
            easyDifficulty.image.color = Color.white;
            mediumDifficulty.image.color = Color.white;
            highDifficulty.image.color = Color.red;
        }
    }

    //Additional Classes

    //Additional Functions
    private async void StartGame()
    {
        if (enemies != 0 && enemies.GetType() == typeof(int))
        {
            SceneManager.LoadScene(1);
            Destroy(musicBox);
        }

        else
        {
            numbOfEnemies.image.color = Color.red;
            await Task.Delay(500);
            numbOfEnemies.image.color = Color.white;
        }
        

    }



    private void Help()
    {
        SceneManager.LoadScene(2);

    }

    public async void NumberOfEnemies(string ships)
    {
        if (ships.All(char.IsDigit))
        {   
            enemies = Convert.ToInt32(ships);
            numbOfEnemies.image.color = Color.green;
        }
        
        else
        {
            numbOfEnemies.image.color = Color.red;
            await Task.Delay(500);
            numbOfEnemies.image.color = Color.white;
        }
    }

    public void EasyDifficulty()
    {
        firingRate = 25;
        gunFiringRate = 15;
        courseChangeRate = 60;
    }

    public void MediumDifficulty()
    {
        firingRate = 20;
        gunFiringRate = 10;
        courseChangeRate = 40;
    }

    public void HighDifficulty()
    {
        firingRate = 15;
        gunFiringRate = 5;
        courseChangeRate = 30;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();

#endif
    }

}
