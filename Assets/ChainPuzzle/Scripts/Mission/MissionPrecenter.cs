using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ProgressMission(PieceData data , int count)
    {
        if(!model.TryGetData(data, out var missionData))
        {
            return;
        }

        model.PrigressData(missionData, count);

        if (model.IsClear(data))
        {
            view.ClearMission(missionData);
        }

        view.UpdateMission(missionData);
    }
}
