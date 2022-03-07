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
    [SerializeField] public Canvas canvasPause;
    [SerializeField] public Canvas canvasGame;

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

    public void NextLevels()
    {
        reloadCanvas();
        generateLevels.GetComponent<DataLevels>().NextLevels();
    }

    public void RestartLevels()
    {
        reloadCanvas();
        generateLevels.GetComponent<DataLevels>().ReloadLevels();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
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

        generateLevels.GetComponent<DataLevels>().Victory();
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
