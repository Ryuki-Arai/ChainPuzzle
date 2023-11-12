using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PieceControlFactory : MonoBehaviour
{
    [SerializeField] private StageDataObject stageData;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private PieceData[] pieceDataArr;
    private List<Piece> piecePool = new List<Piece>();

    public void OnInitialized()
    {
        SetPieceDataArr();
        CreatePiece(stageData.MaxSpawn);
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
            .Where(piece => piece.gameObject.transform.position.y < stageData.InactivePosY && piece.gameObject.activeSelf).ToList();

        offStagePieces.ForEach(piece => InactivePiece(piece));
    }

    private void SetPieceDataArr()
    {
        var startDataIndex = stageData.PieceObject.GetPieceDataIndex(stageData.StartID, stageData.StartDigit);
        var DataList = new List<PieceData>();
        for(var i = startDataIndex; i < startDataIndex + stageData.Index; i++)
        {
            DataList.Add(stageData.PieceObject.DataArr[i]);
        }
        pieceDataArr = DataList.ToArray();
    }

    private void SpawnPiece()
    {
        if(piecePool.Where(piece => piece.gameObject.activeSelf).ToList().Count >= stageData.MaxSpawn)
        {
            return;
        }

        if (piecePool.Count == 0)
        {
            ActivePiece(CreatePiece());
            return;
        }

        var inactivePieceList = piecePool.Where(piece => !piece.gameObject.activeSelf).ToList();

        if(inactivePieceList.Count == 0)
        {
            ActivePiece(CreatePiece());
            return;
        }

        var piece = inactivePieceList[0];
        ActivePiece(piece);
    }

    private Piece CreatePiece()
    {
        var piece = Instantiate(stageData.PiecePrefab, spawnParent);
        piece.gameObject.SetActive(false);
        piecePool.Add(piece);
        return piece;
    }

    private void CreatePiece(int count)
    {
        var pieceArr = new Piece[count];
        for (var i = 0; i < count; i++)
        {
            var piece = Instantiate(stageData.PiecePrefab, spawnParent);
            piece.gameObject.SetActive(false);
            piecePool.Add(piece);
        }
    }

    private void ActivePiece(Piece piece)
    {
        piece.Initialize(GetPieceData());
        piece.gameObject.transform.position = spawnParent.position;
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
}
