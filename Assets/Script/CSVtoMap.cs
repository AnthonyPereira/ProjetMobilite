using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVtoMap : MonoBehaviour
{
    [SerializeField] public string lvl;
    [SerializeField] public GameObject parent;

    [SerializeField] public GameObject Hole;
    [SerializeField] public GameObject Wall;

    [SerializeField] public GameObject Ground;


    List<List<string>> level = new List<List<string>>();

    int levelIndex = 1;
    int nbLevels;
    int SizeX, SizeY;

    void Start()
    {
        nbLevels = Resources.LoadAll("map/").Length;
        LoadLevels();
    }

    

    public void LoadLevels()
    {
        if (lvl == "") lvl = "lvl" + levelIndex;
        ClearLevels();
        ReadCSVFile();
        SpawnMap();
        SizeX = 0;
        SizeY = 0;
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
        Debug.Log("Iciv");

        TextAsset csv =new TextAsset();
        try
        {
            csv = Resources.Load<TextAsset>("map/"+lvl);

        }
        catch (System.Exception)
        {
            Debug.Log("erreur");
        }

        string[] lines = csv.text.Split(new char[] { '\n' });
        int i = 0;
        foreach(string line in lines)
        {
            List<string> addingList = new List<string>();
            string[] columns = line.Split(new char[] { ',' });
            if(columns.Length > SizeX)
            {
                SizeX = columns.Length;
            }
            foreach(string column in columns)
            {
                addingList.Add(column);
            }
            level.Add(addingList);
            ++i;
        }
        SizeY = i;
    }
    
    public void SpawnMap()
    {
        float x, y=0;
        SizeX = SizeX / 2 -1;
        SizeY = SizeY / 2 -1;

        foreach (List<string> line in level)
        {
            if(line[0] == "pos")
            {
                GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
                player[0].transform.position = new Vector3(float.Parse(line[1])-SizeY, 0f, float.Parse(line[2])-SizeX);
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
                            SpawnHole(x - SizeX, y - SizeY);
                            break;
                        case "1":
                            SpawnWay(x-SizeX, y-SizeY);
                            break;
                        case "2":
                            SpawnWall(x - SizeX, y - SizeY);
                            break;
                        case "3":
                            MoveVictoryBox(x - SizeX, y - SizeY);
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

    public void SpawnWall(float x,float y)
    {
        GameObject wall = Instantiate(Wall);
        wall.transform.position = new Vector3(y, 0.5f, x);
        wall.transform.SetParent(parent.transform);
    }

    public void SpawnHole(float x, float y)
    {
        GameObject a = Instantiate(Hole);
        a.transform.position=new Vector3(y, -0.5f, x);
        a.transform.SetParent(parent.transform);
    }

    public void SpawnWay(float x, float y)
    {
        GameObject a = Instantiate(Ground) ;
        a.transform.position=new Vector3(y, -0.5f, x);
        a.transform.SetParent(parent.transform);
    }

    public void MoveVictoryBox(float x, float y)
    {
        GameObject[] VictoryBox = GameObject.FindGameObjectsWithTag("VictoryBox");
        VictoryBox[0].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        VictoryBox[0].transform.position = new Vector3(y, 0.5f, x);
        SpawnWay(x, y);
    }

    public void SpawnVictoryBox(float x, float y)
    {
        GameObject victoryBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
        victoryBox.GetComponent<BoxCollider>().isTrigger = true;
        victoryBox.AddComponent<VictoryBox>();
        victoryBox.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        victoryBox.transform.position = new Vector3(y, 0.5f, x);
        victoryBox.transform.SetParent(parent.transform);
        SpawnWay(x, y);
    }

    public void SpawnPlayer(float x, float y)
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
