using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EventButton : MonoBehaviour
{
    [SerializeField] private GameObject player, startPoint, desappearing,
                                        UIPlay, UIPause, UISetting, UIDead,
                                        AudioSlider, _btnNextLevel;
    public GameObject btnNextLevel {
        get{
            return this._btnNextLevel;
        }
    }
    [SerializeField] private TextMeshProUGUI _countJump;
    public TextMeshProUGUI countJump {
        get{
            return this._countJump;
        }
    }
    [SerializeField] private TextMeshProUGUI AudioText;
    [SerializeField] private TextMeshProUGUI HealthPointText;
    [SerializeField] private AudioSource BgAudio;
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _pointText;
    public Image healthBar {get{
        return _healthBar;
    }}
    public TextMeshProUGUI pointText {get{
        return _pointText;
    }}
    [SerializeField] private TextMeshProUGUI TextTimes;
    [SerializeField] private TextMeshProUGUI showTime, showPoint;

    private Player PlayerCtrl;
    private float audioValue;
    private float timeCount;

    public static EventButton instance;

    void Awake() {
        if(instance == null) instance = this;
    }

    void Start() {
        if(player != null) {
            PlayerCtrl = player.GetComponent<Player>();
            if(startPoint != null) player.transform.position = startPoint.transform.position;
        }
        audioValue = 25;
        AudioSlider.GetComponent<Slider>().value = audioValue / 100;
        BgAudio.GetComponent<AudioSource>().volume = audioValue / 100;
        timeCount = 0;
    }

    void Update() {
        timeCount += Time.deltaTime;
        this.checkPlayer();
        if(TextTimes != null) {
            string timeFormat = string.Format("Time: {0:00}:{1:00} s", (int)(timeCount / 60), (int)(timeCount % 60));
            TextTimes.text = timeFormat;
        }
    }

    private void checkPlayer() {
        if(player != null && PlayerCtrl.isDie){
            Instantiate(desappearing,player.transform.position,Quaternion.identity);
            Destroy(player);
            Invoke("ResumeScreen", 1);
            return;
        }
        if(player != null) HealthPointText.text = PlayerCtrl.getHealth.ToString("F2");
    }

    public void ResumeScreen() {
        UIPlay.SetActive(false);
        UIDead.SetActive(true);
        string timeFormat = string.Format("Time: {0:00}:{1:00} s", (int)(timeCount / 60), (int)(timeCount % 60));
        showTime.text = timeFormat;
        showPoint.text = string.Format("Point: {0:00}", PlayerCtrl.addPoint);
    }

    public void btnLeftDown() {
        PlayerCtrl.setDirMove(-1);
        PlayerCtrl.isMobile = true;
    }

    public void btnLeftUp() {
        PlayerCtrl.setDirMove(0);
        PlayerCtrl.isMobile = false;
    }

    public void btnRightDown() {
        PlayerCtrl.setDirMove(1);
        PlayerCtrl.isMobile = true;
    }

    public void btnRightUp() {
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

    public void btnSettingClick(){
        if(UISetting != null) UISetting.SetActive(true);
        if(player != null) player.SetActive(false);
        if(UIPlay != null) UIPlay.SetActive(false);
        if(UIPause != null) UIPause.SetActive(false);
    }

    public void btnPausetoPlayClick(){
        if(UIPlay != null) UIPlay.SetActive(true);
        if(player != null) player.SetActive(true);
        if(UIPause != null) UIPause.SetActive(false);
        if(UISetting != null) UISetting.SetActive(false);
    }

    public void btnQuitClick(){
        SceneManager.LoadScene("StartScence");
    }

    public void btnReLoadScence(string scence){
        try {
            SceneManager.LoadScene(scence);
            this.UIDead.SetActive(false);
            this.UIPause.SetActive(false);
            this.UISetting.SetActive(false);
            this.UIPlay.SetActive(true);
        } 
        catch (System.Exception ex) {
            Debug.Log(ex);
            SceneManager.LoadScene("StartScence");
        }
    }

    public void sliderAudioChange(){
        if (AudioSlider != null) {
            audioValue = AudioSlider.GetComponent<Slider>().value * 100;
            BgAudio.GetComponent<AudioSource>().volume = audioValue / 100;
            AudioText.text = ((int)audioValue).ToString();
        } else {
            Debug.Log("AudioSlider is null");
        }
    }
}
