using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MusicPlayer : MonoBehaviour
{
    
    [SerializeField] private AudioSource music;
    
    [SerializeField] private GameObject video;
    
    [SerializeField] private List<AudioClip> songs;

    
    
    
    public static MusicPlayer Instance;
    
    
    
    private void Awake()
    {
    
    
    	if (Instance == null)
    	{
    		Instance = this;
    	}
    	else
    	{
    		Debug.LogError("Hay mas de 1 music player :v");
    	}
    	
    	
        music.clip.LoadAudioData();
    }
    
    void Start()
    {
        GetSong();
    }
    
    
    
   
    
    public IEnumerator PlayMusic(float delay)
    {
        video.SetActive(true);
    	yield return new WaitForSeconds(delay);
        //throw new System.Exception("shupa");
    	music.Play();
        
    }
    
    
    private void GetSong()
    {
    
        try
        {
        	music.clip = songs[SongIds.GetSongID()];
        }
        catch
        {
               music.clip = songs[0];
        }
    }
    
    


   public void ResetStaticData()
   {
       Instance = null;
   }
    
}
