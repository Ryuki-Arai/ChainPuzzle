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
        [SerializeField] private Piece piecePrefab;
        [SerializeField] private int maxSpawn;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private PieceData[] pieceDataArr;
        private List<Piece> piecePool = new List<Piece>();

        private const float Inactive_Pos_Y = -10f;

        public List<Piece> ActivePieces => piecePool.Where(piece => piece.gameObject.activeSelf).ToList();

        public void OnInitialized()
        {
            SetPieceDataArr();
            CreatePiece(maxSpawn);
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

        public void RequestNextPiece(int pieceID, Vector3 pos)
        {
            if(pieceID >= DataManager.Instance.PieceDataObject.DataArr.Length - 1)
            {
                return;
            }

            ActivePiece(CreatePiece(), pieceID, pos);
        }

        private void PieceSafety()
        {
            var offStagePieces = piecePool
                .Where(piece =>
                    piece.gameObject.transform.position.y < Inactive_Pos_Y
                    && piece.gameObject.activeSelf)
                .ToList();

            offStagePieces.ForEach(piece => InactivePiece(piece));
        }

        private void SetPieceDataArr()
        {
            var startDataIndex = DataManager.Instance.PieceDataObject.GetPieceDataIndex(DataManager.Instance.StageDataObject.StartID);
            var DataList = new List<PieceData>();
            for (var i = startDataIndex; i < startDataIndex + DataManager.Instance.StageDataObject.Index; i++)
            {
                DataList.Add(DataManager.Instance.PieceDataObject.DataArr[i]);
            }
            pieceDataArr = DataList.ToArray();
        }

        private void SpawnPiece()
        {
            if (piecePool.Where(piece => piece.gameObject.activeSelf).ToList().Count >= maxSpawn)
            {
                return;
            }

            if (piecePool.Count == 0)
            {
                ActivePiece(CreatePiece(), GetRandomPos(spawnPoint.position, spawnPoint.localScale));
                return;
            }

            var inactivePieceList = piecePool.Where(piece => !piece.gameObject.activeSelf).ToList();

            if (inactivePieceList.Count == 0)
            {
                ActivePiece(CreatePiece(), GetRandomPos(spawnPoint.position, spawnPoint.localScale));
                return;
            }

            var piece = inactivePieceList[0];
            ActivePiece(piece, GetRandomPos(spawnPoint.position, spawnPoint.localScale));
        }

        private Piece CreatePiece()
        {
            var piece = Instantiate(piecePrefab, spawnPoint.position, Quaternion.identity);
            piece.gameObject.SetActive(false);
            piecePool.Add(piece);
            return piece;
        }

        private void CreatePiece(int count)
        {
            var pieceArr = new Piece[count];
            for (var i = 0; i < count; i++)
            {
                var piece = Instantiate(piecePrefab, spawnPoint.position, Quaternion.identity);
                piece.gameObject.SetActive(false);
                piecePool.Add(piece);
            }
        }

        private void ActivePiece(Piece piece, Vector3 pos)
        {
            piece.Initialize(GetPieceData());
            piece.gameObject.transform.position = pos;
            piece.gameObject.SetActive(true);
        }

        private void ActivePiece(Piece piece, int id,  Vector3 pos)
        {
            piece.Initialize(GetPieceData(id));
            piece.gameObject.transform.position = pos;
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

        private PieceData GetPieceData(int id)
        {
            return DataManager.Instance.PieceDataObject.DataArr[DataManager.Instance.PieceDataObject.GetPieceDataIndex(id)];
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