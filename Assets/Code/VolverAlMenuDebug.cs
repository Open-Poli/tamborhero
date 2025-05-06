using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAlMenuDebug : MonoBehaviour
{
 
    [SerializeField] private GameObject loadingObject;
    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            loadingObject.SetActive(true);
            StaticDataResetter resetter = GetComponent<StaticDataResetter>();
            resetter.ResetAllStaticData();
            resetter.ReturnToMenu();
        }
    }
}
