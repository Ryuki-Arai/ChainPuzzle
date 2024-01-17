using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

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
                SelectPieces();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (model.IsDeleteLength)
                {
                    view.DisablePieces(model.PieceChain);
                    var skipCount = model.IsPieceSkip ? 2 : 1;
                    model.controlFactory.RequestNextPiece(model.ChainData.ID + skipCount, model.PieceChain[model.PieceChain.Count - 1].transform.position);
                }
                DeletionOfChain();
                MissionAcept();
                if (mission.IsAllClear)
                {
                    MainPrecenter.Instance.OnClear();
                }
            }
        }

        private void MissionAcept()
        {
            var pieceList = model.controlFactory.ActivePieces;
            var missionPiece = pieceList.Where(piece => mission.IncludeMissionPiece(piece.PieceData)).ToList();
            var missionData = missionPiece.Select(piece => piece.PieceData).ToList();
            if(missionData.Count >= 1)
            {
                mission.ProgressMission(missionData[0], missionData.Count);
                missionPiece.ForEach(piece =>
                {
                    DOTween.Sequence()
                        .OnStart(() =>
                        {
                            mission.PlayMoving(model.Camera.WorldToScreenPoint(piece.transform.position),piece.PieceData);
                        })
                        .OnComplete(() =>
                        {
                            model.controlFactory.DesablePiece(piece);
                        });
                });
            }
        }

        private void DeletionOfChain()
        {
            view.DisableChain(model.PieceChain);
            model.RemoveChain();
        }

        private void SelectPieces()
        {
            model.GetPieceItem();
            view.SetLine(model.ChainPoint);
        }
    }
}