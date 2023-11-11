using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerModel
{

    public List<Piece> PieceChain = new List<Piece>();
    public List<Vector3> ChainPoint => PieceChain.Select(piece => piece.gameObject.transform.position).ToList();
    private Camera camera;

    public PlayerModel()
    {
        OnInitialized();
    }

    private void OnInitialized()
    {
        camera = Camera.main;
    }

    private void CreateChain(Piece piece)
    {
        PieceChain.Add(piece);
        piece.Select();
    }

    private void AddChain(Piece piece)
    {
        if (PieceChain[0].PieceData.ID == piece.PieceData.ID
            && PieceChain[0].PieceData.DigitType == piece.PieceData.DigitType
            && !PieceChain.Contains(piece))
        {
            Debug.Log($"{PieceChain[0].PieceData.ID == piece.PieceData.ID}, {PieceChain[0].PieceData.DigitType == piece.PieceData.DigitType}");
            PieceChain.Add(piece);
            piece.Select();
        }
    }

    public void GetPieceItem()
    {
        Ray raycast = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raycast.origin, raycast.direction * 10.0f, Color.red, 3f, false);
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,10.0f))
        {
            var go = hit.collider.gameObject;
            Piece piece;
            if(go.TryGetComponent(out piece))
            {
                if(PieceChain.Count == 0)
                {
                    CreateChain(piece);
                }
                else
                {
                    AddChain(piece);
                }
            }
        }
    }

    public void RemoveChain()
    {
        PieceChain.Clear();
    }
}
