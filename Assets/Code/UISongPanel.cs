using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class UISongPanel : MonoBehaviour
{
    [SerializeField] private string internalSongName;
    [SerializeField] private string shownSongName;
    [SerializeField] private string artistName;
    [SerializeField] private string duration;
    [SerializeField] private Difficulty difficulty;
    [SerializeField] private RectTransform overlay;
    [SerializeField] private RectTransform video;
    private AudioSource musica;

    private IEnumerator coroutine;
    private const float videoPlayDelay = 2f;



    private const float fadeDuration = 0.25f;




    public enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD,
        VERY_HARD,
        BUSTI
    }


    private void Awake()
    {
        /*
        songNameText.text = shownSongName;
        artistNameText.text = "Por " + artistName;
        difficultyText.text = GetDifficultyText();
        */
        coroutine = PlayVideo();
        musica = FindFirstObjectByType<AudioSource>();
        Deselect();
    }


    public string GetInternalSongName()
    {
        return internalSongName;
    }

    public string GetShownSongName()
    {
        return shownSongName;
    }

    public string GetArtistName()
    {
        return artistName;
    }

    public string GetDuration()
    {
        return duration;
    }
    
    public string GetDifficultyText()
    {
        switch(difficulty)
        {
            case Difficulty.EASY:
                return "Facil";
            case Difficulty.MEDIUM:
                return "Medio";
            case Difficulty.HARD:
                return "Dificil";
            case Difficulty.VERY_HARD:
                return "Imposible";
            default:
                return "";
        }
    }

    public int GetDifficultyIndex()
    {
        return (int)difficulty;
    }

    private void FadeIn(RectTransform element, float opacity)
    {
    	LeanTween.alpha(element, opacity, fadeDuration);
    }
    
    private void FadeOut(RectTransform element)
    {
    	LeanTween.alpha(element, 0, fadeDuration);
    }
    /*

    private void ShowText()
    {
        songNameText.CrossFadeAlpha(1f, fadeDuration, true);
        artistNameText.CrossFadeAlpha(1f, fadeDuration, true);
        difficultyText.CrossFadeAlpha(1f, fadeDuration, true);
    }

    private void HideText()
    {
        songNameText.CrossFadeAlpha(0f, fadeDuration, true);
        artistNameText.CrossFadeAlpha(0f, fadeDuration, true);
        difficultyText.CrossFadeAlpha(0f, fadeDuration, true);
    }
*/
    public void Select()
    {
        musica.volume = 1;
        FadeOut(overlay);
        coroutine = PlayVideo();
        StartCoroutine(coroutine);
      //  ShowText();
    }

    public void Deselect()
    {
        StopCoroutine(coroutine);
        video.GetComponent<RawImage>().enabled = false;
        video.GetComponent<VideoPlayer>().enabled = false;
        FadeIn(overlay, 0.9f);
        FadeOut(video);
      //  HideText();
    }

    private IEnumerator PlayVideo()
    {
        yield return new WaitForSeconds(videoPlayDelay);
        musica.volume = 0;
        video.GetComponent<RawImage>().enabled = true;
        video.GetComponent<VideoPlayer>().enabled = true;
        FadeIn(video, 1f);
    } 

    public Difficulty GetDifficulty()
    {
        return difficulty;
    }


    
    
    
    


}
