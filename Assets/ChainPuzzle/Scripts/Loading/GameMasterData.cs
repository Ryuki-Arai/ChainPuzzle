using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterModel
{
    public StageDTO[] StageData;

    public StageDataObject GetStageData(int stageLevel)
    {
        if(StageData == null)
        {
            Debug.LogError("ステージデータがありません");
            return null;
        }

        if(StageData.Length < stageLevel)
        {
            Debug.Log("境界範囲外の為ランダムなステージを読み込みます");
            var randLevel = Random.Range(0, stageLevel - 1);
            return CreateStageData(randLevel);
        }

        Debug.Log($"ステージ{stageLevel}を読み込みます");
        return CreateStageData(stageLevel);
    }

    private StageDataObject CreateStageData(int level)
    {
        foreach(var stage in StageData)
        {
            if(stage.StageLevel == level)
            {
                return new StageDataObject(stage.StageLevel, stage.StartID, stage.Index, stage.ClearPieceID, stage.ClearPieceCount);
            }
        }

        Debug.LogError("境界チェック漏れ");
        return null;
    }
}