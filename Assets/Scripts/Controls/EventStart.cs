using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EventStart : MonoBehaviour
{
    [SerializeField] private Animator playerAnt;
    [SerializeField] List<string> listPlayer = new List<string>
        {
            "CharacterUI1",
            "CharacterUI2"
        };
    private int chosePLayer;

    void Awake(){
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Start()
    {
        chosePLayer = 0;
        //Assets/ReSources/Animations/Player/CharacterUI1.controller
        //                 Animations/Player/CharacterUI1.controller
        string folderPath = ("Animations/Player/" + listPlayer[chosePLayer] + ".controller").Trim();
        RuntimeAnimatorController controller = Resources.Load(listPlayer[chosePLayer]) as RuntimeAnimatorController;
        if (controller != null)
        {
            Debug.Log("Thư mục tồn tại.");
            playerAnt.runtimeAnimatorController = controller;
        }
        else
        {
            Debug.Log("Thư mục không tồn tại." + folderPath);
        }
    }

    void Update()
    {
        
    }

    public void btnStartClick(){
        StartCoroutine(LoadScence());
    }

    private System.Collections.IEnumerator LoadScence(){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("PlayScence0");
        while(!asyncLoad.isDone){
            yield return null;
        }
    }

    public void btnQuitClick(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void btnLeftClick(){
        chosePLayer -= 1;
        if(chosePLayer < 0) chosePLayer = listPlayer.Count -1;
        Debug.Log(chosePLayer);
    }

    public void btnRoghtClick(){
        chosePLayer += 1;
        if(chosePLayer >= listPlayer.Count) chosePLayer =  0;
        Debug.Log(chosePLayer);
    }
}
