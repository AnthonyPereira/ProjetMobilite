using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVtoMap : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] public string lvl;
   List<List<string>> level = new List<List<string>>();

    void Start(){
        lvl = "lvl1.csv";
        ReadCSVFile();
        SpawnMap();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void ReadCSVFile(){
        string[] lines = File.ReadAllLines("Assets/map/"+lvl);
        int i = 0;
        foreach(string line in lines){
            List<string> addingList = new List<string>();
            string[] columns = line.Split(',');
            foreach(string column in columns){
                addingList.Add(column);
            }
            level.Add(addingList);
            ++i;
        }
    }
    
    public void SpawnMap(){
        int x, y=0;
        foreach(List<string> line in level){
            if(line[0] == "pos"){
                GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
                player[0].transform.position = new Vector3(float.Parse(line[1]), 0, float.Parse(line[2]));
                //spawn player at line[1], line[2]
            }
            x=0;
            foreach(string col in line){
                switch(col){
                    case "0":
                        //spawn trou
                        break;
                    case "1":
                        //spawn chemin
                        break;
                    case "2":
                        SpawnWall(x,y);
                        break;
                    case "3":
                        //spawn victory
                    default:
                        // error
                        break;
                }
                ++x;
            }
            ++y;
        }
    }

    public void SpawnWall(int x,int y){
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.position = new Vector3(y, 0.5f, x);
        GameObject a = Instantiate(wall) as GameObject;
        a.transform.position=new Vector3(y, 0.5f, x);
    }
}
