using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public bool TryGetData(PieceData pieceData, out MissionData missionData)
    {
        foreach(var mission in missionDataList)
        {
            if(mission.PieceData == pieceData)
            {
                missionData = mission;
                return true;
            }
        }
        missionData = null;
        return false;
    }

    public void PrigressData(MissionData data, int count)
    {
        data.MinusCount(count);
    }

    public bool IsClear(PieceData data)
    {
        if(TryGetData(data, out var mission))
        {
            return mission.Count <= 0;
        }

        return false;

    }

    public bool IsAllClear()
    {
        return missionDataList.All(data => data.Count <= 0);
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
