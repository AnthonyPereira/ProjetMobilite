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
    [SerializeField] public Sprite StarComplete;
    [SerializeField] public Sprite StarEmpty;
    [Space(10)]
    [SerializeField] public List<Button> ListButtonLevels;

    private int NumTabLevels;

    //private string[] ListLevels;
    private int NbLevels;

    // Start is called before the first frame update
    void Start()
    {
        InitCanvas();
        FileManager FileManager = GameObject.Find("FileManager").GetComponent<FileManager>();
        if(FileManager)
        {
            //ListLevels = FileManager.GetListLevels();
            NbLevels = FileManager.GetListLevels();
        }
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
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
        Debug.Log(NumLevel);
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
            //string NameLevels = ListLevels[Index].Split(new char[] {'/', '\\'})[3];
            
            //string NumLvl = NameLevels.Split(new string[]{"lvl", "."}, System.StringSplitOptions.None)[1];

            //Debug.Log(NameLevels + " -> " + NumLvl);

            GameObject ButtonTmp = ListButtonLevels[IndexButton].gameObject;
            if(ButtonTmp)
            {
                int Num = Index + 1;
                ButtonTmp.SetActive(true);
                ListButtonLevels[IndexButton].onClick.RemoveAllListeners();
                ListButtonLevels[IndexButton].onClick.AddListener(delegate{LoadLevel(Num);});

                string NameData = "lvl" + Num;
                if(PlayerPrefs.HasKey(NameData))
                {
                    string Name = ListButtonLevels[IndexButton].name;
                    int NbStar = PlayerPrefs.GetInt(NameData);
                    GenerateStar(Name, NbStar);
                }
            }

            ++Index;
            ++IndexButton;
        }
    }

    private void GenerateStar(string Name, int NbStar)
    {
        for(int i = 1; i <= NbStar; ++i) {
            GameObject Etoile = GameObject.Find(Name + "/Etoile" + i);
            if(Etoile)
            {
                Image Img = Etoile.GetComponent<Image>();
                Img.sprite = StarComplete;
            }
        }
    }

    private void ClearChoiceLevel()
    {
        PreviousButton.gameObject.SetActive(false);
        NextButton.gameObject.SetActive(false);

        for(int i = 0; i < 8; i++)
        {
            // Clear Star in Levels
            string Name = ListButtonLevels[i].name;
            for(int j = 1; j <= 3; ++j) {
                Debug.Log(Name + "/Etoile" + j);
                GameObject Etoile = GameObject.Find(Name + "/Etoile" + j);
                if(Etoile)
                {
                    Image Img = Etoile.GetComponent<Image>();
                    Img.sprite = StarEmpty;
                }
            }

            GameObject ButtonTmp = ListButtonLevels[i].gameObject;
            if(ButtonTmp) ButtonTmp.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}