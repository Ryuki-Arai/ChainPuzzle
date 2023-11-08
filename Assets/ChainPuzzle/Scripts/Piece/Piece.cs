using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Renderer mesh;
    public PieceData Data { get; private set; }


    public void Initialize(PieceData data)
    {
        Data = data;
        SetData();
    }
    
    private void SetData()
    {
        text.text = Data.StrView;
        mesh.material = Data.Material;
    }
}

