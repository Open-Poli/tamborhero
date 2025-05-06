using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MBanimation : MonoBehaviour
{
    public TextMeshProUGUI difficultyText;
    private Animator animator;
    //public UISongPanel dificultades;
    //public GameObject texto;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //texto = texto.GetComponent<Text>();
        //tengo que hacer que si el texto es "Facil" haga el setTrigger de la animación correspondiente pero no tengo idea de cómo acceder al texto
    
    }

    // Update is called once per frame
    void Update()
    {
        if(difficultyText.text == "Dificil"){
            animator.SetTrigger("esDificil");
            
        }else if(difficultyText.text == "Medio"){
            animator.SetTrigger("esMedio");
            
        }else if(difficultyText.text == "Facil"){
            animator.SetTrigger("esFacil");
            
        }else if(difficultyText.text == "Imposible"){
            animator.SetTrigger("esMuyDificil");
            
        }
        
    }
}
