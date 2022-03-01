using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLevels : MonoBehaviour
{
    
    CSVtoMap GenerateLevel;

    int LevelIndex = 1;
    int nbLevels;
    int SizeX, SizeY;

    void Start()
    {
        GenerateLevel = gameObject.GetComponent<CSVtoMap>();

        nbLevels = Resources.LoadAll("map/").Length;
        if (CrossSceneInformation.Info > 0)
        {
            LevelIndex = CrossSceneInformation.Info;
        }
        if(!PlayerPrefs.HasKey("lvl")){
            PlayerPrefs.SetInt("lvl", 1);
        }
        GenerateLevel.LoadLevels(LevelIndex);
    }

    public void NextLevels()
    {
        LevelIndex++;
        if(LevelIndex <= nbLevels) {
            if(PlayerPrefs.GetInt("lvl") < LevelIndex){
                PlayerPrefs.SetInt("lvl", LevelIndex);
            }
            GenerateLevel.LoadLevels(LevelIndex);
        }
    }

    public void ReloadLevels()
    {
        GenerateLevel.LoadLevels(LevelIndex);
    }

}
