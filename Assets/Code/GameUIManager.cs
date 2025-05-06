using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private List<UISongPanel> songPanels;
    [SerializeField] private int selectedSongID;

    [SerializeField] private TextMeshProUGUI songNameText;
    [SerializeField] private TextMeshProUGUI artistNameText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private TextMeshProUGUI durationText;

    [SerializeField] private Image fondo;

    [SerializeField] private List<Color> difficultyColors;

    [SerializeField] private Image skull;


    [Header("Top Puntos")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI silverText;
    [SerializeField] private TextMeshProUGUI bronzeText;


    private SceneChanger sceneChanger;


    private const float scrollDuration = 0.25f;

    private float xMovement = 352;

    private bool canMove;

    void Awake()
    {
        sceneChanger = GetComponent<SceneChanger>();
        sceneChanger.SetSceneToLoadName(GetSelectedSongPanel().GetInternalSongName());
        sceneChanger.SetSongName(GetSelectedSongPanel().GetShownSongName());
        sceneChanger.SetArtistName(GetSelectedSongPanel().GetArtistName());
        UpdateTopScores();
    }

    private void Start()
    {
        canMove = true;
        GetSelectedSongPanel().Select();

        GameInput.Instance.OnLeftDrumPressed += Instance_OnLeftDrumPressed;
        GameInput.Instance.OnRightDrumPressed += Instance_OnRightDrumPressed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateTopScores();
        }
    }

    private void Instance_OnLeftDrumPressed(object sender, EventArgs e)
    {
        if(canMove)
        {
            canMove = false;
            ShrinkSelectedPanel();
            DecreaseSelectedSongID();
            EnlargeSelectedPanel();
        }
    }


    private void Instance_OnRightDrumPressed(object sender, EventArgs e)
    {
        if(canMove)
        {
            canMove = false;
            ShrinkSelectedPanel();
            IncreaseSelectedSongID();
            EnlargeSelectedPanel();
        }
    }


    public void IncreaseSelectedSongID()
    {
        GetSelectedSongPanel().Deselect();
        selectedSongID = (selectedSongID + 1) % songPanels.Count;
        sceneChanger.SetSceneToLoadName(GetSelectedSongPanel().GetInternalSongName());
        sceneChanger.SetSongName(GetSelectedSongPanel().GetShownSongName());
        sceneChanger.SetArtistName(GetSelectedSongPanel().GetArtistName());

        GetSelectedSongPanel().Select();

        MoveAllPanelsLeft();
        StartCoroutine(TurnPanels());
        ChangeBackgroundColor();

    }

    public void DecreaseSelectedSongID()
    {
        GetSelectedSongPanel().Deselect();
        selectedSongID = (selectedSongID + songPanels.Count - 1) % songPanels.Count;
        sceneChanger.SetSceneToLoadName(GetSelectedSongPanel().GetInternalSongName());
        sceneChanger.SetSongName(GetSelectedSongPanel().GetShownSongName());
        sceneChanger.SetArtistName(GetSelectedSongPanel().GetArtistName());
        
        GetSelectedSongPanel().Select();

        MoveAllPanelsRight();
        StartCoroutine(TurnPanels());  
        ChangeBackgroundColor();

    }

    private UISongPanel GetSelectedSongPanel()
    {
        return songPanels[selectedSongID];
    }

    private void UpdateTitleCard()
    {
        songNameText.text = GetSelectedSongPanel().GetShownSongName();
        artistNameText.text = GetSelectedSongPanel().GetArtistName();
        difficultyText.text = GetSelectedSongPanel().GetDifficultyText();
        skull.enabled = GetSelectedSongPanel().GetDifficulty() == UISongPanel.Difficulty.BUSTI;
        durationText.text = GetSelectedSongPanel().GetDuration();
        UpdateTopScores();
    }
    private void ShowText()
    {
        songNameText.CrossFadeAlpha(1f, scrollDuration, true);
        artistNameText.CrossFadeAlpha(1f, scrollDuration, true);
        difficultyText.CrossFadeAlpha(1f, scrollDuration, true);
        durationText.CrossFadeAlpha(1f, scrollDuration, true);
        skull.CrossFadeAlpha(1f, scrollDuration, true);
        goldText.CrossFadeAlpha(1f, scrollDuration, true);
        silverText.CrossFadeAlpha(1f, scrollDuration, true);
        bronzeText.CrossFadeAlpha(1f, scrollDuration, true);
    }
    private void HideText()
    {
        songNameText.CrossFadeAlpha(0f, scrollDuration, true);
        artistNameText.CrossFadeAlpha(0f, scrollDuration, true);
        difficultyText.CrossFadeAlpha(0f, scrollDuration, true);
        durationText.CrossFadeAlpha(0f, scrollDuration, true);
        skull.CrossFadeAlpha(0f, scrollDuration, true);
        goldText.CrossFadeAlpha(0f, scrollDuration, true);
        silverText.CrossFadeAlpha(0f, scrollDuration, true);
        bronzeText.CrossFadeAlpha(0f, scrollDuration, true);
    }
    private void MoveAllPanelsLeft()
    {
        UISongPanel left = GetFurthestLeftPanel();
        foreach(UISongPanel panel in songPanels)
        {     
            if(panel != left)
            {
                LeanTween.moveLocalX(panel.gameObject, panel.GetComponent<RectTransform>().anchoredPosition.x - xMovement, scrollDuration).setOnComplete(AllowMovement);
            }   
        }
        left.GetComponent<RectTransform>().anchoredPosition = new Vector2 (xMovement * (songPanels.Count - 3), 0);
    }
    private void MoveAllPanelsRight()
    {
        UISongPanel right = GetFurthestRightPanel();
        foreach(UISongPanel panel in songPanels)
        {        
            if(panel != right)
            {
                LeanTween.moveLocalX(panel.gameObject, panel.GetComponent<RectTransform>().anchoredPosition.x + xMovement, scrollDuration).setOnComplete(AllowMovement);
            }
        }
        right.GetComponent<RectTransform>().anchoredPosition = new Vector2((xMovement * -2), 0);
    }

    private void AllowMovement()
    {
        canMove = true;
    }

    private void ShrinkSelectedPanel()
    {
        LeanTween.scale(GetSelectedSongPanel().gameObject, new Vector3(0.75f, 0.75f, 1f), scrollDuration);
    }

    private void EnlargeSelectedPanel()
    {
        LeanTween.scale(GetSelectedSongPanel().gameObject, new Vector3(1f, 1f, 1f), scrollDuration);
    }

    private UISongPanel GetFurthestLeftPanel()
    {
        return songPanels[(selectedSongID - 3 + songPanels.Count) % songPanels.Count];
    }

    private UISongPanel GetFurthestRightPanel()
    {
        return songPanels[(selectedSongID + (songPanels.Count - 2)) % songPanels.Count];
    }

    public IEnumerator TurnPanels()
    {
        HideText();
        yield return new WaitForSeconds(scrollDuration);
        UpdateTitleCard();
        ShowText();
    }


    private void ChangeBackgroundColor()
    {
        
        Color colorActual = fondo.color;
        Color nuevoColor = difficultyColors[GetSelectedSongPanel().GetDifficultyIndex()];
        LeanTween.value(gameObject, SetBackgroundColor, colorActual, nuevoColor, scrollDuration);
    
    }
    private void SetBackgroundColor(Color newColor)
    {
        
        fondo.color = newColor;
        
    }

    private void UpdateTopScores()
    {
        string caminoArchivo = Application.dataPath + "/Scores/" + GetSelectedSongPanel().GetInternalSongName() + "Scores.txt";

        goldText.text = GetPlayerScoreText(1);
        silverText.text = GetPlayerScoreText(2);
        bronzeText.text = GetPlayerScoreText(3);
    }

    private string GetPlayerScoreText(int top)
    {
        string[] lineas = File.ReadAllLines(Application.dataPath + "/Scores/" + GetSelectedSongPanel().GetInternalSongName() + "Scores.txt");

        return lineas[top + 2] + "\n" + lineas[top - 1] + "%";
    }



    
    
    
}
