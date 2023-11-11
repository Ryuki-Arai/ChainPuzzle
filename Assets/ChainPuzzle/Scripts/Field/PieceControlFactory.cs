using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PieceControlFactory : MonoBehaviour
{
    [SerializeField] PieceDataObject pieceObject;
    [SerializeField] int startID;
    [SerializeField] PieceDigitType startDigit;
    [SerializeField] int index;
    [SerializeField] Piece piecePrefab;
    [SerializeField] Transform spawnParent;
    [SerializeField] int maxSpawn;
    [SerializeField] float inactivePosY;

    [SerializeField] private PieceData[] pieceDataArr;
    private List<Piece> piecePool = new List<Piece>();

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

    private void PieceSafety()
    {
        var offStagePieces = piecePool
            .Where(piece => piece.gameObject.transform.position.y < inactivePosY && piece.gameObject.activeSelf).ToList();

        offStagePieces.ForEach(piece => InactivePiece(piece));
    }

    private void SetPieceDataArr()
    {
        var startDataIndex = pieceObject.GetPieceDataIndex(startID, startDigit);
        var DataList = new List<PieceData>();
        for(var i = startDataIndex; i < startDataIndex + index; i++)
        {
            DataList.Add(pieceObject.DataArr[i]);
        }
        pieceDataArr = DataList.ToArray();
    }

    private void SpawnPiece()
    {
        if(piecePool.Where(piece => piece.gameObject.activeSelf).ToList().Count >= maxSpawn)
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
        var piece = Instantiate(piecePrefab,spawnParent);
        piece.gameObject.SetActive(false);
        piecePool.Add(piece);
        return piece;
    }

    private void CreatePiece(int count)
    {
        var pieceArr = new Piece[count];
        for (var i = 0; i < count; i++)
        {
            var piece = Instantiate(piecePrefab, spawnParent);
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
