﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "OP")
        {
            GameManager.instance.updateLevelData(1);
            SaveSystem.SaveGame();
        }
        else if(SceneManager.GetActiveScene().name == "Level2Winter")
        {
            GameManager.instance.updateLevelData(2);
            SaveSystem.SaveGame();
        }
        else if(SceneManager.GetActiveScene().name == "Level2SummerRoom")
        {
            GameManager.instance.updateLevelData(3);
            SaveSystem.SaveGame();
        }

        else if (SceneManager.GetActiveScene().name == "Level2Fall")
        {
            GameManager.instance.updateLevelData(4);
            SaveSystem.SaveGame();
        }
        else if (SceneManager.GetActiveScene().name == "Level3OpenWindow")
        {
            GameManager.instance.updateLevelData(5);
            SaveSystem.SaveGame();
        }
        else if (SceneManager.GetActiveScene().name == "Level4")
        {
            GameManager.instance.updateLevelData(6);
            SaveSystem.SaveGame();
        }
        else if (SceneManager.GetActiveScene().name == "Level4Part2")
        {
            GameManager.instance.updateLevelData(7);
            SaveSystem.SaveGame();
        }
        else if (SceneManager.GetActiveScene().name == "Level5")
        {
            GameManager.instance.updateLevelData(8);
            SaveSystem.SaveGame();
        }
    }
}
