using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DataModel : Singleton<DataModel>
{
    // DB初期化完了フラグ
    public bool IsInitialize { get; private set; }

    // マスタの読み込み完了フラグ
    public bool IsCompleteLoadMasterData { get; private set; }

    // DBの準備が完了しているか
    public bool IsCompleteReady => IsInitialize && IsCompleteLoadMasterData;


    // プレイヤー情報モデル
    //public PlayerModel Player { get; private set; } = new PlayerModel();

    // マスタ情報モデル
    //public VersionMasterModel Version { get; private set; } = new VersionMasterModel();
    public GameMasterModel Game { get; private set; } = new GameMasterModel();

    // 初期化
    public IEnumerator Init()
    {
        if (IsInitialize) { yield break; }
        // データを読み取り可能な場所にコピー
#if UNITY_ANDROID
        //yield return CopyDataFile();
#endif

        // DBのプレイヤー情報をキャッシュする
        //Player.Init();
        //Player.CacheFromDB();

        IsInitialize = true;
    }
    public IEnumerator LoadMasterData(string key, Action<DownloadHandler> downloadData)
    {
        Debug.Log("データ受信開始・・・");
        var request = UnityWebRequest.Get("https://script.google.com/macros/s/" + key + "/exec");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            if (request.responseCode == 200)
            {
                downloadData?.Invoke(request.downloadHandler);
                Debug.Log("データ受信成功！");
            }
            else
            {
                Debug.LogError("データ受信失敗：" + request.responseCode);
            }
        }
        else
        {
            Debug.LogError("データ受信失敗" + request.result);
        }
        Debug.Log("データ受信終了");
    }



    // データファイルをアクセス可能なディレクトリに保存
    IEnumerator CopyDataFile()
    {
        var path = $"jar:file://{Application.dataPath}!/assets/Data/Data.bytes";

        UnityWebRequest request = UnityWebRequest.Get(path);

        // 非同期でデータを取得
        yield return request.SendWebRequest();

        // リクエストが失敗していないかチェック
        if (request.result == UnityWebRequest.Result.Success)
        {
            // データをパーシステントディレクトリに書き込む
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
