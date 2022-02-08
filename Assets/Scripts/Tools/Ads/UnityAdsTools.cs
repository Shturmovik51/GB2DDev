using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Tools.Ads
{
    internal class UnityAdsTools : MonoBehaviour, IAdsShower, IUnityAdsListener
    {
        //private string _gameId = "4593577";
        //private string _rewardPlace = "rewardAds";
        //private string _interstitialPlace = "Interstitial_Android";

        private string _gameId = "4598009";
        private string _rewardPlace = "Rewarded_Android";
        private string _interstitialPlace = "Android_Car_Interstitial";

        private Action _callbackSuccessShowVideo;

        private void Start()
        {
            Advertisement.Initialize(_gameId, true);
        }

        public void ShowInterstitial()
        {
            _callbackSuccessShowVideo = null;
            Advertisement.Show(_interstitialPlace);
        }

        public void ShowVideo(Action successShow)
        {
            _callbackSuccessShowVideo = successShow;
            Advertisement.Show(_rewardPlace);
        }

        public void OnUnityAdsReady(string placementId)
        {

        }

        public void OnUnityAdsDidError(string message)
        {

        }

        public void OnUnityAdsDidStart(string placementId)
        {

        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (showResult == ShowResult.Finished)
                _callbackSuccessShowVideo?.Invoke();
            Debug.Log("DA3");
        }
    }
}