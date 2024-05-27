using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagEndScr : MonoBehaviour
{
    private Animator at;

    [SerializeField] string nextScence;
    private bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        at = GetComponent<Animator>();
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c){
        if(c!=null){
            if(c.gameObject.tag == "Player" && !isOn){
                this.onFlag();
                isOn = true;
            }
        }
    }

    public void onFlag(){
        at.SetTrigger("flag");
        Invoke("LoadNextScene",3);
    }

    void LoadNextScene()
    {
        EventButton.instance.ResumeScreen();
    }
}
