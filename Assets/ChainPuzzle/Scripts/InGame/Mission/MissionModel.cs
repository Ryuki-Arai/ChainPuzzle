using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InGame
{
    public class MissionModel
    {
        public MissionData[] MisionDataArr => missionDataList.ToArray();

        private List<MissionData> missionDataList;


        public MissionModel(FieldPieceData data)
        {
            missionDataList = new List<MissionData>();
            SetMissionData(data);
        }

        public bool TryGetData(PieceData pieceData, out MissionData missionData)
        {
            foreach (var mission in missionDataList)
            {
                if (mission.PieceData == pieceData)
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
            if (TryGetData(data, out var mission))
            {
                return mission.Count <= 0;
            }

            return false;

        }

        public bool IsAllClear()
        {
            return missionDataList.All(data => data.Count <= 0);
        }

        private void SetMissionData(FieldPieceData data)
        {
            var pieceData = data.PieceData;
            var count = data.PieceCount;
            var mission = new MissionData(pieceData, count);
            missionDataList.Add(mission);
        }
    }
}