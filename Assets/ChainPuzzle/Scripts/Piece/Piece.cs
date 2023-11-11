using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityFx.Outline;

public class Piece : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Renderer mesh;
    [SerializeField] private OutlineBehaviour outline;
    public PieceData PieceData { get; private set; }


    public void Initialize(PieceData data)
    {
        Debug.Log("Init Piece");
        PieceData = data;
        SetData();
    }
    
    public void Select()
    {
        outline.OutlineColor = Color.yellow;
    }

    private void SetData()
    {
        text.text = PieceData.StrView;
        mesh.material = PieceData.Material;
        outline.OutlineColor = Color.black;
    }
}

