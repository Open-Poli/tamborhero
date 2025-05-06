using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float noteSpeed;


    public static GameManager Instace;

    [SerializeField] private VideoPlayerController videoController;


    void Awake()
    {
        if(Instace == null)
        {
            Instace = this;
        }
        else 
        {
            Debug.LogError("Ummm... Hay dos GameManagers en esta escena :robot:");
        }
    }

    public void StartGame()
    {
        NoteGenerator.Instance.enabled = true;

        StartCoroutine(MusicPlayer.Instance.PlayMusic(NoteGenerator.Instance.GetSongPlayDelay()));
        ScoreManager.Instance.ShowAccuracyText();

        StartCoroutine(videoController.PlayVideo(NoteGenerator.Instance.GetSongPlayDelay()));
        
        
        
    }

    public void ResetStaticData()
    {
        Instace = null;
    }

}
