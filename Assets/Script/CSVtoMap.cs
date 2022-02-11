using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVtoMap : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] public string lvl = "lvl1";
   List<List<string>> level;

    void Start(){
        ReadCSVFile();
        SpawnMap();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public static void ReadSCVFile(){
        string[] lines = File.ReadAllLines("../map/"+lvl);
        int i = 0;
        foreach(string line in lines){
            level.Add(List<string>);
            string[] columns = line.Split(',');
            foreach(string column in columns){
                level[i].Add(column);
            }
            ++i;
        }
    }
    
    public static void SpawnMap(){
        foreach(List<string> line in level){
            if(line[0] == "pos"){
                //spawn player at line[1], line[2]
            }
            foreach(string col in line){
                switch(col){
                    case '0':
                        //spawn trou
                        break;
                    case '1':
                        //spawn chemin
                        break;
                    case '2':
                        //spawn mur
                        break;
                    case '3':
                        //spawn victory
                    default:
                        // error
                        break;
                }
            }
        }
    }
}
