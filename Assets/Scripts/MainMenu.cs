using System.Collections;
using System.Collections.Generic;
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
    }
    public void OnClickMute()
    {
        GameController.Instance.Mute = !GameController.Instance.Mute;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isStartGame = true;       
        Debug.Log("StartGame");
        gameObject.SetActive(false);
        CanvasStartGame.transform.parent = TransformPlayer;
        CanvasPlayer.SetActive(true);
        ScoreManager.Instance.scoreNow = 0;
        ScoreManager.Instance.LoadBestScore();
    }
}
