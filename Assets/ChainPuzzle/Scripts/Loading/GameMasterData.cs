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
            Debug.LogError("�X�e�[�W�f�[�^������܂���");
            return null;
        }

        if(StageData.Length < stageLevel)
        {
            Debug.Log("���E�͈͊O�̈׃����_���ȃX�e�[�W��ǂݍ��݂܂�");
            var randLevel = Random.Range(0, stageLevel - 1);
            return CreateStageData(randLevel);
        }

        Debug.Log($"�X�e�[�W{stageLevel}��ǂݍ��݂܂�");
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

        Debug.LogError("���E�`�F�b�N�R��");
        return null;
    }
}