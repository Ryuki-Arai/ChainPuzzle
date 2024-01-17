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

    // �����ݒ�̎��s�ςݔ���
    private bool isSetting;

    /// <summary>
    /// �A�v���̏����ݒ���s��
    /// </summary>
    public IEnumerator InitSetting()
    {
        // DB������
        yield return DataModel.I.Init();

        isSetting = true;
        yield break;
    }
    /// <summary>
    /// �}�X�^�[�f�[�^��ǂݍ���
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

        Debug.Log($"�}�X�^�ǂݍ��݊J�n: {env}");
        yield return DataModel.I.LoadMasterData(env, failedCB);

        CasheData();
    }

    /// <summary>
    /// �ǂݍ��񂾃}�X�^�f�[�^���L���b�V�����āA�e�ݒ�ɔ��f����
    /// </summary>
    void CasheData()
    {
        // �����J�n�ɂ��2�x�ďo�h�~�p
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
