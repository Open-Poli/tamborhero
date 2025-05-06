using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    private int score;

    private int combo;
    private int maxCombo;
    private bool hasFullCombo = true;

    private float accuracy;

    private List<int> registroNotas;

    
    [SerializeField] private int[] hits = new int[4];

    [Header("UI")]
    
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI accuracyText;
    [SerializeField] private TextMeshProUGUI hitText;

    [Header("End Screen")]

    [SerializeField] private TextMeshProUGUI endComboText;
    [SerializeField] private TextMeshProUGUI endScoreText;
    [SerializeField] private TextMeshProUGUI endAccuracyText;
    [SerializeField] private TextMeshProUGUI fullComboText;
    [SerializeField] private Image[] stars;




    


    public static ScoreManager Instance;


    void Awake()
    {
        combo = 0;
        accuracy = 100;
        registroNotas = new List<int>();
        if(Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Debug.LogError("Ummm... Hay dos ScoreManagers en esta escena :crazy:");
        }


    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            UpdateScores();
        }
    }

    public void AddScore(Drum.ScoreType baseScore, bool kiaiMode)
    {
        AddHit(baseScore);
        if(baseScore == Drum.ScoreType.MISS)
        {
            BreakCombo();
        }
        
        
        float scoreIncrease = (int) baseScore * GetComboMultiplier();
        
        if(kiaiMode)
        {
        	scoreIncrease = (int)(scoreIncrease * 1.5f);
        
        }
        
        
        score += Mathf.RoundToInt(scoreIncrease);
        IncreaseCombo();
        CalculateAcc();
        UpdateScoreText();
    }
    
    

    public float GetComboMultiplier()
    {
        if(combo == 0)
        {
            return 1;
        }

        return 1 + ((float)(combo - 1)) / 2;


    }

	public void IncreaseCombo()
	{
		combo++;

        if (combo > maxCombo)
        {
            maxCombo = combo;
        }
		
		UpdateComboText();
	}

	
	
	
	
	
	public void CalculateAcc()
	{
	    accuracy = (float)(300*hits[0] + 100*hits[1] + 50*hits[2])/(300*(hits[0]+hits[1]+hits[2]+hits[3]))*100;
	    UpdateAccuracyText();
	}
	

	public void UpdateAccuracyText()
    {
        accuracyText.text = accuracy.ToString("0.00") + "%";
    }

    public void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
    
    public void UpdateComboText()
    {
        if (combo < 2)
        {
        	comboText.text = "";
        }
        else
        {
        	comboText.text =  "x" + combo.ToString();
        }
    }



	
    public void BreakCombo()
    {
        combo = 0;
        comboText.text = "";
        UpdateComboText();

    }
    
    private void AddHit(Drum.ScoreType score)
    {
        switch (score) {
    
    	    case Drum.ScoreType.PERFECT:

    		    hits[0]++;
                registroNotas.Add(3);
                hitText.text = "Exquisite!";
                showTextAnimation();
    		    break;
    	
    	    case Drum.ScoreType.GREAT:
    
    		    hits[1]++;
                registroNotas.Add(3);
                hitText.text = "Great";
                showTextAnimation();
    		    break;
	    
	        case Drum.ScoreType.BAD:
	    
		        hits[2]++;
                registroNotas.Add(3);
                hitText.text = "Bad";
                showTextAnimation();
                
		        break;
	    
	        case Drum.ScoreType.MISS:
            
		        hits[3]++;
                registroNotas.Add(0);
                hitText.text = "Miss";
                showTextAnimation();
                hasFullCombo = false;
                BreakCombo();
		        break;
	    }

    } 

    public void showTextAnimation()
    {
        hitText.enabled = true;
        LeanTween.scale(hitText.gameObject, new Vector3(1.2f, 1.2f, 1f), 0f).setOnComplete(ShrinkText);
    }

    public void ShrinkText()
    {
        LeanTween.scale(hitText.gameObject, new Vector3(1f, 1f, 1f), 0.1f).setOnComplete(disableText);
    }

    public void disableText()
    {
        hitText.enabled = false;
    }

    public float returnPromedioNotas()
    {

        if(registroNotas.Count < 20) // Si no hay 20 notas, el promedio es 3
        {
            return 3;
        }

        int promedio = 0;
        
        foreach(int i in getLast20Notes())
        {
            promedio += i;
        }

        promedio /= 20;


        return promedio;
    }



    public List<int> getLast20Notes()
    {
        List<int> last20Notes = new List<int>();
        foreach(int i in registroNotas.GetRange(registroNotas.Count - 20, 20))
        {
            last20Notes.Add(i);
            if(last20Notes.Count == 20)
            {
                break;
            }


        }




        return last20Notes;
    }


    public void ShowAccuracyText()
    {
        accuracyText.gameObject.SetActive(true);
    }

    public void ResetStaticData()
    {
        Instance = null;
    }

    public void UpdateEndScreen()
    {
        endComboText.text = "x" + maxCombo.ToString();
        endScoreText.text = score.ToString();
        endAccuracyText.text = accuracy.ToString("0.00") + "%";

        float[] starPercentages = {10, 20, 30, 40, 50, 60, 70, 80};

        for(int i = 0; i < stars.Length; ++i)
        {
            if(accuracy >= starPercentages[i])
            {
                stars[i].enabled = true;
            }
        }

    }

    public bool GetFullCombo()
    {
        return hasFullCombo;
    }
    
    
    public void UpdateScores()
    {
        string caminoArchivo = Application.dataPath + "/Scores/" + NoteGenerator.Instance.GetSongName() + "Scores.txt";
        int cantidadDePosiciones = 3;

    	string[] lineas = File.ReadAllLines(caminoArchivo);


        int posicion = cantidadDePosiciones;

        List<float> todosLosPorcentajes = new List<float>();
        

        for (int i = 0; i < cantidadDePosiciones; ++i)
        {
            if (accuracy > float.Parse(lineas[i]))
            {
                posicion--;
            }
            
            todosLosPorcentajes.Add(float.Parse(lineas[i]));
        }

  
        todosLosPorcentajes.Add(accuracy);
        todosLosPorcentajes.Sort();


        lineas[0] = todosLosPorcentajes[3].ToString("0.00");
        lineas[1] = todosLosPorcentajes[2].ToString("0.00");
        lineas[2] = todosLosPorcentajes[1].ToString("0.00");

        string[] nombres = NombresOrdenCorrecto(posicion);

        lineas[3] = nombres[0];
        lineas[4] = nombres[1];
        lineas[5] = nombres[2];



        

        Debug.Log(lineas[0] + " " + lineas[1] + " " + lineas[2] + " --- " + accuracy + " | " + cantidadDePosiciones + " " + posicion);

        File.WriteAllLines(caminoArchivo, lineas);
    	
    }

    private string[] NombresOrdenCorrecto(int posicion)
    {
        string caminoArchivo = Application.dataPath + "/Scores/" + NoteGenerator.Instance.GetSongName() + "Scores.txt";
        string[] lineas = File.ReadAllLines(caminoArchivo);

        string[] nombres = new string[3];

        switch(posicion)
        {
            case 0:
                nombres[0] = "nom"; 
                nombres[1] = lineas[3];
                nombres[2] = lineas[4];
                break;
            case 1:
                nombres[0] = lineas[3];
                nombres[1] = "nom";
                nombres[2] = lineas[4];
                break;
            case 2:
                nombres[0] = lineas[3];
                nombres[1] = lineas[4];
                nombres[2] = "nom";
                break;
            default:
                nombres[0] = lineas[3];
                nombres[1] = lineas[4];
                nombres[2] = lineas[5];
                break;
        }

        return nombres;

    }



}
