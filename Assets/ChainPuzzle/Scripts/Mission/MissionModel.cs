using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionModel
{
    public MissionData[] MisionDataArr => missionDataList.ToArray();

    private List<MissionData> missionDataList;


    public MissionModel(FieldPieceData[] dataArr)
    {
        missionDataList = new List<MissionData>();
        SetMissionData(dataArr);
    }

    private void SetMissionData(FieldPieceData[] dataArr)
    {
        for(int i = 0; i < dataArr.Length; i++)
        {
            var data = dataArr[i];
            var pieceData = data.PieceData;
            var count = data.PieceCount;
            var mission = new MissionData(pieceData, count);
            if (missionDataList.Contains(mission))
            {
                Debug.LogError("ミッションが重複しています");
                continue;
            }
            missionDataList.Add(mission);
        }
    }
}
