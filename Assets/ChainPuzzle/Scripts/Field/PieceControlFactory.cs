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
    [SerializeField] Transform spawnPos;
    [SerializeField] int maxSpawn;

    [SerializeField] private PieceData[] pieceDataArr;
    private List<Piece> piecePool = new List<Piece>();


    private void Start()
    {
        OnInitialized();
    }

    private void OnInitialized()
    {
        SetPieceDataArr();

        this.UpdateAsObservable()
            .Subscribe(_ => SpawnPiece());
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
        var piece = Instantiate(piecePrefab);
        piece.gameObject.SetActive(false);
        piecePool.Add(piece);
        return piece;
    }

    private void ActivePiece(Piece piece)
    {
        piece.Initialize(GetPieceData());
        piece.gameObject.transform.position = spawnPos.position;
        piece.gameObject.SetActive(true);
    }

    private PieceData GetPieceData()
    {
        return pieceDataArr[Random.Range(0, pieceDataArr.Length - 1)];
    }
}
