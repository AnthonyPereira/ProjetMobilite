using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerCanvasMenu : MonoBehaviour
{
    [SerializeField] public Canvas canvasMain;
    [SerializeField] public Canvas canvasLevels;
    [Space(10)]
    [SerializeField] public Button NextButton;
    [SerializeField] public Button PreviousButton;
    [Space(10)]
    [SerializeField] public List<Button> ListButtonLevels;

    private int NumTabLevels;

    private string[] ListLevels;
    private int NbLevels;

    // Start is called before the first frame update
    void Start()
    {
        InitCanvas();
        FileManager FileManager = GameObject.Find("FileManager").GetComponent<FileManager>();
        if(FileManager)
        {
            ListLevels = FileManager.GetListLevels();
            NbLevels = ListLevels.Length;
        }
    }

    public void InitCanvas()
    {
       if(!(canvasMain && canvasLevels)) return;

        canvasMain.enabled = true;
        canvasLevels.enabled = false; 
    }

    public void Continue()
    {
        if(!PlayerPrefs.HasKey("lvl")) PlayerPrefs.SetInt("lvl", 1);

        CrossSceneInformation.Info = PlayerPrefs.GetInt("lvl");
        SceneManager.LoadScene("GameScene");
    }

    public void LoadLevel(int NumLevel)
    {
        if(!PlayerPrefs.HasKey("lvl")) PlayerPrefs.SetInt("lvl", 1);

        int NumMax = PlayerPrefs.GetInt("lvl");

        if(NumLevel <= NumMax)
        {
            CrossSceneInformation.Info = NumLevel;
            SceneManager.LoadScene("GameScene");
        }
    }

    public void PreviousTab()
    {
        if(NumTabLevels > 0) --NumTabLevels;
        GenerateChoiceLevels();
    }

    public void NextTab()
    {
        ++NumTabLevels;
        GenerateChoiceLevels();
    }

    public void ChoiceLevel()
    {
        if(!(canvasMain && canvasLevels)) return;

        canvasMain.enabled = false;
        canvasLevels.enabled = true;

        NumTabLevels = 0;

        GenerateChoiceLevels();
    }

    private void GenerateChoiceLevels()
    {
        ClearChoiceLevel();

        int MaxLvlInTab = NumTabLevels * 8 + 8;

        int Index = NumTabLevels * 8;

        if(Index > 0) PreviousButton.gameObject.SetActive(true);
        if(MaxLvlInTab < NbLevels) NextButton.gameObject.SetActive(true);

        int IndexButton = 0;

        while(Index < NbLevels && Index < MaxLvlInTab)
        {
            string NameLevels = ListLevels[Index].Split(new char[] {'/', '\\'})[3];
            
            string NumLvl = NameLevels.Split(new string[]{"lvl", "."}, System.StringSplitOptions.None)[1];

            Debug.Log(NameLevels + " -> " + NumLvl);

            GameObject ButtonTmp = ListButtonLevels[IndexButton].gameObject;
            if(ButtonTmp)
            {
                ButtonTmp.SetActive(true);
                ListButtonLevels[IndexButton].onClick.AddListener(delegate{LoadLevel(Convert.ToInt32(NumLvl));});
            }

            ++Index;
            ++IndexButton;
        }
    }

    private void ClearChoiceLevel()
    {
        PreviousButton.gameObject.SetActive(false);
        NextButton.gameObject.SetActive(false);

        for(int i = 0; i < 8; i++)
        {
            GameObject ButtonTmp = ListButtonLevels[i].gameObject;
            if(ButtonTmp) ButtonTmp.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}