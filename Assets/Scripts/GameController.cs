﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using Lean.Pool;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameController Instance { get; private set; }
    [SerializeField] private Slider sliderBar;
    [SerializeField] private float timePlay;
    [SerializeField] private GameObject[] BirdPerfect;
    [SerializeField] private Text TextScoreNow;
    [SerializeField] private Text TextLevelNow;
    [SerializeField] private Text TextNextLevel;
    [SerializeField] private GameObject CanvasPlayer;
    [SerializeField] private GameObject CanvasGameOver;
    [SerializeField] private GameObject CanvasGameComplete;
    [SerializeField] private Transform HomePos;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject[] listMap;
    [SerializeField] private ParticleSystem effWin;
    [SerializeField] private ParticleSystem effWin_2;
    public string NameBird;
    public short CountPerfect;
    public short Score;
    public byte Level;
    public bool ResetBird = false;
    public byte[] ListScore = {10,10,11,11,12,12,13};
    public bool isNextLevel = false;
    public bool ResetGame = false;
    private float TimeReset;
    public PlayerBase player;
    public bool LoadGame = false;
    public bool RewardADS = false;
    public Vector3[] postion;
    private Transform temp;
    private GameObject MapTemp;
    public bool ResetCamera = false;
    private bool WaitReward100Coin = false;
    public bool Mute;
    public HashSet<GameObject> collidedObjects = new HashSet<GameObject>();
    public bool Heptic;
  
    private void Awake()
    {
      //  DontDestroyOnLoad(this);
    }
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }else if(Instance != this&& Instance != null) 
        {
            Destroy(Instance);
        }
        LoadGame = true;
        temp = transform;
    }
    private void Update()
    {
        if (MainMenu.isStartGame)
        {
            float currentTime = Time.time - TimeReset;

            float timeRatio = Mathf.Clamp01(currentTime / timePlay);

            UpdateTimeBar(timeRatio);
            if(CountPerfect > 0)
            {
                UpdateBirdBar(CountPerfect);
            }
            if(ResetBird)
            {
                ResetBirdBar();
            }
            TextScoreNow.text = $"{ScoreManager.Instance.scoreNow}";
            TextLevelNow.text = $"{Level}";
            TextNextLevel.text = $"{Level + 1}";
        }
        else
        {
            TimeReset = Time.time;
        }
        Reward100Coin();
    }
    private void OnDestroy()
    {
        player.Level = Level;
        Util.SaveToPlayerJson(player);
    }
    private void UpdateBirdBar(short CountPerfect)
    {
        if(CountPerfect > 0&&CountPerfect < 4)
        {
            BirdPerfect[CountPerfect-1].SetActive(true);
        }
    }
    private void ResetBirdBar()
    {
        foreach(GameObject bird in BirdPerfect)
        {
            bird.SetActive(false);
        }
        ResetBird = false;
    }
    private void UpdateTimeBar(float value)
    {
        if (sliderBar != null)
        {
            sliderBar.value = value;
        }
    }
    public void GameOver()
    {
        ResetBirdBar();
        MainMenu.isStartGame = false;
        CanvasPlayer.SetActive(false);
        CanvasGameOver.SetActive(true);       
        ScoreManager.Instance.SaveBestScore();
        ADSManager.Instance.interstitialAd.LoadAd();
        ADSManager.Instance.interstitialAd.ShowAd();
        collidedObjects.Clear();
    }
    public void GameComplete()
    {
        ResetBirdBar();
        MainMenu.isStartGame = false;
        CanvasPlayer.SetActive(false);
        CanvasGameComplete.SetActive(true);
        ScoreManager.Instance.SaveBestScore();
        Level++;
        player.Coin += 10;
        effWin.Play();
        effWin_2.Play();
        collidedObjects.Clear();
    }
    public void OnClickNextButton()
    {
        if(Level == 1)
        {
            Destroy(listMap[0].gameObject);
        }else
        Destroy(MapTemp);
        temp.position = postion[Level];
        MapTemp = Instantiate(listMap[Level].gameObject, temp.position, Quaternion.identity);
        Player.transform.position = HomePos.position;
        GameController.Instance.ResetCamera = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ADS100Coin()
    {
        ADSManager.Instance.rewardedAds.ShowAd();
        WaitReward100Coin = true;
    }
    public void LoadMap(byte level)
    {
        if (level > 0)
        {
            Destroy(listMap[0].gameObject);
            temp.position = postion[level];
            MapTemp = Instantiate(listMap[level].gameObject, temp.position, Quaternion.identity);
            Player.transform.position = HomePos.position;
            Level = level;
        }
    }
    private void Reward100Coin()
    {
        if (RewardADS&& WaitReward100Coin)
        {
            WaitReward100Coin = false;
            RewardADS = false;
            player.Coin += 100;
        }
    }
}
