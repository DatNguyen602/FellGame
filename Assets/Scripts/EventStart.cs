using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EventStart : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void btnStartClick(){
        SceneManager.LoadScene("PlayScence1");
    }

    public void btnQuitClick(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
