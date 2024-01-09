using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public string NameBird;
    public short CountPerfect;
    public short Score;
    public byte Level;
    public bool ResetBird = false;
    public byte[] ListScore = {10,10,11,11,12,12,13};
    public bool isNextLevel = false;
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
    }
    private void Update()
    {
        if (MainMenu.isStartGame)
        {
            float currentTime = Time.time;

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
        sliderBar.value = 0;
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
        MainMenu.isStartGame = false;
        CanvasPlayer.SetActive(false);
        CanvasGameOver.SetActive(true);
        ResetBirdBar();
        ScoreManager.Instance.SaveBestScore();
    }
    public void GameComplete()
    {
        MainMenu.isStartGame = false;
        CanvasPlayer.SetActive(false);
        CanvasGameComplete.SetActive(true);
        ResetBirdBar();
        ScoreManager.Instance.SaveBestScore();
        Level++;
    }
}
