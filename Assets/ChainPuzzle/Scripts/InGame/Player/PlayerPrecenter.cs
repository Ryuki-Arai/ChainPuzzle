using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class PlayerPrecenter : MonoBehaviour
    {
        [SerializeField] private PlayerView view;
        [SerializeField] private MissionPrecenter mission;
        private PlayerModel model;

        public void OnInitialized(PieceControlFactory controlFactory)
        {
            model = new PlayerModel(controlFactory);
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
                    if (!mission.ProgressMission(model.ChainData, model.PieceChain.Count))
                    {
                        var skipCount = model.IsPieceSkip ? 2 : 1;
                        model.controlFactory.RequestNextPiece(model.ChainData.ID + skipCount, model.PieceChain[model.PieceChain.Count - 1].transform.position);
                    }
                    
                    if (mission.IsAllClear)
                    {
                        MainPrecenter.Instance.OnClear();
                    }
                }
                model.RemoveChain();
            }
        }
    }
}