using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerCanvas : MonoBehaviour
{
    [SerializeField] public GameObject generateLevels;
    [Space(10)]
    [SerializeField] public Canvas canvasVictory;
    [SerializeField] public Canvas canvasLose;

    public void LaunchLevel(int num)
    {
        CrossSceneInformation.Info = num.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void NextLevels()
    {
        canvasVictory.enabled = false;
        generateLevels.GetComponent<CSVtoMap>().NextLevels();
    }

    public void RestartLevels()
    {
        canvasLose.enabled = false;
        generateLevels.GetComponent<CSVtoMap>().LoadLevels();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        GameObject.Find("MainMenu").SetActive(true);
        GameObject.Find("LvlMenu").SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Victory()
    {
       canvasVictory.enabled = true;
    }

    public void Lose()
    {
        canvasLose.enabled = true;
    }
}
