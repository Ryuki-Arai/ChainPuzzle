using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

namespace InGame
{
    public class PlayerView : MonoBehaviour
    {

        [SerializeField] LineRenderer lineRenderer;
        [SerializeField, Range(0.0f, 1.0f)] float lineWidth;
        [SerializeField] float linePosZ;
        [SerializeField] Transform missionMovePos;

        public void SetUp()
        {
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
        }

        public Tween MissionMove(Transform piece)
        {
            return piece.DOMove(missionMovePos.position, 0.15f);
        }

        public void DisableChain(List<Piece> pieceList)
        {
            pieceList.ForEach(p => p.Unselect());
            ClearLinePoints();
        }

        public void SetLine(List<Vector3> point)
        {
            var positions = point.Select(p => new Vector3(p.x, p.y, linePosZ)).ToArray();
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }

        public void DisablePieces(List<Piece> pieceList)
        {
            var piecePosList = pieceList.Select(p => p.transform.position).ToList();
            foreach (var piece in pieceList)
            {
                DOTween.Sequence()
                    .OnStart(() =>
                    {
                        piecePosList.RemoveAt(piecePosList.Count - 1);
                    })
                    .Append(piece.transform.DOMove(piecePosList[piecePosList.Count - 1],0.15f))
                    .OnComplete(() =>
                    {
                        piece.gameObject.SetActive(false);
                    });
            }
        }

        private void ClearLinePoints()
        {
            lineRenderer.positionCount = 0;
        }
    }
}