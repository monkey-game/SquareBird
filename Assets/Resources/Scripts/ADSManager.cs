using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Advertisements.Advertisement;

public class ADSManager : MonoBehaviour
{
    public AdsInitializer AdsInitializer;
    public BannerAd bannerAd;
    public InterstitialAd interstitialAd;
    public RewardedAds rewardedAds;
    public static ADSManager Instance { get;private set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else if(Instance != this&&Instance != null)
        {
            Destroy(Instance);
        }
        bannerAd.LoadBanner();
        rewardedAds.LoadAd();
        interstitialAd.LoadAd();
    }
}
