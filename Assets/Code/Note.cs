using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer overlay;
    
    [SerializeField] private bool kiaiMode;

    public Sprite greenNote;
    public Sprite blueNote;
    public Sprite purpleNote;
    public Sprite redNote;

    public Sprite greenGold;
    public Sprite blueGold;
    public Sprite purpleGold;
    public Sprite redGold;
    
    public enum NoteType
    {
    	GREEN,
    	BLUE,
    	PURPLE,
    	RED
    }
    
    
    
    [SerializeField] private NoteType noteType;
   
    
    void Awake()
    {
       // UpdateColor();
       // SetGoldOverlay();
        
        
    }



    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void SetGoldOverlay()
    {
        switch(noteType)
        {
        	case NoteType.RED:
        		overlay.sprite = redGold;
        		break;
        	case NoteType.BLUE:
        		overlay.sprite = blueGold;
        		break;
        	case NoteType.GREEN:
        		overlay.sprite = greenGold;
        		break;
        	case NoteType.PURPLE:
        		overlay.sprite = purpleGold;
                break;	
        }
    }
    


    public void SetNoteType(NoteType newType)
    {
        noteType = newType;
        UpdateColor();
        SetGoldOverlay();
    }

    public void UpdateColor()
    {
        switch(noteType)
        {
        	case NoteType.RED:
        		GetComponent<SpriteRenderer>().sprite = redNote;
        		break;
        	case NoteType.BLUE:
        		GetComponent<SpriteRenderer>().sprite = blueNote;
        		break;
        	case NoteType.GREEN:
        		GetComponent<SpriteRenderer>().sprite = greenNote;
        		break;
        	case NoteType.PURPLE:
        		GetComponent<SpriteRenderer>().sprite = purpleNote;
                break;	
        }
    }

   

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    
    public float GetSpeed()
    {
    	return speed;
    }

    public void HideVisuals()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        overlay.enabled = false;
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
    
    public bool IsKiaiMode()
    {
    	return kiaiMode;
    }
    
    public void SetKiaiMode()
    {
    	kiaiMode = true;
    	overlay.enabled = true;
    }
}
