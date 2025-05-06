using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTextManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI countdownText;

    [SerializeField] private string readyText;
    [SerializeField] private string setText;
    [SerializeField] private string goText;


    void Awake()
    {
        countdownText.text = readyText;
        StartCoroutine(CountDown());
    }


    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        countdownText.text = setText;
        yield return new WaitForSeconds(1);
        countdownText.text = goText;
        yield return new WaitForSeconds(1);
        countdownText.text = "";
        GameManager.Instace.StartGame();
    }   


    
}
