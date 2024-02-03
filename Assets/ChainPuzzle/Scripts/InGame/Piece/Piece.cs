using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityFx.Outline;

namespace InGame
{
    public class Piece : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Renderer mesh;
        [SerializeField] private MeshRenderer outline;
        [SerializeField] private MeshFilter outlineMesh;
        private Collider collider;
        public PieceData PieceData { get; private set; }
        public Collider Collider => collider;

        private void Awake()
        {
            var Mesh = Instantiate(outlineMesh.sharedMesh);
            Mesh.triangles = Mesh.triangles.Reverse().ToArray();
            outlineMesh.sharedMesh = Mesh;
            collider = GetComponent<Collider>();
        }

        public void Initialize(PieceData data)
        {
            PieceData = data;
            collider.enabled = true;
            SetData();
        }

        public void Select()
        {
            outline.material.color = Color.yellow;
            //outline.material.EnableKeyword("_EMISSION");
        }

        public void Unselect()
        {
            outline.material.color = Color.black;
            //outline.material.DisableKeyword("_EMISSION");
        }

        private void SetData()
        {
            text.text = PieceData.StrView;
            mesh.material = PieceData.Material;
        }
    }
}