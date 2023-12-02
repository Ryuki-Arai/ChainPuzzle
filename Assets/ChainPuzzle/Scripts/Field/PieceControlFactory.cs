using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace InGame
{
    public class PieceControlFactory : MonoBehaviour
    {
        //[SerializeField] private StageDataObject StageDataObject;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private PieceData[] pieceDataArr;
        private List<Piece> piecePool = new List<Piece>();

        public void OnInitialized()
        {
            SetPieceDataArr();
            CreatePiece(DataManager.Instance.StageDataObject.MaxSpawn);
        }

        public void OnUpdate()
        {
            SpawnPiece();
            PieceSafety();
        }

        public void DesablePiece(Piece piece)
        {
            //var index = piecePool.IndexOf(piece);
            InactivePiece(piece);
        }

        private void PieceSafety()
        {
            var offStagePieces = piecePool
                .Where(piece =>
                    piece.gameObject.transform.position.y < DataManager.Instance.StageDataObject.InactivePosY
                    && piece.gameObject.activeSelf)
                .ToList();

            offStagePieces.ForEach(piece => InactivePiece(piece));
        }

        private void SetPieceDataArr()
        {
            var startDataIndex = DataManager.Instance.PieceDataObject.GetPieceDataIndex(DataManager.Instance.StageDataObject.StartID, DataManager.Instance.StageDataObject.StartDigit);
            var DataList = new List<PieceData>();
            for (var i = startDataIndex; i < startDataIndex + DataManager.Instance.StageDataObject.Index; i++)
            {
                DataList.Add(DataManager.Instance.PieceDataObject.DataArr[i]);
            }
            pieceDataArr = DataList.ToArray();
        }

        private void SpawnPiece()
        {
            if (piecePool.Where(piece => piece.gameObject.activeSelf).ToList().Count >= DataManager.Instance.StageDataObject.MaxSpawn)
            {
                return;
            }

            if (piecePool.Count == 0)
            {
                ActivePiece(CreatePiece());
                return;
            }

            var inactivePieceList = piecePool.Where(piece => !piece.gameObject.activeSelf).ToList();

            if (inactivePieceList.Count == 0)
            {
                ActivePiece(CreatePiece());
                return;
            }

            var piece = inactivePieceList[0];
            ActivePiece(piece);
        }

        private Piece CreatePiece()
        {
            var piece = Instantiate(DataManager.Instance.StageDataObject.PiecePrefab, spawnPoint.position, Quaternion.identity);
            piece.gameObject.SetActive(false);
            piecePool.Add(piece);
            return piece;
        }

        private void CreatePiece(int count)
        {
            var pieceArr = new Piece[count];
            for (var i = 0; i < count; i++)
            {
                var piece = Instantiate(DataManager.Instance.StageDataObject.PiecePrefab, spawnPoint.position, Quaternion.identity);
                piece.gameObject.SetActive(false);
                piecePool.Add(piece);
            }
        }

        private void ActivePiece(Piece piece)
        {
            piece.Initialize(GetPieceData());
            piece.gameObject.transform.position = GetRandomPos(spawnPoint.position, spawnPoint.localScale);
            piece.gameObject.SetActive(true);
        }

        private void InactivePiece(Piece piece)
        {
            piece.gameObject.SetActive(false);
        }

        private PieceData GetPieceData()
        {
            return pieceDataArr[Random.Range(0, pieceDataArr.Length - 1)];
        }

        private Vector3 GetRandomPos(Vector3 position, Vector3 scale)
        {
            var min = position - scale / 2;
            var max = position + scale / 2;
            var x = Random.Range(min.x, max.x);
            var y = Random.Range(min.y, max.y);

            return new Vector3(x, y, position.z);
        }
    }
}