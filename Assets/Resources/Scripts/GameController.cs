using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    [SerializeField] private Transform HomePos;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject[] listMap;
    private Transform TransOld;
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
    }
    public void GameComplete()
    {
        ResetBirdBar();
        MainMenu.isStartGame = false;
        CanvasPlayer.SetActive(false);
        CanvasGameComplete.SetActive(true);
        ScoreManager.Instance.SaveBestScore();
        Level++;
    }
    public void OnClickNextButton()
    {
        TransOld = listMap[Level - 1].gameObject.transform;
        Destroy(listMap[Level - 1].gameObject);
        Instantiate(listMap[Level].gameObject, TransOld.position, Quaternion.identity);
        Player.transform.position = HomePos.position;
    }
    public void LoadMap(byte level)
    {
        if (level > 0)
        {
            TransOld = listMap[0].gameObject.transform;
            Destroy(listMap[0].gameObject);
            Instantiate(listMap[level].gameObject, TransOld.position+new Vector3(3,0,0), Quaternion.identity);
            Player.transform.position = HomePos.position;
            Level = level;
        }
    }
}
