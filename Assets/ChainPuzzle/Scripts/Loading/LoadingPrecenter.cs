using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingPrecenter : MonoBehaviour
{
    [SerializeField] Transform loadingText;
    [SerializeField] Slider loadingBar;

    private string accessKey = "AKfycbwR0wBvaAJBEBD3PYGRwDeySvy11C3vw4yD-rkdnUC29qpVD1Bhw0YvV2bNg32xh2bO";

    private Tween loadingTween;
    private void Start()
    {
        loadingTween = loadingBar.DOValue(loadingBar.maxValue, 2f)
                .SetEase(Ease.Linear);
        StartCoroutine(LoadingGame());
    }

    private IEnumerator LoadingGame()
    {
        yield return StartCoroutine(GameManager.Instance.InitSetting());

        yield return GameManager.Instance.LoadMasterDataAsync(download =>
        {
            DataModel.I.Game.StageData = JsonUtility.FromJson<GameMasterModel>(download.text).StageData;
        });

        DataSaveUtility.I.GetData();

        DOTween.KillAll();
        SceneLoader.Instance.ChangeScene(SceneName.Home);
    }

    private void OnDisable()
    {
        // Tween”jŠü
        if (DOTween.instance != null)
        {
            loadingTween?.Kill();
        }
    }
}
