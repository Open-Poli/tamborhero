using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.IO.Ports;

public class NoteGenerator : MonoBehaviour
{





    [SerializeField] private bool shouldPlay;
    [SerializeField] private static string songName;
    public static string shownSongName;
    public static string artistName;

    [SerializeField] private string songNameTest;
    [SerializeField] private Note notePrefab;
    [SerializeField] private Bar barPrefab;
    [SerializeField] private List<Transform> drumPositions;
    [SerializeField] private bool kiaiMode;

    private bool staticPart;
    private bool shouldEnd;

    [SerializeField] private ParticleSystem particulasIzquierda;
    [SerializeField] private ParticleSystem particulasDerecha;

    private int hardNoteCounter;

    private string basePath = Application.dataPath + "/Text/";

    private string[] lineas;



    private int bpm;
    private int tempoCreoNoSe; //no se si se llama asi pero esta bien usado
    private float timeBetweenLines;
    private int contador;
    private float timer;

    private int incioBarra;

    private float initialFallTime;

    private float songPlayDelay;




    private float noteSpawnYPosition = 6;
    private float drumDepth = 4f;


    public static NoteGenerator Instance;

    private void Awake()
    {

        if (!shouldPlay)
        {
            return;
        }

        notePrefab.SetSpeed(7);
        barPrefab.SetSpeed(7);
        initialFallTime = GetFallTime();

        kiaiMode = false;
        staticPart = false;

        if (songNameTest != "")
        {
            songName = songNameTest;
        }


        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Hay mas de 1 generador de notas medie!!!!!!!");
        }

        timer = 0f;
        contador = 3;
        incioBarra = -1;

        lineas = ReadFile();

        bpm = Int32.Parse(lineas[0]);

        tempoCreoNoSe = Int32.Parse(lineas[1]);

        timeBetweenLines = ((float)60 / bpm / tempoCreoNoSe);

        songPlayDelay = Single.Parse(lineas[2]);

        SetParticles(false);

        Instance.enabled = false;



    }




    private void Update()
    {
        if (!shouldPlay)
        {
            return;
        }

        timer += Time.deltaTime;

        if (contador >= lineas.Length)
        {
            // Se termino el archivo
            StartCoroutine(EndScreen.Instance.ShowEndScreen());
            enabled = false;
        }
        else
        {
            if (timer >= timeBetweenLines)
            {
                ReadText();
                timer -= timeBetweenLines;
            }
        }




    }


    private void SetParticles(bool active)
    {
        if(active)
        {
            particulasIzquierda.Play();
            particulasDerecha.Play();
        }
        else
        {
            particulasIzquierda.Stop();
            particulasDerecha.Stop();
        }
    }




    public string[] ReadFile()
    {
        string[] lines = File.ReadAllLines(basePath + songName + ".txt");
        return lines;
    }

    public void ReadText()
    {
        if(incioBarra != -1 && (contador - incioBarra) % (tempoCreoNoSe * 2) == 0)//cambiar si se agregan mas linas al principio
        {
            SpawnBar();
        }
        foreach (char character in lineas[contador])
        {
            switch (character)
            {
                case 'S':

                    SpawnNote(Note.NoteType.GREEN);
                    break;

                case 'D':

                    SpawnNote(Note.NoteType.BLUE);
                    break;

                case 'K':
                    SpawnNote(Note.NoteType.PURPLE);
                    break;

                case 'L':
                    SpawnNote(Note.NoteType.RED);
                    break;

                case 's':
                    TrySpawnHardNote(Note.NoteType.GREEN);
                    break;
                case 'd':
                    TrySpawnHardNote(Note.NoteType.BLUE);
                    break;
                case 'k':
                    TrySpawnHardNote(Note.NoteType.PURPLE);
                    break;
                case 'l':
                    TrySpawnHardNote(Note.NoteType.RED);
                    break;

                case '3':
                    timeBetweenLines = ((float)60 / bpm / 3);
                    break;

                case '4':

                    timeBetweenLines = ((float)60 / bpm / 4);
                    break;

                case '6':
                    timeBetweenLines = ((float)60 / bpm / 6);
                    break;
                    
                case '8':
                	timeBetweenLines = ((float)60 / bpm / 8);
                	break;

                case '=':
                    SetNoteAndBarSpeed(7);
                    break;

                case ')':

                    SetNoteAndBarSpeed(9);
                    break;

                case 'ß':

            
                    SetNoteAndBarSpeed(12);             	
                    break;      

                case '?':

                    SetNoteAndBarSpeed(13);
                    break;

                case '*':
                        
                    kiaiMode = !kiaiMode;
                    SetParticles(kiaiMode);
                        
                    break;
                case '_':
                    incioBarra = contador;
                    SpawnBar();
                    break;
                case '!':
                	incioBarra = -1; // Med
                	break;
                case '{':
                    bpm+=1; //unicamente para About a Girl que va acelerandose
                    timeBetweenLines = ((float)60 / bpm / 4);
                    break;
                case '}':
                    bpm-=1;
                    timeBetweenLines = ((float)60 / bpm / 4);
                    break;
                case 'ø': //Altgr + o
                    timer += timeBetweenLines / 2;
                    break;
                case '[':
                    staticPart = true;
                    break;
                case ']':
                    staticPart = false;
                    break;
                case '~':
                    contador = 99999;
                    break;

            }

            
        }
        contador++;
    }


    private void SetNoteAndBarSpeed(float newSpeed)
    {
        notePrefab.SetSpeed(newSpeed);
        barPrefab.SetSpeed(newSpeed);
        noteSpawnYPosition = (newSpeed * initialFallTime) - drumDepth;
    }

    public void SpawnNote(Note.NoteType noteType)
    {
        var newNote = Instantiate(notePrefab, new Vector3(GetXSpawnPosition(noteType), noteSpawnYPosition, 0), Quaternion.identity);
        newNote.SetNoteType(noteType);
        if (kiaiMode)
        {
            newNote.SetKiaiMode();
        }
        newNote.gameObject.name = "Note " + contador.ToString();
    }

    private void SpawnBar()
    {
        float screenCenter = 0;
        var bar /*lol*/ = Instantiate(barPrefab, new Vector3(screenCenter, noteSpawnYPosition, 0), Quaternion.identity);
        bar.gameObject.name = "Bar " + contador.ToString();
    }

    public void TrySpawnHardNote(Note.NoteType noteType)
    {
        hardNoteCounter = (hardNoteCounter + 1) % 3;

        if ((hardNoteCounter >= 2 - Math.Ceiling((double)ScoreManager.Instance.returnPromedioNotas())) || staticPart)
        {
            SpawnNote(noteType);
        }
    }


    public float GetXSpawnPosition(Note.NoteType noteType)
    {

        return drumPositions[(int)noteType].position.x;

    }

    public float GetFallTime()
    {
        return (noteSpawnYPosition + drumDepth) / notePrefab.GetSpeed();
    }

    public float GetInitialFallTime()
    {
        return initialFallTime;
    }

    public string GetSongName()
    {
        return songName;
    }

    public float GetSongPlayDelay()
    {
        return songPlayDelay;
    }

    public void SetSongName(string newSong)
    {
        songName = newSong;
    }




    public static void LoadGameScene(string songToPlay, string realSongName, string artist)
    {
        songName = songToPlay;
        shownSongName = realSongName;
        artistName = artist;
        SceneManager.LoadScene("Game");
    }

    public void ResetStaticData()
    {
        Instance = null;
    }



}
