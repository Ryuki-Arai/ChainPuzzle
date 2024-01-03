using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class MissionPrecenter : MonoBehaviour
    {
        [SerializeField] private MiissionView view;
        private MissionModel model;

        public bool IsAllClear => model.IsAllClear();

        public void OnInitialized()
        {
            model = new MissionModel(DataManager.Instance.StageDataObject.ClearPieceData);
            view.SetUp(model.MisionDataArr);
        }

        public void PlayMoving(Vector2 pos, PieceData data)
        {
            view.PlayMoving(pos, data);
        }

        public bool IncludeMissionPiece(PieceData data)
        {
            return model.TryGetData(data, out var missionData);
        }

        public bool ProgressMission(PieceData data, int count)
        {
            if (!model.TryGetData(data, out var missionData))
            {
                return false;
            }

            model.PrigressData(missionData, count);

            if (model.IsClear(data))
            {
                view.ClearMission(missionData);
            }

            view.UpdateMission(missionData);

            return true;
        }
    }
}