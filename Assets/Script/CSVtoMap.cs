using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVtoMap : MonoBehaviour
{
    [SerializeField] public string lvl;
    [SerializeField] public GameObject parent;
    List<List<string>> level = new List<List<string>>();

    int levelIndex = 1;
    int nbLevels;

    void Start()
    {
        ReadLevels();
        LoadLevels();
    }

    private void ReadLevels()
    {
        DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory() + "/Assets/map/");
        int nbFile = di.GetFiles().Length;
        Debug.Log("NB File in <" + di.FullName + ">: " + nbFile);
        foreach (FileInfo fi in di.GetFiles())
        {
            Debug.Log("- " + fi.FullName);
            if (fi.FullName.Contains(".meta")) --nbFile;
        }
        Debug.Log("NB File in <" + di.FullName + ">: " + nbFile);

        nbLevels = nbFile;
    }

    public void LoadLevels()
    {
        if (lvl == "") lvl = "lvl" + levelIndex + ".csv";
        ClearLevels();
        ReadCSVFile();
        SpawnMap();
    }

    public void NextLevels()
    {
        levelIndex++;
        lvl = "";
        if(levelIndex <= nbLevels) LoadLevels();
    }

    public void ClearLevels()
    {
        Transform transform = parent.transform;
        for (int i = 0; i < transform.childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        level.Clear();
    }

    public void ReadCSVFile()
    {
        string[] lines = File.ReadAllLines("Assets/map/"+lvl);
        int i = 0;
        foreach(string line in lines)
        {
            List<string> addingList = new List<string>();
            string[] columns = line.Split(',');
            foreach(string column in columns)
            {
                addingList.Add(column);
            }
            level.Add(addingList);
            ++i;
        }
    }
    
    public void SpawnMap()
    {
        int x, y=0;
        foreach(List<string> line in level)
        {
            if(line[0] == "pos")
            {
                GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
                player[0].transform.position = new Vector3(float.Parse(line[1]), 0f, float.Parse(line[2]));
                //spawn player at line[1], line[2]
                //SpawnPlayer(int.Parse(line[1]), int.Parse(line[2]));
            }
            else
            {
                x = 0;
                foreach (string col in line)
                {
                    switch (col)
                    {
                        case "0":
                            //spawn trou
                            break;
                        case "1":
                            SpawnWay(x, y);
                            break;
                        case "2":
                            SpawnWall(x, y);
                            break;
                        case "3":
                            MoveVictoryBox(x, y);
                            break;
                        default:
                            // error
                            break;
                    }
                    ++x;
                }
                ++y;
            }
        }
    }

    public void SpawnWall(int x,int y)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.position = new Vector3(y, 0.5f, x);
        wall.transform.SetParent(parent.transform);
        //GameObject a = Instantiate(wall) as GameObject;
        //a.transform.position=new Vector3(y, 0.5f, x);
    }

    public void SpawnWay(int x, int y)
    {
        GameObject way = GameObject.CreatePrimitive(PrimitiveType.Cube);
        way.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        way.transform.position = new Vector3(y, -0.5f, x);
        way.transform.SetParent(parent.transform);
        //GameObject a = Instantiate(way) as GameObject;
        //a.transform.position=new Vector3(y, -0.5f, x);
    }

    public void MoveVictoryBox(int x, int y)
    {
        GameObject[] VictoryBox = GameObject.FindGameObjectsWithTag("VictoryBox");
        VictoryBox[0].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        VictoryBox[0].transform.position = new Vector3(y, 0.5f, x);
        SpawnWay(x, y);
    }

    public void SpawnVictoryBox(int x, int y)
    {
        GameObject victoryBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
        victoryBox.GetComponent<BoxCollider>().isTrigger = true;
        victoryBox.AddComponent<VictoryBox>();
        victoryBox.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        victoryBox.transform.position = new Vector3(y, 0.5f, x);
        victoryBox.transform.SetParent(parent.transform);
        SpawnWay(x, y);
    }

    public void SpawnPlayer(int x, int y)
    {
        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        player.tag = "Player";
        player.AddComponent<Rigidbody>();
        MouvementBall mV = player.AddComponent<MouvementBall>();
        mV.speedBall = 10;
        player.transform.position = new Vector3(y, 0.5f, x);
        player.transform.SetParent(parent.transform);
        //SpawnWay(x, y);
    }
}
