using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerCanvas : MonoBehaviour
{
    [SerializeField] public GameObject generateLevels;
    [Space(10)]
    [SerializeField] public Canvas canvasVictory;
    [SerializeField] public Canvas canvasLose;

    public void NextLevels()
    {
        canvasVictory.enabled = false;
        generateLevels.GetComponent<CSVtoMap>().NextLevels();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Victory()
    {
       canvasVictory.enabled = true;
    }
}
