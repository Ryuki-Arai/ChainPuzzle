using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public bool HasMasterCashe
    {
        get; set;
    }
    public bool IsReboot
    {
        get; set;
    }

    public bool IsInMaintenance
    {
        get; set;
    }
    public bool IsInitialized
    {
        get; set;
    }

    private float _TimeScale = 1.0f;
    public float TimeScale
    {
        get
        {
            return _TimeScale;
        }
        set
        {
            _TimeScale = value;
            if (!HasMasterCashe)
            {
                return;
            }
            Time.timeScale = value;
        }
    }

    // 初期設定の実行済み判定
    private bool isSetting;

    /// <summary>
    /// アプリの初期設定を行う
    /// </summary>
    public IEnumerator InitSetting()
    {
        // DB初期化
        yield return DataModel.I.Init();

        isSetting = true;
        yield break;
    }
    /// <summary>
    /// マスターデータを読み込む
    /// </summary>
    /// <param name="onInited"></param>
    /// <param name="failedCB"></param>
    public IEnumerator LoadMasterDataAsync(Action<DownloadHandler> failedCB)
    {
        if (!isSetting)
        {
            yield break;
        }

        if (IsInitialized)
        {
            yield break;
        }

        var env = "AKfycbwR0wBvaAJBEBD3PYGRwDeySvy11C3vw4yD-rkdnUC29qpVD1Bhw0YvV2bNg32xh2bO";

        Debug.Log($"マスタ読み込み開始: {env}");
        yield return DataModel.I.LoadMasterData(env, failedCB);

        CasheData();
    }

    /// <summary>
    /// 読み込んだマスタデータをキャッシュして、各設定に反映する
    /// </summary>
    void CasheData()
    {
        // 強制開始による2度呼出防止用
        if (HasMasterCashe)
        {
            return;
        }

        HasMasterCashe = true;
        IsReboot = false;
        IsInMaintenance = false;
        Time.timeScale = TimeScale;
        IsInitialized = true;
    }
}
