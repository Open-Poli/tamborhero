using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreen : MonoBehaviour
{

    [SerializeField] private Material textMaterial;
    [SerializeField] private Color textColor;

    [SerializeField] private TextMeshProUGUI songTitle;
    [SerializeField] private TextMeshProUGUI artistName;


    [SerializeField] private TextMeshProUGUI accuracyText;
    [SerializeField] private TextMeshProUGUI accuracyPercentageText;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreValueText;


    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI maxComboText;
    [SerializeField] private TextMeshProUGUI fullComboText;

    [SerializeField] private GameObject stars;



    

    public static EndScreen Instance;

    private float timeBetweenPoppups = 0.5f;
    private float colorChangeSpeed = 1f;


    private float colorMax = 0.29f;
    private float colorMin = 0.02f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("hay mas de 1 endscreen MMMMMEEEEEEIDEEEBARGA");
        }
    }



    public void ResetStaticData()
    {
        Instance = null;
    }

    public IEnumerator ShowEndScreen()
    {
        songTitle.text = NoteGenerator.shownSongName;
        artistName.text = "por " + NoteGenerator.artistName;
        yield return new WaitForSeconds(NoteGenerator.Instance.GetInitialFallTime() + 2);
        DisableText();
        ScoreManager.Instance.UpdateEndScreen();
        ToRed();
        LeanTween.moveLocalX(gameObject, 0, 1f).setEaseOutQuint();
        yield return new WaitForSeconds(2f);
        accuracyText.enabled = true;
        yield return new WaitForSeconds(timeBetweenPoppups);
        accuracyPercentageText.enabled = true;
        yield return new WaitForSeconds(timeBetweenPoppups);
        comboText.enabled = true;
        yield return new WaitForSeconds(timeBetweenPoppups);
        maxComboText.enabled = true;
        yield return new WaitForSeconds(timeBetweenPoppups);
        scoreText.enabled = true;
        yield return new WaitForSeconds(timeBetweenPoppups);
        scoreValueText.enabled = true;
        yield return new WaitForSeconds(timeBetweenPoppups);
        stars.SetActive(true);
        yield return new WaitForSeconds(timeBetweenPoppups);
        fullComboText.enabled = ScoreManager.Instance.GetFullCombo();
        ScoreManager.Instance.UpdateScores();
        GetComponent<SceneChanger>().enabled = true;
    }
   




   private void ToRed()
   {
        textColor = textMaterial.GetColor("_OutlineColor");
        Color red = new Color (colorMax, colorMin, colorMin, 1f);
        LeanTween.value(gameObject, textColor, red, colorChangeSpeed).setOnUpdate((Color value) => {
            textMaterial.SetColor("_OutlineColor", value);
        }).setOnComplete(ToYellow);
   }

   private void ToYellow()
   {
        textColor = textMaterial.GetColor("_OutlineColor");
        Color yellow = new Color (colorMax, colorMax, colorMin, 1f);
        LeanTween.value(gameObject, textColor, yellow, colorChangeSpeed).setOnUpdate((Color value) => {
            textMaterial.SetColor("_OutlineColor", value);
        }).setOnComplete(ToGreen);
   }

   private void ToGreen()
   {
        textColor = textMaterial.GetColor("_OutlineColor");
        Color green = new Color (colorMin, colorMax, colorMin, 1f);
        LeanTween.value(gameObject, textColor, green, colorChangeSpeed).setOnUpdate((Color value) => {
            textMaterial.SetColor("_OutlineColor", value);
        }).setOnComplete(ToCyan);
   }

   private void ToCyan()
   {
        textColor = textMaterial.GetColor("_OutlineColor");
        Color cyan = new Color (colorMin, colorMax, colorMax, 1f);
        LeanTween.value(gameObject, textColor, cyan, colorChangeSpeed).setOnUpdate((Color value) => {
            textMaterial.SetColor("_OutlineColor", value);
        }).setOnComplete(ToBlue);
   }

   private void ToBlue()
   {
        textColor = textMaterial.GetColor("_OutlineColor");
        Color blue = new Color (colorMin, colorMin, colorMax, 1f);
        LeanTween.value(gameObject, textColor, blue, colorChangeSpeed).setOnUpdate((Color value) => {
            textMaterial.SetColor("_OutlineColor", value);
        }).setOnComplete(ToPurple);
   }

   private void ToPurple()
   {
        textColor = textMaterial.GetColor("_OutlineColor");
        Color purple = new Color (colorMax, colorMin, colorMax, 1f);
        LeanTween.value(gameObject, textColor, purple, colorChangeSpeed).setOnUpdate((Color value) => {
            textMaterial.SetColor("_OutlineColor", value);
        }).setOnComplete(ToRed);
   }


    private void DisableText()
    {
        accuracyText.enabled = false;
        accuracyPercentageText.enabled = false;
        comboText.enabled = false;
        maxComboText.enabled = false;
        scoreText.enabled = false;
        scoreValueText.enabled = false;
    }




}
