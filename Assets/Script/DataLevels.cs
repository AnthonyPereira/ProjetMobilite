using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLevels : MonoBehaviour
{
    
    CSVtoMap GenerateLevel;

    int LevelIndex = 1;
    int nbLevels;
    int SizeX, SizeY;

    int NbCoinCollected;

    void Start()
    {
        GenerateLevel = gameObject.GetComponent<CSVtoMap>();

        NbCoinCollected = 0;
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

    private int CalculScore()
    {
        int Score = 1;
        int NbCoinsLvl = GenerateLevel.NbCoins;
        if(NbCoinCollected >= NbCoinsLvl/2) ++Score;
        if(NbCoinCollected == NbCoinsLvl) ++Score;

        Debug.Log(NbCoinsLvl + " -> " + NbCoinCollected);

        return Score;
    }

    public void Victory()
    {
        int Score = CalculScore();

        string NameData = "lvl" + LevelIndex;
        if(!PlayerPrefs.HasKey(NameData)) PlayerPrefs.SetInt(NameData, Score);
        else
        {
            if(Score > PlayerPrefs.GetInt(NameData)) PlayerPrefs.SetInt(NameData, Score);
        }

        if(PlayerPrefs.GetInt("lvl") < LevelIndex+1){
            PlayerPrefs.SetInt("lvl", LevelIndex+1);
        }
    }

    public void NextLevels()
    {
        NbCoinCollected = 0;
        LevelIndex++;
        if(LevelIndex <= nbLevels) {
            GenerateLevel.LoadLevels(LevelIndex);
        }
    }

    public void ReloadLevels()
    {
        NbCoinCollected = 0;
        GenerateLevel.LoadLevels(LevelIndex);
    }

    public void AddCoins()
    {
        ++NbCoinCollected;
    }
}
