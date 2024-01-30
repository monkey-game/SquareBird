using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IPointerClickHandler
{
    public static bool isStartGame = false;
    private GameObject CanvasStartGame;
    private GameObject ObSetting;
    private bool isPlay;
    [SerializeField]private GameObject CanvasPlayer;
    [SerializeField]private Transform TransformPlayer;
    [SerializeField] private GameObject ObjX;
    public static bool isNewBie;
    private void Awake()
    {
      //  DontDestroyOnLoad(gameObject);     
    }
    private void Start()
    {
        CanvasStartGame = GameObject.Find("CanvasStartGame");
        ObSetting = GameObject.Find("Setting");
        TransformPlayer = GameObject.Find("Player").transform;
    }
    public void OnClickSetting()
    {
        Animation animation = ObSetting.GetComponent<Animation>();
        if (isPlay)
        {
            animation.Play("SettingClose");
            isPlay = false;
        }
        else
        {
            animation.Play();
            isPlay = true;
        }
        if(GameController.Instance.Mute)
        {
            ObjX.SetActive(true);
        }else
            ObjX.SetActive(false);
    }
    public void OnClickMute()
    {
        GameController.Instance.Mute = !GameController.Instance.Mute;
        Debug.Log(GameController.Instance.Mute);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isStartGame = true;       
        Debug.Log("StartGame");
        gameObject.SetActive(false);
        CanvasPlayer.transform.parent = TransformPlayer;
        TransformPlayer.GetComponent<TrailRenderer>().enabled = true;
        CanvasPlayer.SetActive(true);
        ScoreManager.Instance.scoreNow = 0;
        ScoreManager.Instance.LoadBestScore();
        PlayGamesPlatform.Instance.IncrementAchievement("CgkIr6b-jqcDEAIQBQ",5,(bool success)=>{});
        if(isNewBie){
        Social.ReportProgress("CgkIr6b-jqcDEAIQBw",100.0f,(bool Success)=>{});
        isNewBie = false;
        }
    }
}
