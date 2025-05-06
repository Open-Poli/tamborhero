using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{

    public enum DrumType
    {
        LEFT,
        CENTER_LEFT,
        CENTER_RIGHT,
        RIGHT
    }
    
    [SerializeField] private List<Note> notesToHit;

    [SerializeField] private DrumType drumType;
    
    [SerializeField] private GameObject clickVisuals;




    [SerializeField] private float perfectDistance;
    [SerializeField] private float gratDistance;
    [SerializeField] private float badDistance;

    private static float hitVisualTimer = 0.2f;
    
        [SerializeField] private Material hitTextMaterial;
    
    public enum ScoreType
    {
    	MISS = 0,
    	BAD = 50,
    	GREAT = 100,
    	PERFECT = 300,
    }

    // Update is called once per frame

    void Start()
    {
        switch(drumType)
        {
            case DrumType.LEFT:
                GameInput.Instance.OnLeftDrumPressed += Input_OnDrumPressed;
                break;
            case DrumType.CENTER_LEFT:
                GameInput.Instance.OnCenterLeftDrumPressed += Input_OnDrumPressed;
                break;
            case DrumType.CENTER_RIGHT:
                GameInput.Instance.OnCenterRightDrumPressed += Input_OnDrumPressed;
                break;
            case DrumType.RIGHT:
                GameInput.Instance.OnRightDrumPressed += Input_OnDrumPressed;
                break;
        }
    }




    private void Input_OnDrumPressed(object sender, EventArgs e)
    {
        StartCoroutine(ShowVisuals());
        if(HasNote())
        {
            // Clickea y hay nota
            ScoreManager.Instance.AddScore(GetScoreType(), GetBottomNote().IsKiaiMode());
            RemoveNote(GetBottomNote());
        }
        SetHitTextColor();
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Note>(out Note note))
        {
            if(HasNote())
            {
               // Debug.LogError("Ermm.... El tambor está detectando dos notas al mismo tiempo :nerd:");
            }
            notesToHit.Add(note);
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Note>(out Note note))
        {
            //Verifica si la nota está en la lista, o sea, si se "escapó"
            if (notesToHit.Contains(note)) 
            {                            
                ScoreManager.Instance.AddScore(ScoreType.MISS, false);
                RemoveNote(note);
                SetHitTextColor();
            }
            note.DestroySelf();

        }
    }


    private bool HasNote()
    {
        return notesToHit.Count > 0;
    }



    private float GetDistanceToNote()
    {
        return Mathf.Abs(GetBottomNote().transform.position.y - transform.position.y);
    }

    private ScoreType GetScoreType()
    {
        float distance = GetDistanceToNote();
        if (distance <= perfectDistance)
        {
            return ScoreType.PERFECT;
        } 
        if (distance <= gratDistance)
        {
            return ScoreType.GREAT;
        }
        if (distance <= badDistance)
        {
            return ScoreType.BAD;
        }
        return ScoreType.MISS;

    }

    private Note GetBottomNote()
    {
        return notesToHit[0];
    }

    private IEnumerator ShowVisuals()
    {
        clickVisuals.SetActive(true);
        yield return new WaitForSeconds(hitVisualTimer);
        clickVisuals.SetActive(false);
    }

    private void RemoveNote(Note note)
    {
        notesToHit.Remove(note);
        note.HideVisuals();
    }
    
    private void SetHitTextColor()
    {
    	switch(drumType)
        {
            case DrumType.LEFT:
                hitTextMaterial.SetColor("_OutlineColor", new Color(0.32f, 1f, 0.64f, 1f));
                break;
            case DrumType.CENTER_LEFT:
                hitTextMaterial.SetColor("_OutlineColor", new Color(0.32f, 0.57f, 1f, 1f));
                break;
            case DrumType.CENTER_RIGHT:
                hitTextMaterial.SetColor("_OutlineColor", new Color(1f, 0.32f, 0.99f, 1f));
                break;
            case DrumType.RIGHT:
                hitTextMaterial.SetColor("_OutlineColor", new Color(1f, 0.32f, 0.32f, 1f));
                break;
        }
       
        
    }


}
