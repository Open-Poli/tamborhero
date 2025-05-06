using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDrum : MonoBehaviour
{
    [SerializeField] private GameObject clickVisuals;


    [SerializeField] private Drum.DrumType drumType;

    private static float hitVisualTimer = 0.15f;


    void Start()
    {
        switch(drumType)
        {
            case Drum.DrumType.LEFT:
                GameInput.Instance.OnLeftDrumPressed += Input_OnDrumPressed;
                break;
            case Drum.DrumType.CENTER_LEFT:
                GameInput.Instance.OnCenterLeftDrumPressed += Input_OnDrumPressed;
                break;
            case Drum.DrumType.CENTER_RIGHT:
                GameInput.Instance.OnCenterRightDrumPressed += Input_OnDrumPressed;
                break;
            case Drum.DrumType.RIGHT:
                GameInput.Instance.OnRightDrumPressed += Input_OnDrumPressed;
                break;
        }
    }




    private IEnumerator ShowVisuals()
    {
        clickVisuals.SetActive(true);
        yield return new WaitForSeconds(hitVisualTimer);
        clickVisuals.SetActive(false);
    }

    private void Input_OnDrumPressed(object sender, EventArgs e)
    {
        StopCoroutine(ShowVisuals());
        StartCoroutine(ShowVisuals());
    }








}
