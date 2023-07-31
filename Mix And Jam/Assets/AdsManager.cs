using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public string androidAdUnitId;
    public string iosAdUnitId;

    string adUnitId;
    int reward = 250;
    Coins coin;

    void Awake()
    {
    #if UNITY_IOS
            adUnitId = iosAdUnitId;
    #elif UNITY_ANDROID
            adUnitId = androidAdUnitId;
    #endif

    }

    private void Start()
    {
        coin = FindObjectOfType<Coins>();    
    }

    public void LoadAd(int coins = 250)
    {
        reward = coins;
        print("Loading Rewarded!!");
        Advertisement.Load(adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId.Equals(adUnitId))
        {
            print("Rewarded loaded!!");
            ShowAd();
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        print("Rewarded failed to load" + error + message);
    }



    public void ShowAd()
    {
        print("showing Rewarded ad!!");
        Advertisement.Show(adUnitId, this);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        print("Rewarded clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(adUnitId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            print("Rewarded show complete , Distribute the rewards");
            if (reward != -1)
            {
                coin.NumberOfCoins += reward;
            }
            else
            {
                Spawner spawner = FindObjectOfType<Spawner>();
                PlayerPrefs.SetInt("StartingAmmount", spawner.CurrentWave - 1);
                FindObjectOfType<SceneManagement>().LoadScene("Game");
            }
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        print("Rewarded show failure" + error + message);

    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print("Rewarded show start");
    }
}