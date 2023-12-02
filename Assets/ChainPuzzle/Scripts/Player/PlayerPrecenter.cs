using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrecenter : MonoBehaviour
{
    [SerializeField] private PlayerView view;
    [SerializeField] private MissionPrecenter mission;
    private PlayerModel model;

    public void OnInitialized()
    {
        model = new PlayerModel() ;
        view.SetUp();
        mission.OnInitialized();
    }

    public void OnUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            model.GetPieceItem();
            view.SetLine(model.ChainPoint);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            view.DisableChain(model.PieceChain);
            if (model.IsDeleteLength)
            {
                mission.ProgressMission(model.ChainData, model.PieceChain.Count);
                if (mission.IsAllClear)
                {
                    MainPrecenter.Instance.OnClear();
                }
            }
            model.RemoveChain();
        }
    }
}
