using GoogleMobileAds.Api;
using UnityEngine;
//using GoogleMobileAdsMediationTestSuite.Api;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class MediationManagerX : MonoBehaviour
{
    public static MediationManagerX Instance;
    // These ad units are configured to always serve test ads.
    [Header("ADMOB IDS")]
    public string APP_ID = "ca-app-pub-3940256099942544~3347511713";
    [Space(10)]
    public string INTER_ID_1 = "ca-app-pub-3940256099942544/1033173712"; // Interstitial ADS 1
    public string INTER_ID_2 = "ca-app-pub-3940256099942544/1033173712"; // Interstitial ADS 2
    public string INTER_ID_3 = "ca-app-pub-3940256099942544/1033173712"; // Interstitial ADS 3
    [Space(10)]
    public string BANNER_ID_1 = "ca-app-pub-3940256099942544/6300978111"; //Banner ADS 1
    public string BANNER_ID_2 = "ca-app-pub-3940256099942544/6300978111"; //Banner ADS 2
    public string BANNER_ID_3 = "ca-app-pub-3940256099942544/6300978111"; //Banner ADS 3
    [Space(10)]
    public string NATBANNER_ID1 = "ca-app-pub-3940256099942544/6300978111"; // Native Banner ADS 1
    public string NATBANNER_ID2 = "ca-app-pub-3940256099942544/6300978111"; // Native Banner ADS 2
    public string NATBANNER_ID3 = "ca-app-pub-3940256099942544/6300978111"; // Native Banner ADS 3
    [Space(10)]
    public string REWARDED_ID = "ca-app-pub-3940256099942544/5224354917"; //Rewarded Video ADS
#if UNITY_ANDROID

#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
    private string _adUnitId = "unused";
#endif
    private RewardedAd rewardedAd;
    private InterstitialAd interstitialAd1;
    private InterstitialAd interstitialAd2;
    private InterstitialAd interstitialAd3;
    BannerView _bannerView1;
    BannerView _bannerView2;
    BannerView _bannerView3;
    private BannerView NativeBannerAd1;
    private BannerView NativeBannerAd2;
    private BannerView NativeBannerAd3;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this.gameObject); }
    
    }

    public void Start()
    {
        /*        // Initialize the Google Mobile Ads SDK.
                MobileAds.Initialize((InitializationStatus initStatus) =>
                {
                    // This callback is called once the MobileAds SDK is initialized.
                    LoadInterstitialAd();
                });*/

        //mediation INIT

        // Initialize the Mobile Ads SDK.
        MobileAds.Initialize((initStatus) =>
        {
            Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState)
                {
                    case AdapterState.NotReady:
                        // The adapter initialization did not complete.
                        MonoBehaviour.print("Adapter: " + className + " not ready.");
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        MonoBehaviour.print("Adapter: " + className + " is initialized.");
                        break;
                }
            }
        });

        LoadInterstitialAd1();
        LoadInterstitialAd2();
        LoadInterstitialAd3();

        CreateBannerView1(); //For Creating Banner1
        CreateBannerView2();//For Creating Banner1
        CreateBannerView3();
        /*        LoadBannerAd1(); //To Load Call Banner Ads at Start Func()
                LoadBannerAd2();
                LoadBannerAd3();*/
        ShowTopBanner();



        LoadRewardedAd(); //To Load Rewarded Ads at Start Func()
                          //LoadNativeAd();//To Load native Ads at Start

    }
    /*******************************************************Interstitial Ads Below********************************************************************/
    public void LoadInterstitialAd1()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd1 != null)
        {
            interstitialAd1.Destroy();
            interstitialAd1 = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(INTER_ID_1, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd1 = ad;
                RegisterEventHandlers(interstitialAd1);
            });
    }

    public void LoadInterstitialAd2()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd2 != null)
        {
            interstitialAd2.Destroy();
            interstitialAd2 = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(INTER_ID_2, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd2 = ad;
                RegisterEventHandlers(interstitialAd2);
            });
    }

    public void LoadInterstitialAd3()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd3 != null)
        {
            interstitialAd3.Destroy();
            interstitialAd3 = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(INTER_ID_3, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd3 = ad;
                RegisterEventHandlers(interstitialAd3);
            });
    }
    public void ShowAdmobInterstial()
    {
        if (interstitialAd1 != null && interstitialAd1.CanShowAd())
        {
            Debug.Log("Showing interstitial ad 1.");
            interstitialAd1.Show();
        }
        else if (interstitialAd2 != null && interstitialAd2.CanShowAd())
        {
            Debug.Log("Showing interstitial ad 2.");
            interstitialAd2.Show();
        }
        else if (interstitialAd3 != null && interstitialAd3.CanShowAd())
        {
            Debug.Log("Showing interstitial ad 3.");
            interstitialAd3.Show();
        }
        else
        {
            Debug.Log("No interstitial ads available.");
        }

    }

    private void RegisterEventHandlers(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");

            LoadInterstitialAd1();
            LoadInterstitialAd2();
            LoadInterstitialAd3();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            LoadInterstitialAd1();
            LoadInterstitialAd2();
            LoadInterstitialAd3();
        };
    }
    /*******************************************************BannerAds Below********************************************************************/
    public void ShowTopBanner()
    {
        // Check if Banner 1 is loaded and show it if available

        Debug.Log("Showing TOP BANNERS");
        if (_bannerView1 != null)
        {
            //   _bannerView1.Show();
            LoadBannerAd1();
        }
        // Check if Banner 2 is loaded and show it if available
        else if (_bannerView2 != null)
        {
            //  _bannerView2.Show();
            LoadBannerAd2();


        }
        else if (_bannerView3 != null)
        {
            // _bannerView3.Show();
            LoadBannerAd3();
        }
        else
        {
            Debug.Log("No banner ad available to show.");
        }
    }

    public void CreateBannerView1()
    {
        Debug.Log("Creating banner view1");

        // If we already have a banner, destroy the old one.
        /*        if (_bannerView1 != null)
                {
                    DestroyAd();
                }*/

        // Create a 320x50 banner at top of the screen
        _bannerView1 = new BannerView(BANNER_ID_1, AdSize.Banner, AdPosition.Top);
    }

    public void CreateBannerView2()
    {
        Debug.Log("Creating banner view2");

        // If we already have a banner, destroy the old one.
        /*        if (_bannerView2 != null)
                {
                    DestroyAd();
                }*/

        // Create a 320x50 banner at top of the screen
        _bannerView2 = new BannerView(BANNER_ID_2, AdSize.Banner, AdPosition.Top);
    }

    public void CreateBannerView3()
    {
        Debug.Log("Creating banner view3");

        // If we already have a banner, destroy the old one.
        /*        if (_bannerView3 != null)
                {
                    DestroyAd();
                }*/

        // Create a 320x50 banner at top of the screen
        _bannerView3 = new BannerView(BANNER_ID_3, AdSize.Banner, AdPosition.Top);
    }

    public void LoadBannerAd1()
    {
        // create an instance of a banner view first.
        if (_bannerView1 == null)
        {
            CreateBannerView1();
        }
        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView1.LoadAd(adRequest);
        ListenToAdEvents();
    }

    public void LoadBannerAd2()
    {
        // create an instance of a banner view first.
        if (_bannerView2 == null)
        {
            CreateBannerView2();
        }
        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView2.LoadAd(adRequest);
        ListenToAdEvents();
    }

    public void LoadBannerAd3()
    {
        // create an instance of a banner view first.
        if (_bannerView3 == null)
        {
            CreateBannerView3();
        }
        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        _bannerView3.LoadAd(adRequest);
        ListenToAdEvents();
    }

    private void ListenToAdEvents()
    {
        // Raised when an ad is loaded into the banner view.
        _bannerView1.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _bannerView1.GetResponseInfo());
        };
        _bannerView2.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _bannerView2.GetResponseInfo());
        };

        _bannerView3.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _bannerView3.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        _bannerView1.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        _bannerView2.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        _bannerView3.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView1.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        _bannerView2.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        _bannerView3.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _bannerView1.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        _bannerView2.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        _bannerView3.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _bannerView1.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        _bannerView2.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        _bannerView3.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        _bannerView1.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        _bannerView2.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        _bannerView3.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        _bannerView1.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
        _bannerView2.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
        _bannerView3.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }

    public void DestroyAd()
    {
        if (_bannerView1 != null)
        {
            Debug.Log("Destroying banner ad.");
            _bannerView1.Destroy();
            _bannerView1 = null;
        }
    }
    /*******************************************************Rerwarded Ads Below********************************************************************/

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedAd.Load(REWARDED_ID, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                RegisterEventHandlers(rewardedAd);
            });
    }

    public void ShowAdmobRewardedVideo()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " + "with error : " + error);

            LoadRewardedAd();
        };
    }
    /*******************************************************NATIVE Banner Ads Below********************************************************************/
    //= new BannerView(BANNER_ID, AdSize.MediumRectangle, 40, 52);
    public void ShowNativeBanner()
    {
        // Create a banner ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Create a banner view with the specified ad unit ID, ad size, and position.
        NativeBannerAd1 = new BannerView(NATBANNER_ID1, AdSize.MediumRectangle, 40, 52);
        NativeBannerAd2 = new BannerView(NATBANNER_ID2, AdSize.MediumRectangle, 40, 52);
        NativeBannerAd3 = new BannerView(NATBANNER_ID3, AdSize.MediumRectangle, 40, 52);

        // Load the banner ad with the request.
        if (NativeBannerAd1 != null)
        {
            NativeBannerAd1.LoadAd(request);
            //NativeBannerAd1.Show();
        }
        else if (NativeBannerAd2 != null)
        {
            NativeBannerAd2.LoadAd(request);
            // NativeBannerAd2.Show();
        }
        else if (NativeBannerAd3 != null)
        {
            NativeBannerAd3.LoadAd(request);
            // NativeBannerAd3.Show();
        }
        // Show the banner ad.
        //  NativeBannerAd.Show();
    }

    public void HideNativeBanner()
    {
        // Check if Banner 1 is available and hide it
        if (_bannerView1 != null)
        {
            NativeBannerAd1.Hide();
        }

        // Check if Banner 2 is available and hide it
        else if (_bannerView2 != null)
        {
            NativeBannerAd2.Hide();
        }

        // Check if Banner 3 is available and hide it
        else if (_bannerView3 != null)
        {
            NativeBannerAd3.Hide();
        }

    }
    public void OnDestroy()
    {
        // Destroy NativeBannerAd1 if it exists
        if (NativeBannerAd1 != null)
        {
            NativeBannerAd1.Destroy();
            NativeBannerAd1 = null;
        }

        // Destroy NativeBannerAd2 if it exists
        else if (NativeBannerAd2 != null)
        {
            NativeBannerAd2.Destroy();
            NativeBannerAd2 = null;
        }

        // Destroy NativeBannerAd3 if it exists
        else if (NativeBannerAd3 != null)
        {
            NativeBannerAd3.Destroy();
            NativeBannerAd3 = null;
        }
    }

}
