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
        courseChangeRate = 60;
    }

    public void MediumDifficulty()
    {
        firingRate = 20;
        courseChangeRate = 40;
    }

    public void HighDifficulty()
    {
        firingRate = 15;
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
