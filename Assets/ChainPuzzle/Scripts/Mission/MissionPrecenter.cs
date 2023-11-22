using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPrecenter : MonoBehaviour
{
    [SerializeField] private MiissionView view;
    private MissionModel model;

    public void OnInitialized()
    {
        model = new MissionModel(DataManager.Instance.StageDataObject.ClearPieceData);
        view.SetUp(model.MisionDataArr);
    }
}
