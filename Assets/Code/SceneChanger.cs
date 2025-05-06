using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{



    [SerializeField] private string sceneName;
    private string songName;
    private string artistName;
    [SerializeField] private GameObject loadingObject;

    private float timeBetweenCenterDrums = 0.12f;

    private float DDrumTimer = 0;
    private float KDrumTimer = 0;

    private bool hasRecentlyHitD = false;
    private bool hasRecentlyHitK = false;


    void Start()
    {
        GameInput.Instance.OnCenterLeftDrumPressed += Input_OnCenterLeftDrumPressed;
        GameInput.Instance.OnCenterRightDrumPressed += Input_OnCenterRightDrumPressed;
    }

    private void Input_OnCenterLeftDrumPressed(object sender, EventArgs e)
    {
        if(hasRecentlyHitK)
        {
            LoadCorrectScene();
            
        }
        hasRecentlyHitD = true;
        DDrumTimer = timeBetweenCenterDrums;
    }

    private void Input_OnCenterRightDrumPressed(object sender, EventArgs e)
    {
        if(hasRecentlyHitD)
        {
            LoadCorrectScene();
        }
        hasRecentlyHitK = true;
        KDrumTimer = timeBetweenCenterDrums;
    }

    void Update()
    {
        if(hasRecentlyHitD)
        {
            DDrumTimer -= Time.deltaTime;
            if(DDrumTimer <= 0f)
            {
                hasRecentlyHitD = false;
            }
        }

        if(hasRecentlyHitK)
        {
            KDrumTimer -= Time.deltaTime;
            if(KDrumTimer <= 0f)
            {
                hasRecentlyHitK = false;
            }
        }
    }

    void LoadCorrectScene()
    {
        loadingObject.SetActive(true);
        if(sceneName == "MainMenu")
        {
            StaticDataResetter resetter = GetComponent<StaticDataResetter>();
            resetter.ResetAllStaticData();
            resetter.ReturnToMenu();
            GameInput.Instance.PrenderTambores();
        }else if(sceneName == "menu"){
            StaticDataResetter resetter = GetComponent<StaticDataResetter>();
            resetter.ResetAllStaticData();
            resetter.ReturnToMenu();
        }
        else
        {
            NoteGenerator.LoadGameScene(sceneName, songName, artistName);
            GameInput.Instance.ApagarTambores();
        }
    }

    public void SetSceneToLoadName(string name)
    {
        sceneName = name;
    }

    public void SetSongName(string name)
    {
        songName = name;
    }

    public void SetArtistName(string name)
    {
        artistName = name;
    }

}
