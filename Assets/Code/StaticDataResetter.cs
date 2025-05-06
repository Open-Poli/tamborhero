using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticDataResetter : MonoBehaviour
{
    public void ResetAllStaticData()
    {
        GameManager.Instace.ResetStaticData();
        MusicPlayer.Instance.ResetStaticData();
        NoteGenerator.Instance.ResetStaticData();
        ScoreManager.Instance.ResetStaticData();
        EndScreen.Instance.ResetStaticData();
        GameInput.Instance.ResetStaticData();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
