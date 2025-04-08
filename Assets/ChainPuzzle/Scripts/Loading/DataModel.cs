using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DataModel : Singleton<DataModel>
{
    // DB�����������t���O
    public bool IsInitialize { get; private set; }

    // �}�X�^�̓ǂݍ��݊����t���O
    public bool IsCompleteLoadMasterData { get; private set; }

    // DB�̏������������Ă��邩
    public bool IsCompleteReady => IsInitialize && IsCompleteLoadMasterData;


    // �v���C���[��񃂃f��
    //public PlayerModel Player { get; private set; } = new PlayerModel();

    // �}�X�^��񃂃f��
    //public VersionMasterModel Version { get; private set; } = new VersionMasterModel();
    public GameMasterModel Game { get; private set; } = new GameMasterModel();

    // ������
    public IEnumerator Init()
    {
        if (IsInitialize) { yield break; }
        // �f�[�^��ǂݎ��\�ȏꏊ�ɃR�s�[
#if UNITY_ANDROID
        //yield return CopyDataFile();
#endif

        // DB�̃v���C���[�����L���b�V������
        //Player.Init();
        //Player.CacheFromDB();

        IsInitialize = true;
    }
    public IEnumerator LoadMasterData(string key, Action<DownloadHandler> downloadData)
    {
        Debug.Log("�f�[�^��M�J�n�E�E�E");
        var request = UnityWebRequest.Get("https://script.google.com/macros/s/" + key + "/exec");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            if (request.responseCode == 200)
            {
                downloadData?.Invoke(request.downloadHandler);
                Debug.Log("�f�[�^��M�����I");
            }
            else
            {
                Debug.LogError("�f�[�^��M���s�F" + request.responseCode);
            }
        }
        else
        {
            Debug.LogError("�f�[�^��M���s" + request.result);
        }
        Debug.Log("�f�[�^��M�I��");
    }



    // �f�[�^�t�@�C�����A�N�Z�X�\�ȃf�B���N�g���ɕۑ�
    IEnumerator CopyDataFile()
    {
        var path = $"jar:file://{Application.dataPath}!/assets/Data/Data.bytes";

        UnityWebRequest request = UnityWebRequest.Get(path);

        // �񓯊��Ńf�[�^���擾
        yield return request.SendWebRequest();

        // ���N�G�X�g�����s���Ă��Ȃ����`�F�b�N
        if (request.result == UnityWebRequest.Result.Success)
        {
            // �f�[�^���p�[�V�X�e���g�f�B���N�g���ɏ�������
            File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "Data.bytes"), request.downloadHandler.data);
        }
        else
        {
            Debug.LogError($"Failed to load data: {request.error}");
        }

        request.Dispose();
        request = null;
    }
}
