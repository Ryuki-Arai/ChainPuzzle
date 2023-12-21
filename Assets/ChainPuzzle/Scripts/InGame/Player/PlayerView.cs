using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InGame
{
    public class PlayerView : MonoBehaviour
    {

        [SerializeField] LineRenderer lineRenderer;
        [SerializeField, Range(0.0f, 1.0f)] float lineWidth;
        [SerializeField] float linePosZ;

        public void SetUp()
        {
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
        }

        public void DisableChain(List<Piece> pieceList)
        {
            pieceList.ForEach(p => p.Unselect());
            if (pieceList.Count >= DataManager.Instance.ChainDataObject.MinChainLength)
            {
                DisablePieces(pieceList);
            }
            ClearLinePoints();
        }

        public void SetLine(List<Vector3> point)
        {
            var positions = point.Select(p => new Vector3(p.x, p.y, linePosZ)).ToArray();
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }

        private void DisablePieces(List<Piece> pieceList)
        {
            foreach (var piece in pieceList)
            {
                piece.gameObject.SetActive(false);
            }
        }

        private void ClearLinePoints()
        {
            lineRenderer.positionCount = 0;
        }
    }
}