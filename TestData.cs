using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestData : MonoBehaviour
{

    string savedStr = "None";
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            savedStr = "FUCKYOUMEN:D";
            SaveGame();
        }

        if (Input.GetKeyDown("space"))
        {   LoadGame();
            Debug.Log(savedStr);
        }
    }

    void SaveGame()
    {
        PlayerPrefs.SetString("TestSaveStr", savedStr);
        PlayerPrefs.Save();
    }
    void LoadGame()
    {
        if (PlayerPrefs.HasKey("TestSaveStr"))
        {
            savedStr = PlayerPrefs.GetString("TestSaveStr");
            //Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
}
