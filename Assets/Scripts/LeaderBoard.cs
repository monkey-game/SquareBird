using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text[] Names;
    [SerializeField] private Text[] Scores;

    void Start()
    {
    }
    public void OnClickLeaderBoard()
    {
        Social.ShowLeaderboardUI();
      //  PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
