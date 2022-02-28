using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerCanvas : MonoBehaviour
{
    [SerializeField] public GameObject generateLevels;
    [Space(10)]
    [Header("Game Canvas")]
    [SerializeField] public Canvas canvasVictory;
    [SerializeField] public Canvas canvasLose;
    [SerializeField] public Canvas canvasPause;
    [SerializeField] public Canvas canvasGame;

    [Space(10)]
    [Header("Menu Canvas")]
    [SerializeField] public Canvas canvasMain;
    [SerializeField] public Canvas canvasLevels;

    private bool IsPause = false;

    private void reloadCanvas()
    {
        IsPause = false;
        Time.timeScale = 1;

        if(canvasGame)
        {
            canvasGame.enabled = true;
            canvasLose.enabled = false;
            canvasVictory.enabled = false;
            canvasPause.enabled = false;
        }
    }

    void Start()
    {
        reloadCanvas();
    }

    public void LaunchLevel(int num)
    {   
        
        if(!PlayerPrefs.HasKey("lvl")){
            PlayerPrefs.SetInt("lvl", 1);
        }
        if(num == 0){
            num = PlayerPrefs.GetInt("lvl");
        }
        if(num <= PlayerPrefs.GetInt("lvl")){
            CrossSceneInformation.Info = num.ToString();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        else{
            SceneManager.LoadScene(0);
        }
        
    }

    public void NextLevels()
    {
        reloadCanvas();
        generateLevels.GetComponent<CSVtoMap>().NextLevels();
    }

    public void RestartLevels()
    {
        reloadCanvas();
        generateLevels.GetComponent<CSVtoMap>().LoadLevels();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        //GameObject.Find("MainMenu").SetActive(true);
        //GameObject.Find("LvlMenu").SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Victory()
    {
        Time.timeScale = 0;
        canvasVictory.enabled = true;
        canvasGame.enabled = false;
    }

    public void Lose()
    {
        Time.timeScale = 0;
        canvasLose.enabled = true;
        canvasGame.enabled = false;
    }

    public void ClickPause()
    {
        if(IsPause)
        {
            reloadCanvas();
        }
        else
        {
            Time.timeScale = 0;
            canvasPause.enabled = true;
            IsPause = true;
        }
    }

    public void QuitPause()
    {
        reloadCanvas();
    }
}
