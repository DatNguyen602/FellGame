using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventButton : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject UIPlay;
    [SerializeField] private GameObject UIPause;


    private Player PlayerCtrl;

    void Start()
    {
        PlayerCtrl = player.GetComponent<Player>();
    }

    void Update()
    {
        
    }

    public void btnLeftDown(){
        PlayerCtrl.setDirMove(-1);
        PlayerCtrl.isMobile = true;
    }

    public void btnLeftUp(){
        PlayerCtrl.setDirMove(0);
        PlayerCtrl.isMobile = false;
    }

    public void btnRightDown(){
        PlayerCtrl.setDirMove(1);
        PlayerCtrl.isMobile = true;
    }

    public void btnRightUp(){
        PlayerCtrl.setDirMove(0);
        PlayerCtrl.isMobile = false;
    }

    public void btnJumpClick(){
        PlayerCtrl.Jump();
    }

    public void btnSlipClick(){
        PlayerCtrl.Slip();
    }

    public void btnPlaytoPauseClick(){
        if(UIPause != null) UIPause.SetActive(true);
        if(player != null) player.SetActive(false);
        if(UIPlay != null) UIPlay.SetActive(false);
    }

    public void btnPausetoPlayClick(){
        if(UIPlay != null) UIPlay.SetActive(true);
        if(player != null) player.SetActive(true);
        if(UIPause != null) UIPause.SetActive(false);
    }

    public void btnQuitClick(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
