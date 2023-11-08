using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerView : MonoBehaviour
{

    [SerializeField] LineRenderer lineRenderer;
    [SerializeField,Range(0.0f,1.0f)] float lineWidth;
    [SerializeField] float linePosZ;

    public void SetUp()
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
    }

    public void DesableChain(List<Piece> pieceList)
    {
        DesablePieces(pieceList);
        ClearLinePoints();
    }

    public void SetLine(List<Vector3> point)
    {
        var positions = point.Select(p => new Vector3(p.x, p.y, linePosZ)).ToArray();
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    private void DesablePieces(List<Piece> pieceList)
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
