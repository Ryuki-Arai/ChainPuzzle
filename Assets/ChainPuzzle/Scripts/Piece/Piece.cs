using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityFx.Outline;

public class Piece : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Renderer mesh;
    [SerializeField] private MeshRenderer outline;
    [SerializeField] private MeshFilter outlineMesh;
    public PieceData PieceData { get; private set; }

    private void Awake()
    {
        var Mesh = Instantiate(outlineMesh.sharedMesh);
        Mesh.triangles = Mesh.triangles.Reverse().ToArray();
        outlineMesh.sharedMesh = Mesh;
    }

    public void Initialize(PieceData data)
    {
        Debug.Log("Init Piece");
        PieceData = data;
        SetData();
    }
    
    public void Select()
    {
        //outline.material.color = Color.yellow;
        outline.material.EnableKeyword("_EMISSION");
    }

    private void SetData()
    {
        text.text = PieceData.StrView;
        mesh.material = PieceData.Material;
        //outline.material.color = Color.black;
        outline.material.DisableKeyword("_EMISSION");
    }
}

