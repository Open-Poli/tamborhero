using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;
using System.Collections;

public class VideoPlayerController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    [SerializeField] private List<VideoClip> videos;



    // Función pública para reproducir el video


    public IEnumerator PlayVideo(float videoDelay)
    {
        SetVideo();
        Color colorActual = Color.black;
        Color nuevoColor = Color.white;

        yield return new WaitForSeconds(videoDelay - GetVideoDelay());

        if (videoPlayer != null)
        {
            //fade in

            videoPlayer.Play();
            LeanTween.value(gameObject, SetBackgroundColor, colorActual, nuevoColor, 2f);
        }
        else
        {
            Debug.LogError("No se encontró el componente VideoPlayer.");
        }

    }

    private void SetBackgroundColor(Color newColor)
    {
        videoPlayer.GetComponent<SpriteRenderer>().color = newColor;
    }

    private void SetVideo()
    {
    
        try
        {
        	videoPlayer.clip = videos[SongIds.GetSongID()];
        }
        catch
        {
            videoPlayer.clip = videos[0];
        }
    }

    private float GetVideoDelay()
    {
        switch(SongIds.GetSongID())
        {
            case 0: //ratatata
                return 0.7f;

            case 1: //gigachad
                return 0.7f;

            case 2: // medieburger
                return 0.7f;

            case 3: // ligera
                return 0.7f;

            case 4: //girl
                return 0.5f;

            case 5: // hero
                return 0.5f;

            case 6: // partofme
                return 0.5f;

            case 7: // believer
                return 0.3f;
            
            case 8: //rasputin
                return 0.7f;

            case 9: // magica
                return 0.7f;

            case 10: // elHijoDeHernandez
                return 0.7f;

            case 11: //takeonme
                return 0.5f;

            case 12: // despacito
                return 0.5f;

            case 13: //haiyorokonde
                return 0.5f;

            case 14: // noting
                return 0.5f;

            case 15: // toxicity
                return 0.5f;

            case 16: //reptilia
                return 0.5f;
        }

        return 0;
    }

}
