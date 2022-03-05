using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class CSVtoMap : MonoBehaviour
{
    public string lvl;
    [SerializeField] public GameObject ParentLvl;

    [SerializeField] public GameObject Hole;
    [SerializeField] public GameObject Wall;

    [SerializeField] public GameObject Ground;

    int SizeX, SizeY;

    public void LoadLevels(int levelIndex)
    {
        lvl = "lvl" + levelIndex;
        ClearLevels();
        ReadCSVAndGenerate();
    }

    public void ClearLevels()
    {
        Transform transform = ParentLvl.transform;
        for (int i = 0; i < transform.childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void ReadCSVAndGenerate()
    {
        TextAsset csv =new TextAsset();
        try
        {
            csv = Resources.Load<TextAsset>("map/"+lvl);

        }
        catch (System.Exception)
        {
            SceneManager.LoadScene("MainMenu");
        }

        float x = 0, y = 0;
        bool isMoveBall = false;
        bool isMoveVictory = false;

        string[] lines = csv.text.Split(new char[] { '\n' });
        foreach(string line in lines)
        {
            string[] columns = line.Split(new char[] { ',' });

            if(columns[0] == "size")
            {
                SizeX =  (int.Parse(columns[1]) /2) ;
                SizeY = (int.Parse(columns[2]) /2) ;
                continue;
            }

            x = 0;
            foreach(string col in columns)
            {
                switch (col)
                {
                    case "0":
                        SpawnHole(x - SizeX, y - SizeY);
                        break;
                    case "O":
                        SpawnWay(x-SizeX, y-SizeY);
                        break;
                    case "X":
                        SpawnWall(x - SizeX, y - SizeY);
                        break;
                    case "3":
                        MoveVictoryBox(x - SizeX, y - SizeY);
                        isMoveVictory = true;
                        break;
                    case "1":
                        MovePlayer(x - SizeX, y - SizeY);
                        isMoveBall = true;
                        break;
                    default:
                        break;
                }
                ++x;
            }
            ++y;
        }

        if(!isMoveBall || !isMoveVictory)
        {
            Debug.LogError("Il manque la Ball ou la Victory Box dans la génération du niveaux");
            throw new Exception("Il manque la Ball ou la Victory Box dans la génération du niveaux");
        }
    }

    public void SpawnWall(float x,float y)
    {
        GameObject wall = Instantiate(Wall);
        wall.transform.position = new Vector3(y, 0.5f, x);

        wall.transform.SetParent(ParentLvl.transform);
    }

    public void SpawnHole(float x, float y)
    {
        GameObject a = Instantiate(Hole);
        a.transform.position=new Vector3(y, -0.5f, x);
        a.transform.SetParent(ParentLvl.transform);
    }

    public void SpawnWay(float x, float y)
    {
        GameObject a = Instantiate(Ground) ;
        a.transform.position=new Vector3(y, -0.5f, x);
        a.transform.SetParent(ParentLvl.transform);
    }

    public void MoveVictoryBox(float x, float y)
    {
        GameObject[] VictoryBox = GameObject.FindGameObjectsWithTag("VictoryBox");
        VictoryBox[0].transform.position = new Vector3(y, 0.2f, x);
        SpawnWay(x, y);
    }

    public void SpawnVictoryBox(float x, float y)
    {
        GameObject victoryBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
        victoryBox.GetComponent<BoxCollider>().isTrigger = true;
        victoryBox.AddComponent<VictoryBox>();
        victoryBox.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        victoryBox.transform.position = new Vector3(y, 0.5f, x);
        victoryBox.transform.SetParent(ParentLvl.transform);
        SpawnWay(x, y);
    }

    public void MovePlayer(float x, float y)
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        player[0].transform.position = new Vector3(y, 0f, x);
        SpawnWay(x, y);
    }
}
