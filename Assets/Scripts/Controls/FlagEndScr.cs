using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

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

    void OnTriggerEnter2D(Collider2D c){
        if(c!=null){
            if(c.gameObject.tag == "Player" && !isOn){
                this.onFlag();
                isOn = true;
            }
        }
    }

    public async void onFlag(){
        at.SetTrigger("flag");
        Invoke("LoadNextScene",2);
        await Task.Delay(2000);
        EventButton.instance.btnNextLevel.SetActive(true);
    }

    void LoadNextScene()
    {
        EventButton.instance.ResumeScreen();
    }
}
