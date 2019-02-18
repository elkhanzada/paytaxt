using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { set; get; }
    public string bannerId = "";
    public string videoId;
    private string rewId = "ca-app-pub-9000047231612236/3410607305";
    private GameController gameController;
    InterstitialAd interstital;
    RewardBasedVideoAd rewardedAd;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        
       
        rewardedAd = RewardBasedVideoAd.Instance;
        RegisterRewardedVideoDelegate();
      
        
#if UNITY_EDITOR
#elif UNITY_ANDROID
    RequestInterstitial();    

#endif

    }
    public void RequestInterstitial(){
		#if UNITY_EDITOR
		#elif UNITY_ANDROID
		interstital = new InterstitialAd(videoId);
		AdRequest request = new AdRequest.Builder().Build();
        interstital.LoadAd(request);
		#endif
    }
    public void RequestRewardedVideoAd()
    {


#if UNITY_EDITOR
#elif UNITY_ANDROID
        RegisterRewardedVideoDelegate();
        AdRequest request = new AdRequest.Builder().AddTestDevice("F75976D9BFEF7A4B").Build();
        rewardedAd.LoadAd(request, rewId);
#endif
    }
    public void ShowRewardedVideo()
    {

#if UNITY_EDITOR
#elif UNITY_ANDROID
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
#endif
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {

    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        UnregisterRewardedVideoDelegate();
        RequestRewardedVideoAd();
    }
    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        gameController = GameObject.FindObjectOfType<GameController>();

    }
    public void HandleOnAdStarted(object sender, EventArgs args)
    {

    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {

    }
    public void HandleOnAdRewarded(object sender, Reward args)
    {
        if (gameController != null)
        {
			gameController.forRewardedUser();
            gameController.rewardButton.SetActive(false);
           
        }
    }
    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {

    }
    public void RegisterRewardedVideoDelegate()
    {
        rewardedAd.OnAdClosed += HandleOnAdClosed;
        rewardedAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        rewardedAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        rewardedAd.OnAdLoaded += HandleOnAdLoaded;
        rewardedAd.OnAdOpening += HandleOnAdOpening;
        rewardedAd.OnAdRewarded += HandleOnAdRewarded;
        rewardedAd.OnAdStarted += HandleOnAdStarted;
    }
    public void UnregisterRewardedVideoDelegate()
    {
        rewardedAd.OnAdClosed -= HandleOnAdClosed;
        rewardedAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        rewardedAd.OnAdLeavingApplication -= HandleOnAdLeavingApplication;
        rewardedAd.OnAdLoaded -= HandleOnAdLoaded;
        rewardedAd.OnAdOpening -= HandleOnAdOpening;
        rewardedAd.OnAdRewarded -= HandleOnAdRewarded;
        rewardedAd.OnAdStarted -= HandleOnAdStarted;
    }


    public void ShowVideo()
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
        if (interstital.IsLoaded())
        {
            interstital.Show();
        }
#endif


    }
}

