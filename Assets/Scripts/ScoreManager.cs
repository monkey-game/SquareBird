using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int scoreNow;
    public int BestScore;
    public string NamePlayer;
    public int Coin;
    public List<int> ListScore = new List<int>();
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this&& Instance != null)
        {
            Destroy(Instance);
        }
    }
    private void Start()
    {    
        LoadBestScore();
        LoadListScore();     
    }

    // Update is called once per frame
    void Update()
    {      
    }
    private void OnDestroy()
    {
        SaveListScorePlayer();
    }
    public void UpdateScore()
    {

    }
    public void SaveBestScore()
    {
        if (BestScore < scoreNow)
        {
            PlayerPrefs.SetInt("BestScore_"+name, BestScore);
            BestScore = scoreNow;
            Social.ReportScore(BestScore, "CgkIr6b-jqcDEAIQAw", (bool isSucces) =>
            {
                Debug.Log("Success");
            });
        }
        AddListScore();
    }
    public void AddListScore()
    {
        ListScore.Add(scoreNow);
        ListScore = ListScore.OrderByDescending(x => x).ToList();
    }
    private void SaveListScorePlayer()
    {
        for(int i = 0; i<ListScore.Count;i++)
        {
            if (i == 10)
                return;
            PlayerPrefs.SetInt("Score_" + i, ListScore[i]);          
        }
    }
    private void LoadListScore()
    {
        for (int i = 0; i < ListScore.Count; i++)
        {
            if (i==10)
            {
                return;
            }
            PlayerPrefs.GetInt("Score_" + i, ListScore[i]);
        }
    }
    public void LoadBestScore()
    {
        PlayerPrefs.GetInt("BestScore_"+name, BestScore);
    }
}
