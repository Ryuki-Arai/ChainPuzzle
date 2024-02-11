using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace InGame
{
    public class PlayerModel
    {

        public List<Piece> PieceChain = new List<Piece>();
        public List<Vector3> ChainPoint => PieceChain.Select(piece => piece.gameObject.transform.position).ToList();
        public bool IsDeleteLength => PieceChain.Count >= DataManager.Instance.ChainDataObject.MinChainLength;
        public bool IsPieceSkip => PieceChain.Count >= DataManager.Instance.ChainDataObject.SkipChainLength;
        public PieceData ChainData => PieceChain[0].PieceData;
        public PieceControlFactory controlFactory { get; private set; }
        public Camera Camera;

        public PlayerModel(PieceControlFactory controlFactory)
        {
            OnInitialized(controlFactory);
        }

        private void OnInitialized(PieceControlFactory controlFactory)
        {
            Camera = Camera.main;
            this.controlFactory = controlFactory;
        }

        private void CreateChain(Piece piece)
        {
            PieceChain.Add(piece);
            piece.Select();
            UniAndroidVibration.Vibrate(50);
        }

        private void AddChain(Piece piece)
        {
            if (PieceChain[0].PieceData.ID == piece.PieceData.ID
                && !PieceChain.Contains(piece))
            {
                PieceChain.Add(piece);
                piece.Select();
                UniAndroidVibration.Vibrate(50);
            }
        }

        private void RemoveLastPiece()
        {
            var piece = PieceChain[PieceChain.Count - 1];
            PieceChain.Remove(piece);
            piece.Unselect();
            UniAndroidVibration.Vibrate(50);
        }

        private bool CheckDistance(Vector3 piecePosition)
        {
            return DataManager.Instance.ChainDataObject.ChainDistance >= Vector3.Distance(PieceChain.Last().gameObject.transform.position, piecePosition);
        }

        public void GetPieceItem()
        {
            Ray raycast = Camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(raycast.origin, raycast.direction * 10.0f, Color.red, 3f, false);
            var ray = Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                var go = hit.collider.gameObject;
                Piece piece;
                if (go.TryGetComponent(out piece))
                {
                    if (PieceChain.Count == 0)
                    {
                        CreateChain(piece);
                    }
                    else if(PieceChain.Count > 1 && piece == PieceChain[PieceChain.Count - 2])
                    {
                        RemoveLastPiece();
                    }
                    else
                    {
                        if (CheckDistance(piece.gameObject.transform.position))
                        {
                            AddChain(piece);
                        }
                    }
                }
            }
        }

        public void RemoveChain()
        {
            PieceChain.Clear();
        }
    }
}